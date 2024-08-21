using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Nhwts.Model;
using Nhwts.Model.DTO;
using Nhwts.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Repository.Implementation
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppSetting _appSetting;

        public LoginRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value ?? throw new ArgumentNullException(nameof(appSetting), "AppSetting cannot be null");
        }
        public async Task<NonOcmmsUserModel> GetIndustry(LoginDto login)
        {
            try
            {
                NonOcmmsUserModel userData = new NonOcmmsUserModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appSetting.WebApiLink ?? throw new ArgumentNullException(nameof(_appSetting.WebApiLink), "WebApiLink cannot be null"));
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonContent = JsonConvert.SerializeObject(login);

                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("Api/Login/UserLogin", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        userData = JsonConvert.DeserializeObject<NonOcmmsUserModel>(jsonResponse);
                    }
                    else
                    {
                        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                    }
                }

                return userData;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<IndustryRegistration> SignupIndustry(IndustryRegistration details)
        {
            try
            {
                IndustryRegistration userData = new IndustryRegistration();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appSetting.WebApiLink ?? throw new ArgumentNullException(nameof(_appSetting.WebApiLink), "WebApiLink cannot be null"));
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    details.FileName = _appSetting.DocsLink+ details.FileName;
                     var jsonContent = JsonConvert.SerializeObject(details);

                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("Api/Login/SignupIndustry", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        userData = JsonConvert.DeserializeObject<IndustryRegistration>(jsonResponse);
                    }
                    else
                    {
                        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                    }
                }

                return userData;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
