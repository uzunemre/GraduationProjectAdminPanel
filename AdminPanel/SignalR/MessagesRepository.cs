using AdminPanel.Hubs;
using AdminPanel.Models;
using AdminPanel.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.SignalR
{
    public class MessagesRepository

    {

        readonly string _connString = ConfigurationManager.ConnectionStrings["GraduationProjectContext"].ConnectionString;

        public IEnumerable<OrderDTO> GetAllMessages(int id)
        {
              var messages = new List<OrderDTO>();
               using (var connection = new SqlConnection(_connString))
               {
                   connection.Open();
                   using (var command = new SqlCommand(@"SELECT [TableNumber], [OrderDetail] from [dbo].[Orders]", connection))
                   {
                       command.Notification = null;

                       var dependency = new SqlDependency(command);
                       dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                       if (connection.State == ConnectionState.Closed)
                           connection.Open();

                       var reader = command.ExecuteReader();

                       while (reader.Read())
                       {
                           messages.Add(item: new OrderDTO { table_number = (int)reader["TableNumber"], description = (string)reader["OrderDetail"]});
                       }
                   }

               }

           /* using (GraduationProjectContext db = new GraduationProjectContext())
            {
                
                List<OrderDTO> data = (from orders in db.Orders
                                       where id == orders.RestaurantId

                                       select new OrderDTO()
                                       {
                                           table_number = orders.TableNumber,
                                           description = orders.OrderDetail,



                                       }).ToList();

                // Order DTO Listesi siparişlerde gösterilecek

                return data;
            }*/




            return messages;


        }


        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
    }

   
}
