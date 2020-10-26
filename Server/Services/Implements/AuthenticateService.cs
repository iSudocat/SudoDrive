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

        public bool IsAuthenticated(LoginRequestModel requestModel, out string token)
        {
            token = string.Empty;
            if (!_userService.IsValid(requestModel))
                return false;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,requestModel.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagementModel.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagementModel.Issuer, _tokenManagementModel.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagementModel.AccessExpiration), signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;

        }
    }
}
