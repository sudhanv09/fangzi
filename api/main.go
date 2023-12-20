package main

import (
	"context"
	"fmt"
	"net/http"
	"sudhanv09/fangzi/auth"
	"sudhanv09/fangzi/db"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
	"github.com/go-chi/cors"
	"github.com/swaggo/http-swagger"
	"go.uber.org/fx"

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
func initRouter(lc fx.Lifecycle, epmgr *EndpointManager) {
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

	r.Post("/login", epmgr.Login)
	r.Post("/register", epmgr.Register)
	r.Get("/logout", epmgr.Logout)

	lc.Append(fx.Hook{
		OnStart: func(ctx context.Context) error {
			fmt.Println("Started Server. Listening on :3720")
			server := http.Server{
				Addr:    ":3720",
				Handler: r,
			}

			// Run the server in a separate goroutine
			go func() {
				if err := server.ListenAndServe(); err != nil {
					fmt.Printf("Error starting server: %s\n", err)
				}
			}()

			// Wait for the context to be done (e.g., when the application is stopped)
			<-ctx.Done()

			// Shutdown the server gracefully
			if err := server.Shutdown(context.Background()); err != nil {
				fmt.Printf("Error shutting down server: %s\n", err)
			}

			return nil
		},
		OnStop: func(ctx context.Context) error {
			fmt.Println("Stopping Server")
			return nil
		},
	})
}

func main() {
	fx.New(
		fx.Provide(
			db.InitRedis,
			db.InitPgDb,
			auth.InitSessionManager,
			InitEndpoints,
		), fx.Invoke(initRouter),
	).Run()
}
