module datab.RepoAdoBase

open Npgsql
open System.Data.Common


type UserX =
    { 
        Id: int
        Username: string
    }

let ToUserX (reader: DbDataReader) =
    { 
        Id = unbox(reader.["Id"])
        Username = unbox(reader.["Username"])
    }
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