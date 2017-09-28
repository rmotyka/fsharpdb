module datab.RepoUser

open datab.RepoBase
open datab.Models

let getUser connection name = 
    let sql = @"select * from public.""User"" where ""Username"" = @username"
    dbQuery<User> connection sql (Some (["username" => name]))
