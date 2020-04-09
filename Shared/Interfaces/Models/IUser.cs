
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces.Models
{
    public interface IUser
    {
     
        public string nickname { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
       
        public string updatedAt { get; set; }
        public string email { get; set; }
        
        public bool emailVerified { get; set; }
        public string sub { get; set; }
    }
}
