module Score.Api.DbModels

type DbUser = 
  {
    Id: int
    Cpf: string
    Score: int
    CreatedAt: string
  }
 
type DbNewUser =
  {
    Cpf: string
    Score: int
    CreatedAt: string
  }