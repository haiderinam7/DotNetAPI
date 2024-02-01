using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _config = new config;

        }

        [HttpPost("Register")]

        public IActionResult Register(UserForRegistrationDto userForRegistrationDto)
        {
            if (userForRegistration.Password == userForRegistration.Password)
            {
                string sqlCheckUserExists = "SELECT Email FROM TutorialAppSchema.Auth WHERE Email = '" +
                userForRegistrationDto.Email = "'";

                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);
                if (existingUsers.Count() == 0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value +
                    Convert.ToBase64String(passwordSalt);

                    byte[] passwordHash = KeyDerivation.Pbkdf2(
                        password: userForRegistration.Password,
                        salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 1000000,
                        numBytesRequested: 256 / 8
                    );

                    string sqlAddAuth = @"
                    INSERT INTO TutorialAppSchema.Auth ([Email],
                    [PasswordHash],
                    [PasswordSalt]) VALUES ('" + userForRegistrationDto.Email +
                    "', @Passwordhash, @PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", System.Data.SqlDbType.VarBinary);
                    passwordSaltParameter.Value = passwordSalt;
                    
                    SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarBinary);
                    passwordHashParameter.Value = passwordHash;
                    
                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParameter);

                    if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                        return Ok();
                    }
                }
                throw new Exception ("User with this email already exists!");
            }
            throw new Exception("Passwords do not match!");
        }

        [HttpPost("Login")]

        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            return Ok();
        }

        private string CreateToken(int userId)
        {
            Claim[] claims = new Claim[] {
                new Claim("userId", userId.ToString())
            };

            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config.GetSection("Appsettings:TokenKey").Value
                )
            );

            SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}