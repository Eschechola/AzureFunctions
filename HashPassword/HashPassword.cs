using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CHLabs.Functions.Data;

namespace CHLabs.Function
{
    public static class HashPassword
    {
        [FunctionName("HashPassword")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/chlabs/functions/hashpassword")] HttpRequest request,
            ILogger log)
        {
            log.LogInformation("Http trigger has been dispareted, starting to hash password at: " + DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"));

            var salt = Environment.GetEnvironmentVariable("BCRYPT_SALT", EnvironmentVariableTarget.Process);

            try
            {
                var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
                
                var password = JsonConvert.DeserializeObject<Password>(requestBody);
                password.Hash(salt);

                return new OkObjectResult(new Result(
                    message: "Password hashed with success",
                    success: true,
                    data: password
                ));
            }
            catch(System.Exception e)
            {
                return new BadRequestObjectResult(new Result(
                    message: "A internal server error has been occured, " + e.Message,
                    success: false,
                    data: new {}
                ));
            }    
        }
    }
}
