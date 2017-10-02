module datab.RepoAdoBase

open Npgsql
open System.Data.Common
open Microsoft.FSharp.Reflection


type UserX =
    { 
        Id: int
        Username: string option
    }

let toRecordAut<'R> (reader: DbDataReader) = 
    let rty = typeof<'R>
    let allValues = FSharpType.GetRecordFields (rty)
    let getValueFromReader (prop: System.Reflection.PropertyInfo) =
        let ordinal = reader.GetOrdinal(prop.Name)
        //let isOption = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() = typedefof<Option<_>>)
        let returnValue = reader.GetValue(ordinal)
        //if isOption then Some returnValue else returnValue
        returnValue
        
    let vals = allValues |> Array.map (getValueFromReader)

    FSharpValue.MakeRecord(rty, vals, allowAccessToPrivateRepresentation = false) :?> 'R

let ToUserX (reader: DbDataReader) =
    { 
        Id = unbox(reader.["Id"])
        Username = Some (unbox(reader.["Username"]))
    }

// let ToUserX = toRecordAut<UserX>

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
    tx.Commit()
    printfn "%A" users