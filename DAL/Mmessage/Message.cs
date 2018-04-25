using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Mmessage
{
    public class Message
    {
        public string key { get; set; }
        public string value { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }


        public Message(string key,string value)
        {
            this.key = key;
            this.value = value;
            this.updateTime = DateTime.Now.ToString();
            this.updateTime = DateTime.Now.ToString();
        }

        public Message(string key,string value,string createTime,string updateTime)
        {
            this.key = key;
            this.value = value;
            this.createTime = createTime;
            this.updateTime = updateTime;
        }
        
    }

    
}
