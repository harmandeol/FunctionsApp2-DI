using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp2
{
    public class Function1
    {
        public Function1(IScoped1 scoped1)
        {

        }
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */5 * * * *",RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }


    }


}
