module datab.UserService

open Newtonsoft.Json
open Newtonsoft.Json.Converters
open Models

let getUser userRepoGetUser userName = 
    let user = userRepoGetUser userName
    JsonConvert.SerializeObject(user, IdiomaticDuConverter())