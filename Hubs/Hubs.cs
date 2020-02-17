using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace stock.Hubs
{
    [HubName("hubClient")]
    public class Hubs : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Notification(string numero,string numero_ticket)
        {
            int tailleNotif = Int32.Parse(numero) + 1;
            Clients.All.setNotifications(tailleNotif,numero_ticket);
        }
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}