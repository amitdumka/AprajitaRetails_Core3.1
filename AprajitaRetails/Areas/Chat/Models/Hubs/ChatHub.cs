using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AprajitaRetails.Areas.Chat.Models.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync ("receiveMessage", message);
    }

    public class ChatHubOld : Hub
    {


        public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync ("receiveMessage", message);
        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User (user).SendAsync ("ReceiveMessage", message);
            //Change the context in hub

            // and also make a user list in the view
        }




    }



}
