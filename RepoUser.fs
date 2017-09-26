module datab.RepoUser

open datab.RepoBase
open datab.Models

let getUser name = 
    let sql = @"select * from public.""User"" where ""Username"" = @username"
    dbQuery<User> sql (Some (["username" => name]))
