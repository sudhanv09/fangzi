package main

import (
	"encoding/json"
	"github.com/go-playground/validator/v10"
	"net/http"
	"sudhanv09/fangzi/auth"
)

type EndpointManager struct {
	auth *auth.AuthManager
}

func InitEndpoints(auth *auth.AuthManager) *EndpointManager {
	return &EndpointManager{auth: auth}
}

// @Summary Get listings
// @Description Get listings
// @Tags listings
// @Produce json
func getListings(w http.ResponseWriter, r *http.Request) {}

// @Summary Get listing
// @Description Get listing
// @Tags listings
// @Produce json
func getListingById(w http.ResponseWriter, r *http.Request) {}

// @Summary Create listing
// @Description Create listing
// @Tags listings
// @Accept json
// @Produce json
func createListings(w http.ResponseWriter, r *http.Request) {}

// Login @Summary Login
// @Description Login user
// @Tags auth
// @Accept json
// @Produce json
// @Param user body main.signInRequestBody true "User credentials"
// @Success 200 {string} string "Session ID"
// @Router /login [post]
func (e *EndpointManager) Login(w http.ResponseWriter, r *http.Request) {
	var user signInRequestBody

	if err := json.NewDecoder(r.Body).Decode(&user); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	validate := validator.New()
	err := validate.Struct(user)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	sessionId, err := e.auth.LogInHandler(user.Email, user.Password)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	w.Write([]byte(sessionId))

}

// Register @Summary Register
// @Description Register user
// @Tags auth
// @Accept json
// @Produce json
// @Param user body main.signUpRequestBody true "User credentials"
// @Success 200 {string} string "Session ID"
// @Router /register [post]
func (e *EndpointManager) Register(w http.ResponseWriter, r *http.Request) {
	var user auth.SignUpRequestBody

	if err := json.NewDecoder(r.Body).Decode(&user); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	validate := validator.New()
	err := validate.Struct(user)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	sessionId, err := e.auth.SignUpHandler(user)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	w.Write([]byte(sessionId))
}

// Logout @Summary Logout
// @Description Logout user
// @Tags auth
// @Accept json
// @Produce json
// @Success 200 {string} string "Session ID"
// @Router /logout [post]
func (e *EndpointManager) Logout(w http.ResponseWriter, r *http.Request) {
	sessionHeader := r.Header.Get("Authorization")
	if sessionHeader == "" || len(sessionHeader) < 8 || sessionHeader[:7] != "Bearer " {
		http.Error(w, "Unauthorized", http.StatusUnauthorized)
		return
	}

	sessionId := sessionHeader[7:]
	err := e.auth.LogOutHandler(sessionId)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	w.Write([]byte("Logged Out Successfully"))
}
