using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KursovayaBlazorNet6.ServerHub
{

    
    public static class Message 
    {
       public static string Receive = "ReceiveMessage";
       public static string Register = "Register";
       public static string Send = "Send";
    }
}
