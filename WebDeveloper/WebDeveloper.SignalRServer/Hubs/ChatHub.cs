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
            // a este Hub. Vamos a envair el nuevo ID de conexion como parametro
            await Clients.Others.SendAsync("NuevaConexion", Context.ConnectionId); // , mensajePrevios
        }

        public async Task TransmitirMensaje(string mensaje)
        {
            await Clients.All.SendAsync("RecibirMensaje", mensaje);
            // _chinokContext.Messages.Add(new Message { mensaje, Context.ConnectionId })
        }
    }
}
