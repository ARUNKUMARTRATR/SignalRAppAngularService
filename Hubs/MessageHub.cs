﻿namespace SignalRAppAngular.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using SignalRAppAngular.Dtos;
    using System;
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
        public async Task SendToCallerAsync(ChatbotMessageReceiverDto message)
        {
            var data = new ChatbotMessageReceiverDto
            {
                Message = "sucess",
                MessageUserType = 0,
                MessageType = 0,
                AnswerType = "file",
                IsAnswer = false,
                DateTime = DateTime.Now,
                ConnectionId = Context.ConnectionId,
                QuestionId = 1,
                IsAuto = false
            };
            await Clients.Caller.SendAsync("RecieveMsg", data);
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
