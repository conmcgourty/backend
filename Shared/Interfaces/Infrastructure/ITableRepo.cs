using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces.Infrastructure
{
    public interface ITableRepo
    {
        public bool AddEntity(object payload, string table, string partitionKey, string rowKey);

        public void DeleteEntity(string payload, string table);


    }
}
