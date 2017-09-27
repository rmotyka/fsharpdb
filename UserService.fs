module datab.UserService

open Newtonsoft.Json
open Models

let getUser (userRepoGetUser: string -> Async<(User seq)>) userName = 
    async {
        let! user = userRepoGetUser userName
        return JsonConvert.SerializeObject(user)
    }