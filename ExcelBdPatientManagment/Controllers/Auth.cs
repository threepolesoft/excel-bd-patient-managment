
using API.Repository;
using API.Repository.Interface;
using API.Utility;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        Res res = new Res();

        private readonly IConfiguration config;
        private readonly IToken token;
        private readonly IUserService userService;
        public Auth(
            IConfiguration config,
            IToken token,
            IUserService userService
            )
        {
            this.config = config;
            this.token = token;
            this.userService = userService;
        }

        // GET: api/<Auth>
        [HttpGet, TokenValidation]
        public ActionResult<Res> Get()
        {
            string getToken = Request.Headers.TryGetValue(HeaderNames.Authorization, out var tokenString) == true ?
                tokenString.FirstOrDefault().Replace("Bearer ", "") : "";

            try
            {
                res.Status = true;
                res.Message = ActionStatus.Success;
                res.Data = userService.User(token.GetUserIdFromToken(getToken));
                return StatusCode((int)StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = ex.Message;
                return StatusCode((int)StatusCodes.Status500InternalServerError, res);
            }

        }

        // GET api/<Auth>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Auth>
        [HttpPost]
        public ActionResult<Res> Post([FromBody] LoginModelReq loginModelReq)
        {
            try
            {
                string status = userService.AuthenticatedUser(loginModelReq.UserName, loginModelReq.Password);

                if (status == ActionStatus.Success)
                {
                    res.Message = ActionStatus.Success;
                    res.Data = new LoginModelRes()
                    {
                        Token = token.GenerateToken(loginModelReq.UserName),
                        Expiry = DateTime.UtcNow.AddDays(2)
                    };

                    return StatusCode((int)StatusCodes.Status200OK, res);
                }
                else
                {
                    res.Status = false;
                    res.Message = status;
                    return StatusCode((int)StatusCodes.Status200OK, res);
                }


            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = ex.Message;
                return StatusCode((int)StatusCodes.Status500InternalServerError, res);
            }

        }

        // PUT api/<Auth>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] LoginModelReq loginModelReq)
        {

        }

        // DELETE api/<Auth>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
