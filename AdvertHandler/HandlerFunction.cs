﻿using Shared.Interfaces.Infrastructure;
using Shared.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Newtonsoft.Json;

namespace AdvertHandler
{
    public class HandlerFunction 
    {
        private List<IMessageBase> messages;
        private IServiceProvider serviceProvider;

        public void Run()
        {
            messages = GetMessages();

            Console.WriteLine($"I Got {messages.Count.ToString()} Messages: Processing");

            foreach (var message in messages)
            {
                switch (message.Command)
                {
                    default:
                        break;

                    case "Create":
                        CreateAdvert(message); //Added in a cnew comment
                        break;                    
                }
            }

        }

        private void CreateAdvert(IMessageBase message)
        {
            var tableRepo = serviceProvider.GetService<ITableRepo>();
            IAdvert advert;

            try
            {
                advert = JsonConvert.DeserializeObject<IAdvert>(message.Payload);

                string PartionKey = advert.Advert_Category.ToUpper();
                string RowKey = advert.Advert_Title.ToUpper().Trim() + "^" + advert.Advert_Contact + "^" + DateTime.Now.Millisecond.ToString();

                bool created = tableRepo.AddEntity(advert, "advert", PartionKey, RowKey);

                if (created == true)
                {
                    var messageRepo = serviceProvider.GetService<IQueueRepo>();
                    messageRepo.DeleteMessage(message, "advert");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Was thrown @ CreateAdvert  - Not going to remove message form the Advert queue - Exception {ex}");
            }
            
        }

        private List<IMessageBase> GetMessages()
        {
            var queRepo = serviceProvider.GetService<IQueueRepo>();
            dynamic messages = queRepo.ReadMessages("advert", 25);

            foreach (var message in messages)
            {
                Console.WriteLine(message.Payload);
            }

            return messages;
        }


        public void Config()
        {
            Shared.Logic.DIHelper diHelper = new Shared.Logic.DIHelper();

            serviceProvider = diHelper.ReturnServiceProvider(); 
        }


    }
}
