using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KursovayaBlazorNet6.ServerHub
{
    [AllowAnonymous]
    public class Chat : Hub
    {
        // метод для отправки сообщений
        // всем ПОДКЛЮЧЕННЫМ КЛИЕНТАМ!
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }
    }
}
