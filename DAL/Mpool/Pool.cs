using DAL.Mmessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Mpool
{
   public class Pool
    {
        public string poolName { get; set; }
        public string poolMode { get; set; }       
        public string description { get; set; }       
        public dynamic messages { get; set; }
        public string messageSortColumn { get; set; }
        public string creator { get; set; }
        public string createTime { get; set; }        
        public string updateTime { get; set; }
        private HashSet<string> clients = new HashSet<string>();



        public Pool(string poolName, string poolMode, string description, Dictionary<string, Message> messages,string messageSortColumn, string creator, string createTime, string updateTime, HashSet<string> clients)
        {
            this.poolName = poolName;
            this.poolMode = poolMode;
            this.description = description;
            this.messages = messages;
            this.messageSortColumn = messageSortColumn;
            this.creator = creator;
            this.createTime = createTime;
            this.updateTime = updateTime;
            this.clients = clients;
        }

        public Pool(string poolName, string poolMode, string description, string messageSortColumn)
        {
            this.poolName = poolName;
            this.poolMode = poolMode;
            this.description = description == null ? "null" : description;
            this.messageSortColumn = messageSortColumn == null ? "null" : messageSortColumn;
            this.clients = new HashSet<string>();
            this.messages = new Dictionary<string, Message>();
        }

    }

   public enum PoolType
    {
        normal,//通常
        info,//返回池简介信息
        front//返回给前端模式

    }       
}
