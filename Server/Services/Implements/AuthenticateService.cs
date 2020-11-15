using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Models;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserService _userService;
        private readonly TokenManagementModel _tokenManagementModel;

        public AuthenticateService(IUserService userService, IOptions<TokenManagementModel> tokenManagement)
        {
            _userService = userService;
            _tokenManagementModel = tokenManagement.Value;
        }

        public string GetNewToken(User loginUser)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "" + loginUser.Username),
                new Claim(ClaimTypes.Actor, "" + loginUser.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagementModel.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagementModel.Issuer, _tokenManagementModel.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagementModel.AccessExpiration), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public bool IsAuthenticated(LoginRequestModel requestModel, out string token, out User loginUser)
        {
            token = string.Empty;
            if (!_userService.IsValid(requestModel, out loginUser))
                return false;

            token = GetNewToken(loginUser);
            return true;

        }
    }
}
