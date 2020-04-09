using Shared.Interfaces.Infrastructure;
using Shared.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Newtonsoft.Json;
using Shared.Interfaces.Models;
using Shared.Models.DomainModels;

namespace UserHandler
{
    public class HandlerFunction
    {
        private List<IMessageBase> messages;
        private IServiceProvider serviceProvider;

        public void Run()
        {
            messages = GetMessages();

            foreach (var message in messages)
            {
                switch (message.Command)
                {
                    case "Create":
                        CreateUser(message);
                        break;

                    default:
                        break;
                }
            }
        }

        public void Config()
        {
            Shared.Logic.DIHelper diHelper = new Shared.Logic.DIHelper();

            serviceProvider = diHelper.ReturnServiceProvider();
        }

        private void CreateUser(IMessageBase message)
        {
            try
            {
                var userRepo = serviceProvider.GetService<ITableRepo>();

                IUser user = JsonConvert.DeserializeObject<UserDTO>(message.Payload); 

                

                string PartitionKey = user.email;
                string RowKey = user.sub;

              //  bool created = userRepo.AddEntity(JsonConvert.SerializeObject(user), "users");
                bool created = userRepo.AddEntity(user, "users", PartitionKey, RowKey);

                if (created == true)
                {
                    var messageRepo = serviceProvider.GetService<IQueueRepo>();
                    messageRepo.DeleteMessage(message, "users");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Was thrown @ CreateUser  - Not going to remove message form the User queue - Exception {ex}");
            }
        }

        private List<IMessageBase> GetMessages()
        {
            var queRepo = serviceProvider.GetService<IQueueRepo>();
            dynamic messages = queRepo.ReadMessages("users", 25);

            foreach (var message in messages)
            {
                Console.WriteLine(message.Payload);
            }

            return messages;
        }
    }
}
