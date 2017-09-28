module datab.RepoBase

open System
open System.Collections.Generic
open Dapper
open Npgsql

let getConnection () =
    printfn "GET connection"
    let connection = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=cctuwise;User Id=postgres; Password=cctechnology;")
    connection

let inline (=>) k v = k, box v

let dbQuery<'T> (connection: NpgsqlConnection) (sql: string) (parameters: (string * obj) list option) = 
    match parameters with
    | Some(p) ->
        let args = new Dictionary<string, obj>()
        p |> List.iter (fun (key, value) -> args.Add(key, value))
        connection.QueryAsync<'T>(sql, args)
    | None ->
        connection.QueryAsync<'T>(sql)
    |> Async.AwaitTask
