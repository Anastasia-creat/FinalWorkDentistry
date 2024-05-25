using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Smart.Blazor;
using Syncfusion.Blazor.Notifications;
using System.Security.Policy;
using System.Xml.XPath;

namespace KursovayaBlazorNet6.ServerHub
{
    public class ChatClient : IAsyncDisposable
    {
        public const string hubURL = "/ChatHub";
        private readonly NavigationManager _navigationManager;
        private readonly string _username;

        private bool _started = false;

        private HubConnection _hubConnection;

        public ChatClient(string username, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _username = username;
        }

        public async Task StartAsync()
        {
            if (!_started)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_navigationManager.ToAbsoluteUri(hubURL))
                    .Build();

                Console.WriteLine("Chat start");

                _hubConnection.On<string, string>(Message.Receive, (user, message) =>
                {
                    HandleReceiveMessage(user, message);
                });

                await _hubConnection.StartAsync();
                Console.WriteLine("Chat return");

                _started = true;

                await _hubConnection.SendAsync(Message.Register, _username);
            }
        }

        private void HandleReceiveMessage(string username, string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(username, message));
        }

        public event MessageReceivedEventHandler MessageReceived;
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

        public async Task SendAsync(string message)
        {
            if (!_started)
            {
                throw new InvalidOperationException("Client not started");
            }
            await _hubConnection.SendAsync(Message.Send, _username, message);
        }
        public async Task StopAsync()
        {
            if (!_started)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _started = false;

            }
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("ChatClient: Dispose");
            await StopAsync();
        }

    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public MessageReceivedEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }
    }

}
