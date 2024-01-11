using API.Dtos;
using API.Errors;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IActiveDirectoryService _activeDirectoryService;

        public AccountController(ITokenService tokenService, IActiveDirectoryService activeDirectoryService) 
        {
            _tokenService = tokenService;
            _activeDirectoryService = activeDirectoryService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var result = await _activeDirectoryService.ValidateActiveDirectoryCredentials(loginDto.Username, loginDto.Password);

            if (!result) return Unauthorized(new ApiResponse(401));

            var user = new UserEntity() 
            {
                Username = loginDto.Username,
                Password = loginDto.Password
            };

            return new UserDto
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
