module datab.UserService

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open Models

let getUser userRepoGetUser connection userName = 
    let user = userRepoGetUser connection userName
    //JsonConvert.SerializeObject(user, IdiomaticDuConverter())
    user