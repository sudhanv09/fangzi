package auth

import (
	"database/sql"
	"github.com/redis/go-redis/v9"
)

type AuthManager struct {
	Db  *sql.DB
	Rdb *redis.Client
}

type SignUpRequestBody struct {
	Name     string `json:"name" validate:"required"`
	Email    string `json:"email" validate:"required,email"`
	Password string `json:"password" validate:"required,min=6"`
}

type User struct {
	Id       int
	Name     string
	Email    string
	Password string
}

type UserSessionResponse struct {
	Id    int
	Name  string
	Email string
}
