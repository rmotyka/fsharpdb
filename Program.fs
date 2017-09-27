open System
open datab

[<EntryPoint>]
let main argv =
    let userList = UserService.getUser RepoUser.getUser "mxManager" |> Async.RunSynchronously
    userList |> printfn "%A"

    let testUser : Models.User = {
        Id = 3;
        Username = "xxx";
        Password = "tttt";
        Active = true;
        Role = "r";
        UserFacilityIdLista = Some "lll"
    }

    UserService.getUser (fun x -> async { return Seq.ofList [testUser]}) "mxManager" 
    |> Async.RunSynchronously
    |> printfn "%A"

    0
