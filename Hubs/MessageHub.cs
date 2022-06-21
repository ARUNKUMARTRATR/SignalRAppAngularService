namespace SignalRAppAngular.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Newtonsoft.Json.Linq;
    using SignalRAppAngular.Dtos;
    using SignalRAppAngular.Model;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="MessageHub" />.
    /// </summary>
    public class MessageHub : Hub
    {
        /// <summary>
        /// The SendToAllAsync.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SendToAllAsync(string message)
        {
            await Clients.All.SendAsync("RecieveMsg", message);
        }

        /// <summary>
        /// The SendToCallerAsync.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SendToCallerAsync(string message)
        {
            var type = message.GetType();
            ChatbotMessageReceiver values = new ChatbotMessageReceiver();
            values = JsonSerializer.Deserialize<ChatbotMessageReceiver>(message);
            await Clients.Caller.SendAsync("RecieveMsg", "ads");
        }

        /// <summary>
        /// The OnConnectedAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("onUserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// The OnDisconnectedAsync.
        /// </summary>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("onUserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
