using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api_rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetResultAsync()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> AuthLoginAsync([FromBody] AuthUserResource authUserResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var user = _mapper.Map<AuthUserResource, User>(authUserResource);

            var response = await _userService.AuthLoginAsync(user);

            return Ok(response);
        }   

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource saveUserResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessage());

                var user = _mapper.Map<SaveUserResource, User>(saveUserResource);

                var response = await _userService.PostAsync(user);

                if (!response.Success)
                    return BadRequest(response.Message);

                var result = _mapper.Map<User, UserResource>(response.User);

                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
