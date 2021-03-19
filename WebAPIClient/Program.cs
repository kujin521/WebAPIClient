using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        //此对象负责处理请求和响应
        private static readonly HttpClient _client = new HttpClient();
        
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        
        /// <summary>
        /// 创建一个用于web请求的方法
        /// </summary>
        /// <returns></returns>
        private static async Task ProcessRepositories()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _client.DefaultRequestHeaders.Add("User-Agent",".NET Foundation Repository Reporter");
            
           // await test1();
           
           var streamTask = _client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
           var repositories = await JsonSerializer.DeserializeAsync<List<Pepository>>(await streamTask);
           
           foreach (var repo in repositories)
               Console.WriteLine(repo.name);
        }

        private static async Task test1()
        {
            var stringTask = _client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.WriteLine(msg);
        }
    }
}