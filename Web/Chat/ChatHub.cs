using Microsoft.AspNetCore.SignalR;

namespace Web.Chat
{
    public class ChatHub : Hub
    {
        //Enviar el mensaje al usuario de la room correspondiente
        public async Task EnviarMensaje(int room, string usuario, string mensaje)
        {
            await Clients.Group(room.ToString()).SendAsync("RecibirMensaje", usuario, mensaje);
        }

        //Agrego al usuario al grupo
        public async Task AgregarAlGrupo(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }
    }
}
