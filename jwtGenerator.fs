module datab.JwtGenerator

open System
open System.IdentityModel.Tokens.Jwt
open System.Security.Claims
open System.Text
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.IdentityModel.Tokens

let validatePassword userName password =
    userName = "Admin" && password = "Admin"

let buildToken userName = 
    let issuer = "http://localhost:5000"
    let claims = [
        Claim(ClaimTypes.Name, "John", ClaimValueTypes.String, issuer);
        Claim(ClaimTypes.Surname, "Doe", ClaimValueTypes.String, issuer);
        Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, issuer)
    ]

    let secretKey = "VerySecretKeyase23423dq3ew3e23e23ewdqwerqw3a"
    let signingKey = SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
    let expiresHours = 2
    let (now: Nullable<DateTime>) =  Nullable<DateTime>(DateTime.UtcNow)
    let expireTime = Nullable<DateTime>(now.Value.Add(TimeSpan(0, expiresHours, 0, 0)))
    let jwt = JwtSecurityToken(issuer, "all", claims, now, expireTime, SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256))
    let jwtSecurityTokenHandler = JwtSecurityTokenHandler()
    let encodedJwt = jwtSecurityTokenHandler.WriteToken(jwt)
    encodedJwt

let getToken userName password =
    let isValid = validatePassword userName password

    if isValid then 
        buildToken userName
    else
        "Invalid password"
