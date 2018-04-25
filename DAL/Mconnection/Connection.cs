using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAL.Mconnection
{
    public class Connection
    {
        private string connectionId;
        private string clientName;
        private string clientCode;
        private string clientIpAddress;
        private Collection<string> privatePools;

        public Connection(string connectionId, string clientName, string clientCode, string clientIpAddress, Collection<string> privatePools)
        {
            this.connectionId = connectionId;
            this.clientName = clientName;
            this.clientCode = clientCode;
            this.clientIpAddress = clientIpAddress;
            this.privatePools = privatePools;
        }
    }
}
