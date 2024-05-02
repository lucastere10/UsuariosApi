
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class TokenService
{
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id),
            new Claim(ClaimTypes.DateOfBirth, usuario.dataNascimento.ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DU54D6A84DW84DAS24DAW8D41DW"));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);  

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(60),
            claims: claims,
            signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);   
    }
}