using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Common.Models;
using System.ComponentModel.Design.Serialization;
using System.Net.Http;
using System.Text.Json;

namespace WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelReq model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string url = string.Format("{0}/api/Auth", Utils.BaseUrl);

                    HttpClient httpClient = new HttpClient();

                    // Add any custom headers
                    HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, model);
                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadFromJsonAsync<Res>();

                        if (response.Status)
                        {
                            //Save the token, decrypt and get user claims and save to secured storage.

                            LoginModelRes loginModelRes = JsonSerializer.Deserialize<LoginModelRes>(response.Data.ToString());

                            var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.SerialNumber, loginModelRes.Token)
                                };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            Console.WriteLine(response.Message);
                            return RedirectToAction("Index", "Authentication");
                         
                        }

                    }
                    else
                    {
                        Console.WriteLine(result.ReasonPhrase.ToString());
                        return RedirectToAction("Index", "Authentication");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index", "Authentication");
                }

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Authentication");
        }
    }
}
