using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRVue.Models.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRVue.Hubs
{
    public class NotifyHub : Hub
    {
        private static ConcurrentBag<TaskModel> _tasks = new ConcurrentBag<TaskModel>();
        public async Task AddTask(object taskItem)
        {
            TaskModel item = JsonConvert.DeserializeObject<TaskModel>(((JsonElement)taskItem).ToString());

            _tasks.Add(item);
            
            #pragma warning disable CS4014
            Task.Factory.StartNew(DoTasks);

            await Clients.All.SendAsync("AddedTask", taskItem);
        }

        public async Task TaskDone(object taskItem)
        {
            await Clients.All.SendAsync("TaskDone", taskItem);
        }

        private void DoTasks()
        {
            _tasks.ToList().ForEach(x =>
            {
                Thread.Sleep(1000 * RandomNumber(1, 10));
                SignalRVue.Models.Tasks.Notifier.NotifyDone(x);
            });
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
