# Datarisk Case

An API for creating and accessing cpf scoring

## Endpoints

- GET localhost:5000/migrate -> run all the migrations on the Postgres connection
- POST localhost:5000/api {"cpf": "000-000-000/00"} -> Insert a user on the database, with the generated score for the CPF 
- GET localhost:5000/api/<cpf> -> Returns a Json containing the info on the DB of that CPF, example: 
```json
  [
  {
    "id": 1,
    "cpf": "00000000191",
    "score": 860,
    "createdAt": "07/16/2021 17:54:55"
  }
]
```
- GET localhost:5000/api -> Returns a list with all the records on the database

## Running the application

After cloning the repository and cd into it, run on your terminal:

```bash
cd /src
docker-compose up -d
cd /Score
dotnet restore
dotnet run
```
