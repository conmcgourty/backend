using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces.Models
{
    interface IUser
    {
        public string nickname { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public DateTime updated_at { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string sub { get; set; }
    }
}
