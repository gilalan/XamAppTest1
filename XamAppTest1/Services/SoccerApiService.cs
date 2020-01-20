using Newtonsoft.Json;
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
					string content = response.Content.ReadAsStringAsync().Result;
					//dynamic result = JsonConvert.DeserializeObject(content);
					//var job = new Job();
					//job.Specification = result.data.results[0].especificacao;
					//job.FemaleName = result.data.results[0].nomeFeminino;
					//job.Description = result.data.results[0].descricao;
					//job.CBO = result.data.results[0].cbo;
					//jobs.Add(job);
					var Items = JsonConvert.DeserializeObject<List<Job>>(content);
					foreach (var item in Items)
					{
						Debug.WriteLine(item.Specification);
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
