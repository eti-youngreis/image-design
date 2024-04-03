using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImagesDesign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IService<UserDto> service;

        public UserController(IService<UserDto> service, IConfiguration _config)
        {
            this._config = _config;
            this.service = service;
        }

        // GET: api/<UserController>

        // GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public async Task<UserDto?> Get(int id)
        //{
        //    return await service.GetByIdAsync(id);
        //}
        [HttpGet("{name} {password}")]
        public async Task<IActionResult?> Get(string name, string password)
        {
            var user = await Authenticate(name, password);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NoContent();
        }
        // POST api/<UserController>
        [HttpPost]
        public async Task<UserDto?> Post([FromBody] UserDto entity)
        {
            return await service.AddAsync(entity);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<UserDto> Put(int id, UserDto entity)
        {
            return await service.UpdateAsync(id, entity);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<UserDto?> Delete(int id)
        {
            return await service.DeleteAsync(id);
        }

        private async Task<UserDto?> Authenticate(string name, string password)
        {

            var user = (await service.GetAllAsync()).FirstOrDefault(
            user => user.Password.ToLower() == password.ToLower() &&
            user.Name.ToLower() == name.ToLower());
            if (user != null)
                return user;
            return null;
        }

        private string Generate(UserDto user)
        {
            //מפתח להצפנה
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //אלגוריתם להצפנה
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Sid,user.Id.ToString()),
            new Claim(ClaimTypes.UserData,user.Password)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("/getById/{id}")]
        public async Task<IActionResult> GetById(int id) {

            return Ok(await service.GetByIdAsync(id));
        }

    }
}
