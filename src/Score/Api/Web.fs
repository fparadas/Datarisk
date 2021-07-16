module Score.Api.Web

open Score.Api.Database
open Saturn

open FSharp.Control.Tasks
open Microsoft.AspNetCore.Http
open FSharp.Data

type Request = 
  {
    CPF: string
  }

type AppSettings = JsonProvider<"config.json">
let conf = AppSettings.Load("config.json")
let connectionString = conf.ConnectionStrings.Score

let private getAll (ctx: HttpContext) =
  task {
    let! dbUsers = Database.getAll connectionString
    let users = 
      dbUsers
      |> List.map UserDto.fromDbModel
    
    return!
      users
      |> Controller.json ctx
  }
 
let private getAllByCpf (ctx: HttpContext) (cpf: string) =
  task {
    let! dbUsers = Database.getAllByCpf connectionString cpf
    let users = 
      dbUsers
      |> List.map UserDto.fromDbModel
    
    return!
      users
      |> Controller.json ctx
  }

let private createUser (ctx: HttpContext) = 
  task {

    let! obj = (Controller.getJson ctx)

    let dtoUsers = 
      obj.CPF
      |> NewUserDto.create
      |> NewUserDto.toDbModel
    
    let! _ = Database.create connectionString dtoUsers

    return Controller.text ctx "OK"
  }

let appController = controller {
  index getAll
  show getAllByCpf
  create createUser
}