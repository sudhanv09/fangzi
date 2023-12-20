package db

import "database/sql"

func initPgDb() error {
	connStr := "host=localhost;user=postgres;password=postgres;dbname=fangzidb;port=5432;pooling=true;"
	_, err := sql.Open("postgres", connStr)
	if err != nil {
		return err
	}
	return nil
}
