module Score.Migrations.Migration

open Score.Migrations
open Npgsql
open FSharp.Data
open SimpleMigrations
open SimpleMigrations.DatabaseProvider

type Appsettings = JsonProvider<"config.json">

let migrate () =
    try 
        let conf = Appsettings.Load("config.json")
        use connection = new NpgsqlConnection(conf.ConnectionStrings.Score)
        
        let provider = PostgresqlDatabaseProvider(connection)
        let migrator = SimpleMigrator(typeof<Initial>.Assembly, provider)
        
        migrator.Load()
        migrator.MigrateToLatest()
    with
        | e -> printfn "%s" e.Message 