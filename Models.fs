module datab.Models

type User = {
    Id: int
    Username: string
    Password: string
    Active: bool
    Role: string
    UserFacilityIdLista: string
}