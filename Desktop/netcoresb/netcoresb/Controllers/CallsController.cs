using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;



namespace openbanknetcore.Controllers
{
    [Route("[controller]")]
    public class CallsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("createsandbox")]
        //Create Sandbox
        public async Task<IActionResult> createSandbox()
        {
            var client = new HttpClient();
            //replace sandbox_id value
            string sandbox_id = "REPLACE_SANDBOX_ID";
            Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { "sandbox_id", sandbox_id }
                };
            StringContent content = new StringContent(JsonConvert.SerializeObject(dict), Encoding.UTF8, "text/json");
            HttpResponseMessage response = await client.PostAsync("https://apis.nbg.gr/public/nbgapis/obp/v3.0.1/sandbox", content);
            var responseText = await response.Content.ReadAsStringAsync();
            client.DefaultRequestHeaders.Accept.Clear();
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            var obj = JsonConvert.DeserializeObject(responseText);
            return Ok(obj);


        }

        [HttpGet("getbanks")]
        public async Task<IActionResult> getBanks()
        {
            //Get a list of banks supported for current application ID.
            var client = new HttpClient();
            //replace sandbox_id with the id of an existing sandbox
            Uri uri = new Uri("https://apis.nbg.gr/public/nbgapis/obp/v3.0.1/banks");
            client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "text/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("content-type", "text/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("application_id", "REPLACE_THIS_VALUE");
            client.DefaultRequestHeaders.TryAddWithoutValidation("provider", "REPLACE_THIS_VALUE");
            client.DefaultRequestHeaders.TryAddWithoutValidation("provider_id", "REPLACE_THIS_VALUE");
            client.DefaultRequestHeaders.TryAddWithoutValidation("sandbox_id", "REPLACE_SANDBOX_ID");
            //replace user_id and username with the user id and the username of an existing user
            client.DefaultRequestHeaders.TryAddWithoutValidation("user_id", "REPLACE_THIS_VALUE");
            client.DefaultRequestHeaders.TryAddWithoutValidation("username", "REPLACE_THIS_VALUE");
            HttpResponseMessage response = await client.GetAsync(uri);
            string responseText = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(responseText);
            return Ok(obj);

        }

        [HttpGet("deleteSandbox")]
        //Delete Sandbox
        public async Task<IActionResult> deleteSandbox()
        {
            //replace sandbox_id value with the id of an existing sandbox
            string sandbox_id = "REPLACE_SANDBOX_ID";
            var client = new HttpClient();
            Uri uri = new Uri("https://apis.nbg.gr/public/nbgapis/obp/v3.0.1/sandbox/" + sandbox_id);
            HttpResponseMessage response = await client.DeleteAsync(uri);
            string responseText = await response.Content.ReadAsStringAsync();
            return Ok(responseText);
        }
    }
}
