module datab.JwtGenerator

open Microsoft.IdentityModel.Tokens
open System.Security.Claims

let validatePassword userName password =
    userName = "Admin" && password = "Admin"

let buildToken userName = 
    let claims =
    [
        Claim(ClaimTypes.Name,      "John",  ClaimValueTypes.String, issuer)
        Claim(ClaimTypes.Surname,   "Doe",   ClaimValueTypes.String, issuer)
        Claim(ClaimTypes.Role,      "Admin", ClaimValueTypes.String, issuer)
    ]

    let secretKey = "VerySecretKey";
    let signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
    let expiresHours = 2
    let issuer = "http://localhost:5000"
    let jwt = new JwtSecurityToken(
        issuer: issuer,
        audience: "all",
        claims: claims,
        notBefore: now,
        expires: now.Add(new TimeSpan(0, expiresHours, 0, 0)),
        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
    let encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    encodedJwt

let getToken userName password =
    let isValid = validatePassword userName password

    if isValid then 
        buildToken userName
    else
        "Invalid password"
