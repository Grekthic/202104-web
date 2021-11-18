using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDeveloper.SignalRServer.Hubs
{
    public class TracksHub : Hub
    {
        public async Task NotificarRegistro(int id, string name)
        {
            await Clients.Others.SendAsync("NotificarNuevaCancion", id, name);
        }

        public async Task NuevaConsultaCantidadCanciones(int count)
        {
            await Clients.Others.SendAsync("PintarNuevaCantidadCanciones", count, DateTime.Now);
        }
    }
}
