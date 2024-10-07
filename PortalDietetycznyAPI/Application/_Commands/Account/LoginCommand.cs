using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<User> _userManager;
    private readonly IKeyService _keyService;
 

    public LoginCommandHandler(IPDRepository repository, IKeyService keyService, UserManager<User> userManager)
    {
        _repository = repository;
        _keyService = keyService;
        _userManager = userManager;
    }

    public async Task<OperationResult<JwtTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<JwtTokenDto>()
        {
            Data = new JwtTokenDto()
        };

        var dto = request.Dto;
        
        var anyUsers = await _repository.AnyUserAsync();

        if (anyUsers == false)
        {
            var user = new User()
            {
                UserName = dto.Username.ToLower(),
            };

            var registerResult = await _userManager.CreateAsync(user, dto.Password);
            if (registerResult.Succeeded == false)
            {
                operationResult.AddError(ErrorsRes.InvalidCredentials);
                return operationResult;
            }
        }
        
        var userInDb = await _userManager.FindByNameAsync(dto.Username.ToLower());

        var isCorrectPassword = await _userManager.CheckPasswordAsync(userInDb, dto.Password);
        
        if (isCorrectPassword == false)
        {
            operationResult.AddError(ErrorsRes.InvalidCredentials);
            return operationResult;
        }
        
        var settings = await _keyService.GetPortalSettings();

        var token = GenerateJwtToken(userInDb, settings);

        operationResult.Data = token;

        return operationResult;
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
        
        var cookieOptions = new CookieOptions() 
        { 
            Expires = expires, 
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var result = new JwtTokenDto()
        {
            Token = tokenHandler.WriteToken(token),
            CookieOptions = cookieOptions
        };

        return result;
    }
}