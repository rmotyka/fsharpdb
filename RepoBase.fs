module datab.RepoBase

open System
open System.Collections.Generic
open Dapper
open Npgsql

let getConnection =
    let connection = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=cctuwise;User Id=postgres; Password=cctechnology;")
    connection.Open()
    connection

let inline (=>) k v = k, box v

type OptionHandler<'T>() =
    inherit SqlMapper.TypeHandler<option<'T>>()

    override __.SetValue(param, value) = 
        let valueOrNull = 
            match value with
            | Some x -> box x
            | None -> null

        param.Value <- valueOrNull    

    override __.Parse value =
        if isNull value || value = box DBNull.Value 
        then None
        else Some (value :?> 'T)

SqlMapper.AddTypeHandler (OptionHandler<string>())
SqlMapper.AddTypeHandler (OptionHandler<int>())


let dbQuery<'T> (sql: string) (parameters: (string * obj) list option) = 
    use connection = getConnection
    match parameters with
    | Some(p) -> 
        let args = new Dictionary<string, obj>()
        p |> List.iter (fun (key, value) -> args.Add(key, value))
        connection.Query<'T>(sql, args)
    | None    -> 
        connection.Query<'T>(sql)
