using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Configuration;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdminPanel.SignalR
{
    public class OrdersHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();


        public void Hello()
        {
            Clients.All.hello();
        }


        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();
            context.Clients.All.updateMessages();
        }




    }
}