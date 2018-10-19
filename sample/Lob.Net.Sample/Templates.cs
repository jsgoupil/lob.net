using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class Templates
    {
        private readonly ILobTemplates lobTemplates;

        public Templates(
            ILobTemplates lobTemplates
        )
        {
            this.lobTemplates = lobTemplates;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobTemplates.CreateAsync(new TemplateRequest
                {
                    Description = "My Template",
                    Html = "<html>hello"
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var result2 = await lobTemplates.CreateVersionAsync(result1.Id, new TemplateVersionRequest
                {
                    Description = "My Version",
                    Html = "<html>hello2"
                });

                var str2 = JsonConvert.SerializeObject(result2);
                Console.WriteLine(str2);

                var result3 = await lobTemplates.UpdateAsync(result1.Id, new TemplateUpdate
                {
                    PublishedVersion = result2.Id,
                    Description = "Other Description"
                });

                var str3 = JsonConvert.SerializeObject(result3);
                Console.WriteLine(str3);

                var result4 = await lobTemplates.UpdateVersionAsync(result1.Id, result2.Id, "New Description");

                var str4 = JsonConvert.SerializeObject(result4);
                Console.WriteLine(str4);

                var result5 = await lobTemplates.ListAsync();

                var str5 = JsonConvert.SerializeObject(result5);
                Console.WriteLine(str5);
            }
            catch (LobException ex)
            {
                throw ex;
            }
        }
    }
}
