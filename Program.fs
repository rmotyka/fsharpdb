open System
open datab

[<EntryPoint>]
let main argv =
    let userList = UserService.getUser RepoUser.getUser "mxManager"
    userList |> Seq.iter (fun x -> printfn "%A" x)

    let testUser : Models.User = {
        Id = 3;
        Username = "xxx";
        Password = "tttt";
        Active = true;
        Role = "r";
        UserFacilityIdLista = "lll"
    }
    UserService.getUser (fun x -> [testUser]) "mxManager"
    |> Seq.iter (fun x -> printfn "%A" x)


    0
