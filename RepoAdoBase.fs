module datab.RepoAdoBase

open Npgsql
open System.Data.Common
open Microsoft.FSharp.Reflection


type UserX =
    { 
        Id: int
        Username: string
    }

let toRecordAut<'R> (reader: DbDataReader) = 
    let rty = typeof<'R>
    let allValues = FSharpType.GetRecordFields (rty)
    let getValueFromReader name =
        let ordinal = reader.GetOrdinal(name)
        reader.GetValue(ordinal)
    let vals = allValues |> Array.map (fun x -> getValueFromReader x.Name)

    FSharpValue.MakeRecord(rty, vals, allowAccessToPrivateRepresentation = false) :?> 'R

// let ToUserX (reader: DbDataReader) =
//     { 
//         Id = unbox(reader.["Id"])
//         Username = unbox(reader.["Username"])
//     }

let ToUserX = toRecordAut<UserX>

let dbConnection connectionString = 
    let conn = new NpgsqlConnection(connectionString)
    conn.Open()
    conn

let query toType connection (sql: string) = 
  seq { let cmd = new NpgsqlCommand(sql, connection)
        // cmd.CommandType <- CommandType.Text
    
        use (reader: DbDataReader) = cmd.ExecuteReader() :> DbDataReader
        while reader.Read() do
            yield reader |> toType
         }

let connectionFactory = dbConnection "Server=127.0.0.1;Port=5432;Database=cctuwise;User Id=postgres; Password=cctechnology;" 
let userXLoader = query ToUserX

let getData = 
    use myConnection = connectionFactory
    use tx = myConnection.BeginTransaction()
    let users = userXLoader myConnection @"Select ""Id"", ""Username"" from public.""User"""
    printfn "%A" users