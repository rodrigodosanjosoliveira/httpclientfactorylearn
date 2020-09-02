﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

using static System.Console;


namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient(); 
        public static async Task Main(string[] args) => await ProcessRepositories();

        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
            );
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos").ConfigureAwait(continueOnCapturedContext: false);
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            foreach(var repo in repositories)
                WriteLine(repo.name);
        }
    }
}
