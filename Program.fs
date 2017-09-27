open System
open datab

[<EntryPoint>]
let main argv =
    let userList = UserService.getUser RepoUser.getUser "mxManager" |> Async.RunSynchronously
    userList |> printfn "%A"

    // let testUser : Models.User = {
    //     Id = 3;
    //     Username = "xxx";
    //     Password = "tttt";
    //     Active = true;
    //     Role = "r";
    //     UserFacilityIdLista = Some "lll"
    // }

    // UserService.getUser (fun x -> [testUser]) "mxManager"
    // |> printfn "%A"

    0
