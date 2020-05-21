using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRVue.Models.Tasks
{
    public static class Notifier
    {
        public async static void NotifyDone(TaskModel task)
        {
            HubConnection connection;
            connection = new HubConnectionBuilder().WithUrl("https://localhost:44321/notify").Build();

            await connection.StartAsync();
            task.Done = true;

            await connection.InvokeAsync("TaskDone", task);
        }
    }
}
