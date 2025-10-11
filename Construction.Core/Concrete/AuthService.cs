using Azure.Core;
using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IUserRepository userRepository , IUserRoleRepository userRoleRepository , IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<AuthonticateResponseModel> LoginAsync(UserLoginRequestModel loginRequest)
        {
            // Find user by email (or username, depending on your logic)
            var user = _userRepository.GetAll()
                .FirstOrDefault(u => u.Email == loginRequest.UserName && u.Isactive == true);
            // Verify password (assuming user.Password is hashed)
            bool isPasswordValid = user != null ? BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password) : false;

            if (isPasswordValid && user != null)
            {
                var userrole = await _userRoleRepository.GetByUserIdAsync(user.Userid);
                var role = await _roleRepository.GetAsyncById(userrole.Roleid);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Firstname + ' ' + user.Lastname),
                    new Claim(ClaimTypes.Role, role.Rolename),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserId", user.Userid.ToString()),
                    new Claim("OrganisationId", user.OrganisationId.ToString()),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_very_secure_32char_key!"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7047/",
                    audience: "https://localhost:7047/",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                return new AuthonticateResponseModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token), 
                };
            }
            else
            {
                return new AuthonticateResponseModel
                {
                    Token = null
                };
            }
        }
    }
}
