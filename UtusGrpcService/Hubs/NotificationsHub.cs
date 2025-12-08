using Microsoft.AspNetCore.SignalR;

namespace UtusGrpcService.Hubs
{
    public class NotificationsHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}


