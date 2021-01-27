using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pokeshop.Api.Common;
using Pokeshop.Api.Dtos.IdentityDtos;
using Pokeshop.Api.Models.Identity;
using Pokeshop.Entities.Entities;
using Pokeshop.Services.Dappers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pokeshop.Api.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public IdentityRepository(
            IIdentityProvider identityProvider,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _identityProvider = identityProvider;
            _mapper = mapper;
            _appSettings = appSettings.Value;

        }

        public async Task<LoginResponse> LoginAsync(LoginVm model)
        {
            model.Password = PasswordHelper.Encrypt(model.Password);
            var loggedInUser = await _identityProvider.LoginAsync(model.Email, model.Password);

            if (loggedInUser == null)
                return new LoginResponse();

            return new LoginResponse
            {
                UserId = loggedInUser.UserId,
                Email = loggedInUser.Email,
                Secret = _appSettings.Secret,
                Success = true
            };
        }

        public async Task<int> RegisterAsync(RegisterDto model)
        {
            var newUser = _mapper.Map<ApplicationUser>(model);
            newUser.Password = PasswordHelper.Encrypt(model.Password);
            newUser.UserId = await GenerateUserIdAsync();
            
            return await _identityProvider.RegisterAsync(newUser);
        }

        public async Task<PagedList<IdentityReadDto>> GetByPageAsync(int pageNumber, int pageSize)
        {
            var users = await _identityProvider.GetByPageAsync(pageNumber, pageSize);
            var result = _mapper.Map<IEnumerable<IdentityReadDto>>(users);
            var usersCount = await _identityProvider.CountAsync();

            return PagedList<IdentityReadDto>.ToPagedList(pageNumber, pageSize, usersCount, result);
        }

        public async Task<IdentityReadDto> GetByIdAsync(string userId)
        {
            var user = await _identityProvider.GetByIdAsync(userId);

            return _mapper.Map<IdentityReadDto>(user);
        }

        public async Task<int> UpdateAsync(IdentityUpdateDto model)
        {
            var modifiedUser = _mapper.Map<ApplicationUser>(model);

            return await _identityProvider.UpdateAsync(modifiedUser);
        }

        public async Task<int> DeleteAsync(string userId)
        {
            return await _identityProvider.DeleteAsync(userId);
        }

        public TokenVm GenerateJwtToken(string userId, string email, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return new TokenVm { Token = encryptedToken };
        }

        private async Task<string> GenerateUserIdAsync()
        {
            var userCount = await _identityProvider.CountAsync();
            var sb = new StringBuilder();
            sb.Append("USER");
            sb.Append(userCount + 1);

            return sb.ToString();
        }
    }
}
