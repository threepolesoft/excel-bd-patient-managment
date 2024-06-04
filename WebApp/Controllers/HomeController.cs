using Common.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public string message = "Process executed with error!";
        public string error = "Process executed with error!";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region NCD

        [HttpPost]
        public async Task<IActionResult> NcdSave([FromBody] NCDReqModel nCDReqModel)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    string url = string.Format("{0}/api/NCD", Utils.BaseUrl);

                    HttpClient httpClient = new HttpClient();

                    ClaimsPrincipal user = HttpContext.User;

                    // Retrieve the SerialNumber claim
                    string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, nCDReqModel);
                    if (result.IsSuccessStatusCode)
                    {

                        var response = await result.Content.ReadFromJsonAsync<Res>();
                        message = response.Message;

                    }
                    else
                    {
                        error = result.ReasonPhrase.ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return Json(new
            {
                message = message,
                error = message == "Process executed with error!" ? error : "",
                data = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    string url = string.Format("{0}/api/NCD/{1}", Utils.BaseUrl, id);

                    HttpClient httpClient = new HttpClient();

                    ClaimsPrincipal user = HttpContext.User;

                    // Retrieve the SerialNumber claim
                    string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.DeleteAsync(url);
                    if (result.IsSuccessStatusCode)
                    {

                        message = "NCD delete success";

                    }
                    else
                    {
                        error = result.ReasonPhrase.ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return Json(new
            {
                message = message,
                error = message == "Process executed with error!" ? error : "",
                data = ""
            });
        }
        #endregion

        #region Allergies
        [HttpPost]
        public async Task<IActionResult> AllergiesSave([FromBody] AllergiesReqModel allergiesReqModel)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    string url = string.Format("{0}/api/Allergies", Utils.BaseUrl);

                    HttpClient httpClient = new HttpClient();

                    ClaimsPrincipal user = HttpContext.User;

                    // Retrieve the SerialNumber claim
                    string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, allergiesReqModel);
                    if (result.IsSuccessStatusCode)
                    {

                        var response = await result.Content.ReadFromJsonAsync<Res>();
                        message = response.Message;

                    }
                    else
                    {
                        error = result.ReasonPhrase.ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return Json(new
            {
                message = message,
                error = message == "Process executed with error!" ? error : "",
                data = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> AllergiesDelete(int id)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    string url = string.Format("{0}/api/Allergies/{1}", Utils.BaseUrl, id);

                    HttpClient httpClient = new HttpClient();

                    ClaimsPrincipal user = HttpContext.User;

                    // Retrieve the SerialNumber claim
                    string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.DeleteAsync(url);
                    if (result.IsSuccessStatusCode)
                    {

                        message = "NCD delete success";

                    }
                    else
                    {
                        error = result.ReasonPhrase.ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return Json(new
            {
                message = message,
                error = message == "Process executed with error!" ? error : "",
                data = ""
            });
        }
        #endregion


        #region Patients

        [HttpGet]
        public IActionResult PatientEntry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PatientSave([FromBody] PatientsModel patientsModel)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    string url = string.Format("{0}/api/Patient", Utils.BaseUrl);

                    HttpClient httpClient = new HttpClient();

                    ClaimsPrincipal user = HttpContext.User;

                    // Retrieve the SerialNumber claim
                    string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, patientsModel);
                    if (result.IsSuccessStatusCode)
                    {

                        var response = await result.Content.ReadFromJsonAsync<Res>();
                        message = response.Message;

                    }
                    else
                    {
                        error = result.ReasonPhrase.ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return Json(new
            {
                message = message,
                error = message == "Process executed with error!" ? error : "",
                data = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> PatientsDelete(int id)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    string url = string.Format("{0}/api/Allergies/{1}", Utils.BaseUrl, id);

                    HttpClient httpClient = new HttpClient();

                    ClaimsPrincipal user = HttpContext.User;

                    // Retrieve the SerialNumber claim
                    string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.DeleteAsync(url);
                    if (result.IsSuccessStatusCode)
                    {

                        message = "NCD delete success";

                    }
                    else
                    {
                        error = result.ReasonPhrase.ToString();
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return Json(new
            {
                message = message,
                error = message == "Process executed with error!" ? error : "",
                data = ""
            });
        }
        #endregion
    }
}
