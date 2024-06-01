using Common.Models;
using Common.Models.DbSet;
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
        public async Task<IActionResult> NcdList()
        {

            List<NCD> list = new List<NCD>();

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

                        list = JsonSerializer.Deserialize<List<NCD>>(response.Data.ToString());

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

            List<Allergies> list = new List<Allergies>();

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

                        list = JsonSerializer.Deserialize<List<Allergies>>(response.Data.ToString());

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
