package main

import (
	"encoding/json"
	"github.com/go-playground/validator/v10"
	"net/http"
	"sudhanv09/fangzi/auth"
)

type endpointManager struct {
	auth *auth.AuthManager
}

func getListings(w http.ResponseWriter, r *http.Request) {}

func getListingById(w http.ResponseWriter, r *http.Request) {}

func createListings(w http.ResponseWriter, r *http.Request) {}

func (e *endpointManager) login(w http.ResponseWriter, r *http.Request) {
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

func register(w http.ResponseWriter, r *http.Request) {}

func logout(w http.ResponseWriter, r *http.Request) {}

func health(w http.ResponseWriter, r *http.Request) {}
