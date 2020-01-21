using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XamAppTest1.Helpers;
using XamAppTest1.Models;

namespace XamAppTest1.Services
{
    public class SoccerApiService
    {
        public async Task<User> GetUserAsync()
        {
            User user = new User();
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "login");
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return user;
        }
        
		public async Task<List<Job>> GetJobsAsync()
		{
			List<Job> jobs = new List<Job>();
			
			try
			{
				var httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				string ts = DateTime.Now.Ticks.ToString();

				var response = await httpClient.GetAsync(Constants.ApiBaseUrl + "cargos");
				if (response.IsSuccessStatusCode)
				{
                    using(HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync();
                        //jobs = JsonConvert.DeserializeObject<List<Job>>(json.Result);
                        //foreach (Job job in jobs)
                        //{
                        //    Debug.WriteLine(job.CBO);
                        //}
                        //dynamic result = JsonConvert.DeserializeObject(json.Result);
                        JArray resultArray = (JArray) JsonConvert.DeserializeObject(json.Result);
                        var job = new Job();
                        foreach (JObject item in resultArray)
                        {
                            job = new Job();
                            job.CBO = (item.GetValue("cbo") != null) ? item.GetValue("cbo").Value<string>() : "";
                            job.Description = (item.GetValue("descricao") != null) ? item.GetValue("descricao").Value<string>() : "";
                            job.FemaleName = (item.GetValue("nomeFeminino") != null) ? item.GetValue("nomeFeminino").Value<string>() : "";
                            job.Specification = item.GetValue("especificacao") != null ? item.GetValue("especificacao").Value<string>() : "";
                            jobs.Add(job);
                        }
                        //foreach (dynamic item in result.data.results)
                        //{
                        //    job.Specification = item.especificacao;
                        //    job.FemaleName = item.nomeFeminino;
                        //    job.Description = item.descricao;
                        //    job.CBO = item.cbo;
                        //    jobs.Add(job);
                        //}

                    }

                }
				
			}
			catch (Exception ex)
			{

				Debug.WriteLine(ex.Message);
			}

			return jobs;
		}
		
	}
		
}
