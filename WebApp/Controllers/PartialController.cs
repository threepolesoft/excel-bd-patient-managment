using Common.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace WebApp.Controllers
{
    public class PartialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DiseaseList()
        {

            List<DiseaseInformationModel> list = new List<DiseaseInformationModel>();

            try
            {
                string url = string.Format("{0}/api/Disease", Utils.BaseUrl);

                HttpClient httpClient = new HttpClient();

                ClaimsPrincipal user = HttpContext.User;

                // Retrieve the SerialNumber claim
                string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                // Add any custom headers
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<Res>();

                    if (response.Status)
                    {
                        //Save the token, decrypt and get user claims and save to secured storage.

                        list = JsonSerializer.Deserialize<List<DiseaseInformationModel>>(response.Data.ToString());

                    }
                    else
                    {
                        Console.WriteLine(response.Message);

                    }

                }
                else
                {
                    Console.WriteLine(result.ReasonPhrase.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return PartialView(list);
        }

        [HttpGet]
        public async Task<IActionResult> NcdList()
        {

            List<NCDModel> list = new List<NCDModel>();

            try
            {
                string url = string.Format("{0}/api/NCD", Utils.BaseUrl);

                HttpClient httpClient = new HttpClient();

                ClaimsPrincipal user = HttpContext.User;

                // Retrieve the SerialNumber claim
                string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                // Add any custom headers
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<Res>();

                    if (response.Status)
                    {
                        //Save the token, decrypt and get user claims and save to secured storage.

                        list = JsonSerializer.Deserialize<List<NCDModel>>(response.Data.ToString());

                    }
                    else
                    {
                        Console.WriteLine(response.Message);

                    }

                }
                else
                {
                    Console.WriteLine(result.ReasonPhrase.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return PartialView(list);
        }

        [HttpGet]
        public async Task<IActionResult> AllergiesList()
        {

            List<AllergiesModel> list = new List<AllergiesModel>();

            try
            {
                string url = string.Format("{0}/api/Allergies", Utils.BaseUrl);

                HttpClient httpClient = new HttpClient();

                ClaimsPrincipal user = HttpContext.User;

                // Retrieve the SerialNumber claim
                string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                // Add any custom headers
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<Res>();

                    if (response.Status)
                    {
                        //Save the token, decrypt and get user claims and save to secured storage.

                        list = JsonSerializer.Deserialize<List<AllergiesModel>>(response.Data.ToString());

                    }
                    else
                    {
                        Console.WriteLine(response.Message);

                    }

                }
                else
                {
                    Console.WriteLine(result.ReasonPhrase.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return PartialView(list);
        }

        [HttpGet]
        public async Task<IActionResult> PatientList()
        {

            List<PatientsModel> list = new List<PatientsModel>();

            try
            {
                string url = string.Format("{0}/api/Patient", Utils.BaseUrl);

                HttpClient httpClient = new HttpClient();

                ClaimsPrincipal user = HttpContext.User;

                // Retrieve the SerialNumber claim
                string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                // Add any custom headers
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<Res>();

                    if (response.Status)
                    {
                        //Save the token, decrypt and get user claims and save to secured storage.

                        list = JsonSerializer.Deserialize<List<PatientsModel>>(response.Data.ToString());

                    }
                    else
                    {
                        Console.WriteLine(response.Message);

                    }

                }
                else
                {
                    Console.WriteLine(result.ReasonPhrase.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return PartialView(list);
        }

        [HttpGet]
        public async Task<IActionResult> PatientsDetails(long id)
        {

           PatientDetailsModel list = new PatientDetailsModel();

            try
            {
                string url = string.Format("{0}/api/Patient/{1}", Utils.BaseUrl, id);

                HttpClient httpClient = new HttpClient();

                ClaimsPrincipal user = HttpContext.User;

                // Retrieve the SerialNumber claim
                string serialNumberClaim = user.FindFirst(ClaimTypes.SerialNumber).Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + serialNumberClaim);

                // Add any custom headers
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<Res>();

                    if (response.Status)
                    {
                        //Save the token, decrypt and get user claims and save to secured storage.

                        list = JsonSerializer.Deserialize<PatientDetailsModel>(response.Data.ToString());

                    }
                    else
                    {
                        Console.WriteLine(response.Message);

                    }

                }
                else
                {
                    Console.WriteLine(result.ReasonPhrase.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return PartialView(list);
        }
    }
}
