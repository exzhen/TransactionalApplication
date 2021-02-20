using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AccountService.Models;

namespace AccountService
{
    public class ApiCall
    {
        static readonly HttpClient client = new HttpClient();

        static async Task SimpleHttpCall()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Console.WriteLine(responseBody);
        }

        //public static Queue<int> requests = new Queue<int>();
    }
}
