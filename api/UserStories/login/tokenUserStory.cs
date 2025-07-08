using interfaces.DataBase;
using Api.Models;
using common;
using infrastructure.jwt;
using Microsoft.Extensions.Configuration;
using Interfaces.UserStory;
using Models.ClientApi;
using Api.Models.Request;
using Api.Infrastructure.Models;

namespace UserStories.login
{
    public class TokenUserStory(
        IMyUnitOfWork unitOfWork, 
        IMyTokenGenerator tokenGenerator) : ItokenUserStory
    {
        private readonly IMyUnitOfWork unitOfWork = unitOfWork;
        private readonly IMyTokenGenerator tokenGenerator = tokenGenerator;

        public async Task<Response<ClientToken>> GetToken(IConfiguration configuration, LoginRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new Response<ClientToken>() { StatusCode = 400, Message = "Usuario y contraseña requeridos" };
            }

            // check user exist on database
            var RepUser = unitOfWork.Repository<User>();

            var user = await RepUser.FirstOrDefaultAsync(x => x.Username == request.Username).ConfigureAwait(false);

            if (user == null)
            {
                return new Response<ClientToken>() { StatusCode = 400, Message = "Usuario no identificado" };
            }


            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return new Response<ClientToken>() { StatusCode = 400, Message = "Credenciales inválidas" };

            Dictionary<string, bool> claim_list = new Dictionary<string, bool>() {

                { "salesAdd",true },
              
            };

            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            var token = tokenGenerator.GenerateStringToken(user.Id.ToString(), user.Username, user.FullName, claim_list, jwtKey, jwtIssuer, jwtAudience);

            return new Response<ClientToken>() { StatusCode = 200, Message = "OK", Payload = new ClientToken() { Token = token } };
           // return null;
        }
    }
}
