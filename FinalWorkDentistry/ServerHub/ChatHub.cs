using Microsoft.AspNetCore.SignalR;

namespace KursovayaBlazorNet6.ServerHub
{
    public class ChatHub : Hub
    {



        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        //private static readonly Dictionary<string, string> userLookup =
        //    new Dictionary<string, string>();


        //2 вариант чата
        //public async Task SendMessage(string username, string message)
        //{
        //    await Clients.All.SendAsync(Message.Receive, username, message);


        //}


        //public void Send(string name, string message)
        //{
        //    Clients.All.addNewMessageToPage(name, message);
        //}


        //public async Task Register(string username)
        //{
        //    var currentId = Context.ConnectionId;
        //    if (!userLookup.ContainsKey(currentId))
        //    {
        //        userLookup.Add(currentId, username);
        //        await Clients.AllExcept(currentId).SendAsync(
        //           Message.Receive,
        //           username, $"{username} joined the chat"
        //            );
        //    }
        //}

        //public override Task OnConnectedAsync()
        //{
        //    Console.WriteLine("Connected"); ;
        //    return base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception e)
        //{
        //    Console.WriteLine($"Disconnect {e.Message}");

        //    string id = Context.ConnectionId;
        //    if(!userLookup.TryGetValue(id, out string username))
        //    {
        //        username = "[unknown]";
        //    }

        //    userLookup.Remove(id);
        //    await Clients.AllExcept(Context.ConnectionId).SendAsync(
        //        Message.Receive,
        //        username, $"{username} has left the chat"
        //        );
        //    await base.OnDisconnectedAsync(e);
        //}

    }
}
