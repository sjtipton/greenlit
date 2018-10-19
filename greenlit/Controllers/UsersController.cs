﻿using AutoMapper;
using greenlit.Dtos;
using greenlit.Entities;
using greenlit.Helpers;
using greenlit.Helpers.Jwt;
using greenlit.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace greenlit.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IJwtService _jwtService;
        private IMapper _mapper;
        private readonly JwtIssuerOptions _jwtOptions;

        public UsersController(IUserService userService, IJwtService jwtService, IMapper mapper, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userService = userService;
            _jwtService = jwtService;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        // POST users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]CredentialsDto credentialsDto)
        {
            var user = _userService.Authenticate(credentialsDto.EmailAddress, credentialsDto.Password);

            if (user == null)
                return BadRequest(new { message = "Email address or password is incorrect" });

            var identity = _jwtService.GenerateClaimsIdentity(user.EmailAddress, user.Id);
            var jwt = await Tokens.GenerateJwt(identity, _jwtService, credentialsDto.EmailAddress, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        // POST users/register
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        // GET users/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        // PUT users/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE users/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
