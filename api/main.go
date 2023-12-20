package main

import (
	"fmt"
	"net/http"
	"sudhanv09/fangzi/auth"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
	"github.com/go-chi/cors"
	"github.com/swaggo/http-swagger"

	_ "sudhanv09/fangzi/docs"
)

// @title Swagger Example API
// @version 1.0
// @description Backend API for fangzi.
// @termsOfService http://swagger.io/terms/

// @contact.name API Support
// @contact.url http://www.swagger.io/support
// @contact.email support@swagger.io

// @license.name Apache 2.0
// @license.url http://www.apache.org/licenses/LICENSE-2.0.html
func initRouter() {
	r := chi.NewRouter()
	r.Use(middleware.Logger)
	r.Use(cors.Handler(cors.Options{}))

	r.Get("/", func(w http.ResponseWriter, r *http.Request) {
		w.Write([]byte("welcome"))
	})

	r.Get("/swagger/*", httpSwagger.Handler(
		httpSwagger.URL("http://localhost:3720/swagger/doc.json"),
	))

	//r.Get("/listings", getListings)
	//r.Get("/listings/{id}", getListingById)
	//r.Post("/listings/new", createListings)

	r.Post("/login", Login)
	r.Post("/register", Register)
	r.Get("/logout", Logout)

	fmt.Println("Started Server. Listening on :3720")
	err := http.ListenAndServe(":3720", nil)
	if err != nil {
		return
	}

}

func main() {
	authMgr := &auth.AuthManager{}
	authMgr.InitDb()

	initRouter()
}
