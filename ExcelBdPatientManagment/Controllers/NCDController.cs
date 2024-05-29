using API.Repository.Interface;
using API.Utility;
using Common.Models;
using Common.Models.DbSet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NCDController : ControllerBase
    {
        Res res = new Res();

        private readonly IConfiguration config;
        private readonly IToken token;
        private readonly IUserService userService;
        private readonly INCD nCD;
        public NCDController(
            IConfiguration config,
            IToken token,
            IUserService userService,
            INCD nCD
            )
        {
            this.config = config;
            this.token = token;
            this.userService = userService;
            this.nCD = nCD;
        }

        // GET: api/<NCDController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NCDController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NCDController>
        [HttpPost, TokenValidation]
        public ActionResult<Res> Post([FromBody] NCDReqModel nCDReqModel)
        {
            string getToken = Request.Headers.TryGetValue(HeaderNames.Authorization, out var tokenString) == true ?
                                tokenString.FirstOrDefault().Replace("Bearer ", "") : "";

            try
            {
                ApplicationUser applicationUser = userService.UserByUserName(token.GetUserIdFromToken(getToken));

                string status = nCD.Save(new NCD
                {
                    ID = nCDReqModel.ID,
                    Name = nCDReqModel.Name,
                    EntryDate = DateTime.Now,
                    EntryUser = applicationUser.ID,
                    UpdateDate = DateTime.Now,
                    UpdateUser = applicationUser.ID,
                });

                if (status == ActionStatus.Success)
                {
                    res.Status = true;
                    res.Message = ActionStatus.Success;
                    res.Data = null;
                    return StatusCode((int)StatusCodes.Status200OK, res);
                }
                else
                {
                    res.Status = false;
                    res.Message = status;
                    res.Data = null;
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

        // PUT api/<NCDController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NCDController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
