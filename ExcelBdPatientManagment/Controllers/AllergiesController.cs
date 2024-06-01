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
    public class AllergiesController : ControllerBase
    {
        Res res = new Res();

        private readonly IConfiguration config;
        private readonly IToken token;
        private readonly IUserService userService;
        private readonly IAllergies _allergies;
        public AllergiesController(
            IConfiguration config,
            IToken token,
            IUserService userService,
            IAllergies allergies
            )
        {
            this.config = config;
            this.token = token;
            this.userService = userService;
            this._allergies = allergies;
        }

        // GET: api/<AllergiesController>
        [HttpGet, TokenValidation]
        public ActionResult<Res> Get()
        {

            try
            {

                res.Status = true;
                res.Message = ActionStatus.Success;
                res.Data = _allergies.GetAll();
                return StatusCode((int)StatusCodes.Status200OK, res);


            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = ex.Message;
                return StatusCode((int)StatusCodes.Status500InternalServerError, res);
            }
        }

        // GET api/<AllergiesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AllergiesController>
        [HttpPost, TokenValidation]
        public ActionResult<Res> Post([FromBody] AllergiesReqModel allergiesReqModel)
        {
            string getToken = Request.Headers.TryGetValue(HeaderNames.Authorization, out var tokenString) == true ?
                                tokenString.FirstOrDefault().Replace("Bearer ", "") : "";

            try
            {
                ApplicationUser applicationUser = userService.UserByUserName(token.GetUserIdFromToken(getToken));

                string status = _allergies.Save(new Allergies
                {
                    ID = allergiesReqModel.ID,
                    Name = allergiesReqModel.Name,
                    EntryDate = DateTime.Now,
                    EntryUser = applicationUser.ID,
                    UpdateDate = DateTime.Now,
                    UpdateUser = applicationUser.ID,
                });

                if (status == ActionStatus.Success)
                {
                    res.Status = true;
                    res.Message = string.Format("Allergies save success");
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

        // PUT api/<AllergiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AllergiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _allergies.Delete(id);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
