using Microsoft.Extensions.Options;
using Nhwts.Model;
using Nhwts.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Nhwts.Repository.Implementation
{
    public class StateRepository : IStateRepository
    {
        private readonly AppSetting _appSetting;

        public StateRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value ?? throw new ArgumentNullException(nameof(appSetting), "AppSetting cannot be null");
        }

        public async Task<List<States>> GetStates()
        {
            try
            {
                List<States> stateList = new List<States>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appSetting.WebApiLink ?? throw new ArgumentNullException(nameof(_appSetting.WebApiLink), "WebApiLink cannot be null"));
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("Api/State/GetState");

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = await response.Content.ReadAsStringAsync();
                        stateList = JsonConvert.DeserializeObject<List<States>>(readTask);
                    }
                }

                return stateList;
            }
            catch (Exception ex) 
            {
                throw new Exception();
            }
        }

       
    }
}
