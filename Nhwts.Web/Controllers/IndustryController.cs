using Microsoft.AspNetCore.Mvc;
using Nhwts.Model;
using Nhwts.Model.DTO;
using Nhwts.Repository.Contract;
using Nhwts.Repository.Implementation;
using Nhwts.Web.Models;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Nhwts.Web.Controllers
{
    public class IndustryController : Controller
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILoginRepository _loginRepository;

        public IndustryController(IStateRepository stateRepository,ILoginRepository loginRepository)
        {

            _stateRepository = stateRepository;
            _loginRepository = loginRepository;

        }
        public IActionResult Industry()
        {
            // industry page.
            //===========================ss
            ViewBag.Title = "Industry Page";
            return View();
        }

      
        public async Task<IActionResult> IndustryRegistration()
        {
            // industry page.
            var states = await _stateRepository.GetStates();
            var viewModel = new IndustryRegistration
            {
                States = states
            };
            //===========================
            ViewBag.Title = "Industry Page";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Get_Districts([FromBody] States data)
        {
            try
            {
                if (data.Stcode != null)
                {
                    var districts = await _stateRepository.GetDistricts(data);
                    var viewModel = new IndustryRegistration
                    {
                        States = districts
                    };
                    return Json(viewModel);
                }
                // RedirectToAction("Industry", "Industry");
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignupIndustry(IndustryRegistration signupdetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (signupdetails != null)
                    {


                        if (signupdetails.Username != null && signupdetails.Password != null)
                        {
                            var md5pass = GetMD5Hash(signupdetails.Password);
                            signupdetails.Password = md5pass;
                            //signupdetails.Dtcode = "603";
                            if (signupdetails.FormFile != null)
                            {
                                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                                //create folder if not exist
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);

                                //get file extension
                                FileInfo fileInfo = new FileInfo(signupdetails.FormFile.FileName);
                                //string fileName =signupdetails.FormFile.FileName + fileInfo.Extension;

                                string fileName = DateTime.Now.ToString("yyyyMMddTHHmmss") + "_" + signupdetails.FormFile.FileName;

                                string fileNameWithPath = Path.Combine(path + "/authorizationpdf/", fileName);

                                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                                {
                                    signupdetails.FormFile.CopyTo(stream);
                                }
                                //var pdflink = "https://nhwts.nic.in/nhwtsdocs/authorizationpdf/" + newauthno + ".pdf";
                                signupdetails.FileName = "authorizationpdf/" + fileName;
                            }
                            signupdetails = await _loginRepository.SignupIndustry(signupdetails);
                            signupdetails.IsSuccess = true;
                            signupdetails.Message = "Registeration request sent to CPCB for approval..";
                        }
                    }
                    // RedirectToAction("Industry", "Industry");
                    return Json(signupdetails);
                    /*return Json(new
                    {
                        signupdetails
                    });*/
                }
                else
                {
                    signupdetails.IsSuccess = false;
                    signupdetails.Message = "Fill the All Fields";
                    return Json(signupdetails);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
