﻿using Application.Constants;
using Application.Extensions;
using Application.Services.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services;

public class TokenService : ITokenService
{
    private const int TokenMinutesLifetime = 999;

    private readonly IConfiguration _config;
    private readonly IUnitOfWork _uow;
    private readonly SymmetricSecurityKey _key;

    public TokenService(
        IConfiguration config,
        IUnitOfWork uow)
    {
        _config = config;
        _uow = uow;
        _key = new SymmetricSecurityKey(_config[ApplicationSectionConstants.JwtTokenKey].ToByteArray());
    }

    public async Task<Result<string>> CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        user.UserRoles
            .Select(ur => ur.Role)
            .ToList()
            .ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Name)));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenMinutesLifetime.Minutes()),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Result<string>.Success(tokenHandler.WriteToken(token));
    }
}
