using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class BiodataController : ControllerBase
    {
        private readonly IMapper _mapper;

        public BiodataController(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


        public async Task<IActionResult> Get()
        {
            string baseUrl = "https://bio.tools/api/tool/?biotoolsID=CytoNorm&format=json";
            //Have your using statements within a try/catch block
            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            //Now assign your content to your data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                            if (data != null)
                            {
                                var results = JObject.Parse(data).Property("list").First.First;
                                BiodataModel[] bios = _mapper.Map<BiodataModel[]>(results);
                                //Now log your data in the console
                                Console.WriteLine("data------------{0}", results);
                                return Ok(bios);
                            }
                            else
                            {
                                Console.WriteLine("NO Data----------");
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
                return BadRequest();
            }
        }
    }
}