package auth

import (
	"context"
	"database/sql"
	"encoding/json"
	"github.com/google/uuid"
	"github.com/redis/go-redis/v9"
	"golang.org/x/crypto/bcrypt"
	"time"
)

func InitSessionManager(rdb *redis.Client, db *sql.DB) *AuthManager {
	return &AuthManager{Db: db, Rdb: rdb}
}

func (auth *AuthManager) GetUserSession(sessionId string) (UserSessionResponse, error) {
	var user UserSessionResponse
	err := auth.Rdb.Get(context.Background(), sessionId).Scan(&user)
	if err != nil {
		return user, err
	}
	return user, nil
}

func (auth *AuthManager) generateSession(data UserSessionResponse) (string, error) {
	sessionId := uuid.NewString()
	session, _ := json.Marshal(data)
	err := auth.Rdb.Set(context.Background(), sessionId, session, 24*time.Hour).Err()
	if err != nil {
		return "", err
	}
	return sessionId, nil
}

func (auth *AuthManager) LogInHandler(email, password string) (string, error) {
	var user User

	err := auth.Db.QueryRow("SELECT id, name, email, password FROM users WHERE email = $1", email).Scan(&user.Id, &user.Name, &user.Email, &user.Password)
	if err != nil {
		return "", err
	}

	err = bcrypt.CompareHashAndPassword([]byte(user.Password), []byte(password))
	if err != nil {
		return "", err
	}

	sessionId, err := auth.generateSession(UserSessionResponse{
		Id:    user.Id,
		Name:  user.Name,
		Email: user.Email,
	})
	if err != nil {
		return "", err
	}

	return sessionId, nil
}

func (auth *AuthManager) LogOutHandler(sessionId string) error {
	return auth.Rdb.Del(context.Background(), sessionId).Err()
}

func (auth *AuthManager) SignUpHandler(user SignUpRequestBody) (string, error) {

	hashedPass, err := bcrypt.GenerateFromPassword([]byte(user.Password), bcrypt.DefaultCost)
	if err != nil {
		return "", err
	}

	res, err := auth.Db.Exec("INSERT INTO users (name, email, password, join_date) VALUES ($1, $2, $3, $4)",
		user.Name, user.Email, string(hashedPass), time.Now().Format(time.RFC3339))
	if err != nil {
		return "", err
	}

	id, err := res.LastInsertId()
	if err != nil {
		return "", err
	}

	sessionId, err := auth.generateSession(UserSessionResponse{
		Id:    int(id),
		Name:  user.Name,
		Email: user.Email,
	})
	if err != nil {
		return "", err
	}

	return sessionId, nil
}
