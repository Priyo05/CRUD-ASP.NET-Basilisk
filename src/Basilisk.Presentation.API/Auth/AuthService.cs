using Basilisk.Business.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Basilisk.Presentation.API.Auth;
public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly IAuthRepository _authRepository;

    public AuthService(IConfiguration configuration, IAuthRepository authRepository)
    {
        _configuration = configuration;
        _authRepository = authRepository;
    }

    public AuthRespondDto CreateToken(AuthRequestDto requestDto)
    {
        var user = _authRepository.GetAccount(requestDto.Username);

        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(requestDto.Password, user.Password);
        if (!isCorrectPassword)
        {
            throw new KeyNotFoundException("Username atau Password Salah");
        }

        var algorithm = SecurityAlgorithms.HmacSha256;

        //memasukan data payload

        var payload = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role,user.Role.ToString()),
        };

        var signature = _configuration.GetSection("AppSettings:TokenSignature").Value;
        var encodedSignature = Encoding.UTF8.GetBytes(signature);

        var Token = new JwtSecurityToken(
            claims: payload,
            expires: DateTime.Now.AddDays(10),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(encodedSignature), algorithm)
            );

        var serializeToken = new JwtSecurityTokenHandler().WriteToken(Token);
        return new AuthRespondDto
        {
            Token = serializeToken
        };
    }

}

