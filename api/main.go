package main

import (
	"net/http"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
)

func initRouter() {
	r := chi.NewRouter()
	r.Use(middleware.Logger)
	r.Get("/", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte("welcome"))
	})

	r.Get("/listings", getListings)
	r.Get("/listings/{id}", getListingById)

	r.Post("/listings/new", createListings)
	r.Post("/login", login)
	r.Post("/register", register)
	r.Get("/logout", logout)

	http.ListenAndServe(":8080", r)
}

func main() {
	initRouter()
}
