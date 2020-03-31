using Shared.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class UserDTO : IUser
    {
        public string nickname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string picture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime updated_at { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool email_verified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string sub { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
