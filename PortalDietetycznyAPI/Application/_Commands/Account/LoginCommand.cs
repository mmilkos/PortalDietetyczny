using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PortalDietetycznyAPI.Domain.Common;
using PortalDietetycznyAPI.Domain.Entities;
using PortalDietetycznyAPI.Domain.Interfaces;
using PortalDietetycznyAPI.Domain.Resources;
using PortalDietetycznyAPI.DTOs;

namespace PortalDietetycznyAPI.Application._Commands.Account;

public class LoginCommand : IRequest<OperationResult<JwtTokenDto>>
{
    public LoginUserRequestDto Dto;
    
    public LoginCommand(LoginUserRequestDto dto)
    {
        Dto = dto;
    }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<JwtTokenDto>>
{
    private readonly IPDRepository _repository;
    private readonly IKeyService _keyService;
    

    public LoginCommandHandler(IPDRepository repository, IKeyService keyService)
    {
        _repository = repository;
        _keyService = keyService;
    }

    public async Task<OperationResult<JwtTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<JwtTokenDto>()
        {
            Data = new JwtTokenDto()
        };

        var any = await _repository.AnyAsync<User>();

        if (any == false)
        {
            using var hmac = new HMACSHA512();
            
            var user = new User(
                userName: request.Dto.Username.ToLower(), 
                passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Dto.Password)),
                passwordSalt: hmac.Key);
            
          await  _repository.AddAsync(user);
          return operationResult;
        }

        var settings = await _keyService.GetPortalSettings();
        
        var account = await _repository.FindEntityByConditionAsync<User>(u => u.UserName == request.Dto.Username.ToLower());
        
        if (account == null)
        {
            operationResult.AddError(ErrorsRes.IncorrectCredentials);
            return operationResult;
        }

        var isCorrectPassword = CheckCredentials(account, request.Dto);

        if (isCorrectPassword == false)
        {
            operationResult.AddError(ErrorsRes.IncorrectCredentials);
            return operationResult;
        }

        var token = GenerateJwtToken(account, settings);

        operationResult.Data = token;

        return operationResult;
    }
    
    private bool CheckCredentials(User? user, LoginUserRequestDto loginRequestDto)
    {
        if (user == null) return false;
        
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequestDto.Password));
        
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return false;
            }
        }

        return true;
    }
    
    private JwtTokenDto GenerateJwtToken(User user, PortalSettings settings)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(settings.JwtExpireHours);

        var token = new JwtSecurityToken(
            issuer: settings.JwtIssuer,
            audience: settings.JwtIssuer,
            claims: claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var cookieOptions = new CookieOptions() 
        { 
            Expires = expires, 
            HttpOnly = true,
            Secure = false,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        var result = new JwtTokenDto()
        {
            Token = tokenHandler.WriteToken(token),
            CookieOptions = cookieOptions
        };

        return result;
    }
}