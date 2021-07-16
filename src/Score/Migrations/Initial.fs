namespace Score.Migrations

open SimpleMigrations

[<Migration(1L, "Create Users table")>]
type Initial() =
    inherit Migration()

    override this.Up() =
        this.Execute
            @"CREATE TABLE users (
                id serial PRIMARY KEY,
                cpf VARCHAR(50) NOT NULL,
                score integer NOT NULL,
                created_at VARCHAR(50) NOT NULL
            )"
    
    override this.Down() =
        this.Execute "DROP TABLE users"
    