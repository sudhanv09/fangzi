package db

import (
	"database/sql"
	_ "github.com/lib/pq"
)

func InitPgDb() (*sql.DB, error) {
	connStr := "host=localhost;user=postgres;password=postgres;dbname=fangzidb;port=5432;pooling=true;"
	db, err := sql.Open("postgres", connStr)
	if err != nil {
		return nil, err
	}
	return db, nil
}
