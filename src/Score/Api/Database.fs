module Score.Api.Database

open FSharp.Control.Tasks
open Score.Api.DbModels
open Npgsql.FSharp


let getAll connectionString =
  task {
    let! result =
      connectionString
      |> Sql.connect
      |> Sql.query "SELECT * FROM users"
      |> Sql.executeAsync (fun read -> 
        {
          Id = read.int "id"
          Cpf = read.text "cpf"
          Score = read.int "score"
          CreatedAt = read.text "created_at"
        })
    
    return result
  }
  
let getAllByCpf connectionString (cpf: string) =
  task {
    let! result =
      connectionString
      |> Sql.connect
      |> Sql.query "SELECT * FROM users WHERE cpf = @cpf"
      |> Sql.parameters ["cpf", Sql.text cpf]
      |> Sql.executeAsync (fun read -> 
        {
          Id = read.int "id"
          Cpf = read.text "cpf"
          Score = read.int "score"
          CreatedAt = read.text "created_at"
        })
    
    return result
  }
  

let create connectionString newUser = 
  task {
    let! _ = 
      connectionString
      |> Sql.connect
      |> Sql.query "INSERT INTO users (cpf, score, created_at) VALUES (@cpf, @score, @created_at)"
      |> Sql.parameters [ "cpf", Sql.text newUser.Cpf; "score", Sql.int newUser.Score; "created_at", Sql.text newUser.CreatedAt]
      |> Sql.executeNonQueryAsync 
    
    return ()
  }
  


type UserDto = 
  {
    Id: int
    Cpf: string
    Score: int
    CreatedAt: string
  }


module UserDto =
  let fromDbModel (dbModel: DbUser) = 
    {
      Id = dbModel.Id
      Cpf = dbModel.Cpf
      Score = dbModel.Score
      CreatedAt = dbModel.CreatedAt
    }


type NewUserDto =
  {
    Cpf: string
    Score: int
    CreatedAt: string
  }

module NewUserDto =
  let toDbModel dto: DbNewUser =
    {
      Cpf = dto.Cpf
      Score = dto.Score
      CreatedAt = dto.CreatedAt
    }

  let create cpf = 
    // We can insert validations later if needed

    let score = System.Random().Next(1,1000) // generate a random integer less between 1 and 1000
    let createdAt = System.DateTime.Now.ToString() // get the system current date

    {
      Cpf = cpf
      Score = score
      CreatedAt = createdAt
    }