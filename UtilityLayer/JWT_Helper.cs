using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.ResponseModel;
using Newtonsoft.Json;

public class JwtHelper
{
    private IConfiguration _configuration;
    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //private static string SecretKey = "mysupersecret_secretkey!123";
    public string GenerateToken(UserResponseModel user, int expireMinutes = 60)
    {
        string userdata=JsonConvert.SerializeObject(user);
        var jwtsection= _configuration.GetSection("JWTSettings");
        string SecretKey = jwtsection["SecretKey"];

        var symmetricKey = Encoding.UTF8.GetBytes(SecretKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userdata)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var stoken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(stoken);
    }
}
