using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RISTExamOnlineProject.Hubs
{
    public class CounterHub : Hub
    {
        private static int _count;

        public override Task OnConnectedAsync()
        {
            _count++;
            base.OnConnectedAsync();
            Clients.All.SendAsync("updateCount", _count);
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _count--;
            base.OnDisconnectedAsync(exception);
            Clients.All.SendAsync("updateCount", _count);
            return Task.CompletedTask;
        }
    }
}