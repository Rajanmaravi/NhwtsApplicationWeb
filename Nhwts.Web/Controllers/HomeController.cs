using Microsoft.AspNetCore.Mvc;
using Nhwts.Model;
using Nhwts.Model.DTO;
using Nhwts.Repository.Contract;
using Nhwts.Web.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Nhwts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStateRepository _stateRepository;
        private readonly ICaptchaRepository _captchaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoginRepository _loginRepository;
        public HomeController(ILogger<HomeController> logger, IStateRepository stateRepository, 
            ICaptchaRepository captchaRepository, IHttpContextAccessor httpContextAccessor, ILoginRepository loginRepository)
        {
            _logger = logger;
            _stateRepository = stateRepository;
            _captchaRepository = captchaRepository;
            _httpContextAccessor = httpContextAccessor;
            _loginRepository = loginRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {   
            var states =await _stateRepository.GetStates();
            var viewModel = new StateViewModel
            {
                States = states
            };

            var captchaImage = await _captchaRepository.GenerateCaptchaImageAsync();

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                httpContext.Session.SetString("CaptchaCode", captchaImage);
            }

            ViewBag.CaptchaImage = $"data:image/png;base64,{captchaImage}";

            return View(viewModel);
        }

        [HttpGet("RefreshCaptcha")]
        public async Task<IActionResult> RefreshCaptcha()
        {
            try
            {
                var captchaImage = await _captchaRepository.GenerateCaptchaImageAsync();
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext != null)
                {
                    httpContext.Session.SetString("CaptchaCode", captchaImage);
                }

                return Json(new { image = $"data:image/png;base64,{captchaImage}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating CAPTCHA.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginForNonOcmmsAndOcmmsUser([FromBody] LoginDto login)
        {
            try
            {
                //login 
                var md5pass = GetMD5Hash(login.password).ToLower();
                var pass = string.Empty;

                NonOcmmsUserModel? result = null;

                if (login.loginRadio == "industry")
                {
                     result = await _loginRepository.GetIndustry(login);
                    if (result != null)
                    {
                        pass = result.Password;
                    }
                    else
                    {
                        throw new Exception("No industry user found.");
                    }
                }
               
                else if ("usertypedd.SelectedValue" == "SPCB")
                {
                  //  usertable = System.Configuration.ConfigurationManager.AppSettings["spcb_usertable"];
                  //  sqlQuery = "SELECT * FROM \"" + usertable + "\"  WHERE \"username\"='" + usernametextbox.Text.ToString().ToLower() + "' and \"state\"='" + userstatedd.SelectedValue + "'";
                }             
                else if ("usertypedd.SelectedValue" == "Transporter")
                {
                   // usertable = System.Configuration.ConfigurationManager.AppSettings["usertable"];
                   // sqlQuery = "SELECT * FROM \"" + usertable + "\"  WHERE \"username\"='" + usernametextbox.Text.ToString().ToLower() + "' and \"state\"='" + userstatedd.SelectedValue + "' and \"category\"='" + usertypedd.SelectedValue + "'";
                }
                else if ("usertypedd.SelectedValue" == "CPCB")
                {
                  //  usertable = System.Configuration.ConfigurationManager.AppSettings["usertable"];
                  //  sqlQuery = "SELECT * FROM \"" + usertable + "\"  WHERE \"username\"='" + usernametextbox.Text.ToString().ToLower() + "'  and \"category\"='" + usertypedd.SelectedValue + "'";
                }
                else if ("usertypedd.SelectedValue" == "Admin")
                {
                    //usertable = System.Configuration.ConfigurationManager.AppSettings["usertable"];
                   // sqlQuery = "SELECT * FROM \"" + usertable + "\"  WHERE \"username\"='" + usernametextbox.Text.ToString().ToLower() + "'  and \"category\"='" + usertypedd.SelectedValue + "'";
                }

                

               

                if (md5pass == pass.ToLower())
                {
                    if (login.loginRadio == "industry")
                    {
                        RedirectToAction("Industry", "Industry");
                    }
                    else if ("usertypedd.SelectedValue" == "SPCB")
                                Response.Redirect("spcbpage.aspx");
                            else if ("usertypedd.SelectedValue" == "CPCB")
                                Response.Redirect("cpcbpage.aspx");
                            else if ("usertypedd.SelectedValue" == "Admin")
                                Response.Redirect("adminpage.aspx");

                }

               return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult ValidateCaptcha(string captchaInput)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var sessionCaptcha = httpContext?.Session.GetString("CaptchaCode");
            if (ValidateCaptchaImage(captchaInput, sessionCaptcha))
            {
                return RedirectToAction("Success");
            }

            ModelState.AddModelError("Captcha", "Invalid CAPTCHA.");
            return View("Index");
        }

        public bool ValidateCaptchaImage(string captchaInput, string sessionCaptcha)
        {
            return captchaInput != null && captchaInput.Equals(sessionCaptcha, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetMD5Hash(string theInput)
        {
            using (MD5 hasher = MD5.Create())    // create hash object
            {
                byte[] dbytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput));

                StringBuilder sBuilder = new StringBuilder();
                for (int n = 0; n <= dbytes.Length - 1; n++)
                    sBuilder.Append(dbytes[n].ToString("X2"));

                return sBuilder.ToString();
            }
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
    }
}
