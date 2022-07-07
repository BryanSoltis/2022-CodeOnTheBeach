using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lab4DemoFunctions
{
    public static class DataManipulator4000Function
    {
        [FunctionName("DataManipulator4000Function")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string response = null;
            log.LogInformation("DataManipulator4000Function processed a request.");

            try
            {
                string body = await new StreamReader(req.Body).ReadToEndAsync();
                char[] arr = body.ToCharArray();
                Array.Reverse(arr);
                foreach (char c in arr)
                {
                    if (char.IsUpper(c))
                    {
                        response += char.ToLower(c).ToString();
                    }
                    else
                    {
                        response += char.ToUpper(c).ToString();
                    }
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
