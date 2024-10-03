using BuscoAPI.DTOS.Users;
using BuscoAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;


namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public UsersController(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLoginDTO user)
        {
            try
            {
                var userDb = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email || x.Username == user.Username);

                if (userDb == null) return Unauthorized();

                var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, userDb.Password);

                if (!isPasswordCorrect)
                {
                    return Unauthorized();
                }

                var userToken = await TokenHelper.BuildToken(userDb, context, configuration);
                return userToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 500: An error occurred: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserLoginDTO userCreation)
        {
            try
            {
                var creationCode = configuration["Authentication:UserCreationCode"];

                if(userCreation.Code != creationCode)
                {
                    return Unauthorized("Invalid user creation code");
                }

                var user = new User
                {
                    Email = userCreation.Email,
                    Password = HashPassword.HashingPassword(userCreation.Password),
                    Username = userCreation.Username,
                    Role = "Admin"
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                var userToken = await TokenHelper.BuildToken(userCreation, context, configuration);

                return userToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 500: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
