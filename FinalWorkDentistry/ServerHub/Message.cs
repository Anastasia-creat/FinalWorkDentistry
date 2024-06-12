using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FinalWorkDentistry.ServerHub
{

    
    public static class Message 
    {
        public static string Receive = "ReceiveMessage";
        public static string Register = "Register";
        public static string Send = "Send";
    }
}
