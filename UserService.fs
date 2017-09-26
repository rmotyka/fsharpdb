module datab.UserService

open Newtonsoft.Json

let getUser userRepoGetUser userName = 
    let user = userRepoGetUser userName
    JsonConvert.SerializeObject(user)
