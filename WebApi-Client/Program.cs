using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApi_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63680/Person");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Run(client);

            Console.ReadKey();
        }

        static async void Run(HttpClient client)
        {
            bool result = await AddPerson(client);
            Console.WriteLine($"添加结果：{ result }");
            Person person = await GetPerson(client);
            Console.WriteLine($"查询结果：{ person }");
            result = await PutPerson(client);
            Console.WriteLine($"更新结果：{ result }");
            result = await DeletePerson(client);
            Console.WriteLine($"删除结果：{ result }");
        }

        static async Task<bool> AddPerson(HttpClient client)
        {
            return await client.PostAsJsonAsync("Person", new Person() { ID = 5, Name = "曹操", Age = 22 }).ContinueWith(x => x.Result.IsSuccessStatusCode);
        }

        static async Task<Person> GetPerson(HttpClient client)
        {
            return await await client.GetAsync("Person/1").ContinueWith(x => x.Result.Content.ReadAsAsync<Person>(new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter(), new XmlMediaTypeFormatter() }));
        }

        static async Task<bool> PutPerson(HttpClient client)
        {
            return await client.PutAsJsonAsync("Person/2", new Person { ID = 2, Name = "吕布", Age = 33 }).ContinueWith(x => x.Result.IsSuccessStatusCode);
        }

        static async Task<bool> DeletePerson(HttpClient client)
        {
            return await client.DeleteAsync("Person/1").ContinueWith(x => x.Result.IsSuccessStatusCode);
        }
        
        public class Person
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
