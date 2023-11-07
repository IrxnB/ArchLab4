using Arch4Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Arch4Client
{
    internal class TaskWebClient : ITaskSource
    {
        private HttpClient _client;

        public TaskWebClient(string baseUrl)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
        }
        public void Create(TaskEntity? task)
        {
            if(task != null)
            {
                var req = _client.PostAsJsonAsync<TaskEntity>($"{_client.BaseAddress}/", task);
                req.Wait();
            }
        }

        public void Delete(long? id)
        {
            if(id != null)
            {
                var req = _client.DeleteAsync($"{_client.BaseAddress}/{id}");
                req.Wait();
                Console.WriteLine(req);
            }
        }

        public List<TaskEntity> ReadAll()
        {
            try
            {
                return _client.GetFromJsonAsync<List<TaskEntity>>("").Result;
            }
            catch(Exception ex) { }
            return new List<TaskEntity>();
        }

        public void Update(TaskEntity? task)
        {
            if(task != null)
            {
                var req = _client.PutAsJsonAsync<TaskEntity>($"{_client.BaseAddress}/{task.Id}", task);
                req.Wait();
                Console.WriteLine($"{req} {task}");
            }
        }
    }
}
