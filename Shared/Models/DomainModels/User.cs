using Newtonsoft.Json;
using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class UserDTO : IUser
    {      
        public string nickname { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
       
        [JsonProperty("updated_at")]
        public string updatedAt { get; set; }
        public string email { get; set; }
        
        [JsonProperty("email_verified")]
        public bool emailVerified { get; set; }
        public string sub { get; set; }
    }
}
