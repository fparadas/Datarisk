open Saturn
open FSharp.Control.Tasks
open System.Threading.Tasks
open Score.Api.Web

let migrate () = 
    task {
        Score.Migrations.Migration.migrate ()
        return ()
    }
    
let toJson (get: unit -> Task<'b>) (_: 'c) ctx =
    task {
        let! v = get ()
        return! Controller.json ctx v
    }

let appRouter = router {
    get "/migrate" (migrate |> toJson)

    forward "/api" appController
}

let app = application {
    use_router appRouter
    url "http://0.0.0.0:8085/"
}

[<EntryPoint>]
let main argv =
    run app
    0
