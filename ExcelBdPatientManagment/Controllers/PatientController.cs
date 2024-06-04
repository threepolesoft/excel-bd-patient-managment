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
    public class PatientController : ControllerBase
    {
        Res res = new Res();

        private readonly IConfiguration config;
        private readonly IToken token;
        private readonly IUserService userService;
        private readonly IPatient _patient;
        public PatientController(
            IConfiguration config,
            IToken token,
            IUserService userService,
            IPatient patient
            )
        {
            this.config = config;
            this.token = token;
            this.userService = userService;
            this._patient = patient;
        }

        // GET: api/<PatientController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientController>
        [HttpPost, TokenValidation]
        public ActionResult<Res> Post([FromBody] PatientsModel patientsModel)
        {
            string getToken = Request.Headers.TryGetValue(HeaderNames.Authorization, out var tokenString) == true ?
                                tokenString.FirstOrDefault().Replace("Bearer ", "") : "";

            try
            {
                ApplicationUser applicationUser = userService.UserByUserName(token.GetUserIdFromToken(getToken));

                string status = _patient.Save(new PatientsModel
                {
                    ID = patientsModel.ID,
                    PatientName = patientsModel.PatientName,
                    DiseaseInformationID = patientsModel.DiseaseInformationID,
                    OthersNCDs = patientsModel.OthersNCDs,
                    Allergies = patientsModel.Allergies,
                    Epilepsy = patientsModel.Epilepsy,
                    EntryDate = DateTime.Now,
                    EntryUser = applicationUser.ID,
                    UpdateDate = DateTime.Now,
                    UpdateUser = applicationUser.ID,
                });

                if (status == ActionStatus.Success)
                {
                    res.Status = true;
                    res.Message = string.Format("NCD save success");
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

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
