# MoviesAPI
This project was about provide Application Program Interface Movies Information equipped with unit testing.

## Dev Environment Setup
* **Back end** (.Net Core 2.1)
* **ORM** (Dapper 2.1)
* **Unit Testing** (xUnit Test Project 2.4)
* **Database** (Mysql)

## API

#### /movies
* `GET` : Get all movies data
* `POST` : Create a new movies

#### /movies/:id
* `GET` : Get a specific movie
* `PATCH` : Update a specific movie
* `DELETE` : Delete a specific movie

### Example Request Body
{
    "id" : 1,
    "title" : "Pengabdi",
    "description" : "Adalah sebuah film horor Indonesia tahun 2022 yang disutradarai dan ditulis oleh Joko Anwar sebagai sekuel dari film tahun 2017, Pengabdi Setan.",
    "rating" : 22.0,
    "image" : "",
    "created_at" : "2022-08-01 10:56:31",
    "updated_at": "2022-08-13 09:30:23"
}
