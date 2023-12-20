package db

import (
	"database/sql"
	_ "github.com/lib/pq"
	"log"
)

var Db *sql.DB

func InitPgDb() (*sql.DB, error) {
	connStr := "host=localhost user=postgres password=postgres dbname=fangzidb sslmode=disable"
	var err error

	Db, err = sql.Open("postgres", connStr)
	if err != nil {
		log.Fatal("Db open error: ", err)
		return nil, err
	}
	defer Db.Close()

	return Db, nil
}
