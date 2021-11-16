using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.SignalRServer.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            // Transmitir un mensaje a todos los demas clientes que estan previamente conectados
            // a este Hub
            await Clients.Others.SendAsync("NuevaConexion");
        }
    }
}
