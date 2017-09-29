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


let Query (connectionString: string) toType (sql: string) = 
  seq { use cn = new NpgsqlConnection(connectionString)
        let cmd = new NpgsqlCommand(sql, cn) 
        // cmd.CommandType <- CommandType.Text
    
        cn.Open()
        use (reader: DbDataReader) = cmd.ExecuteReader() :> DbDataReader
        while reader.Read() do
            yield reader |> toType
         }

let getData = 
    let users = Query "Server=127.0.0.1;Port=5432;Database=cctuwise;User Id=postgres; Password=cctechnology;" ToUserX @"Select ""Id"", ""Username"" from public.""User"""
    printfn "%A" users