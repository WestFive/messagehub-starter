using DAL.Mmessage;
using DAL.Mpool;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageHubCore.Hubs
{
    public class MessageHub : Hub
    {
        public static Dictionary<string, Pool> DataMap = new Dictionary<string, Pool>();
        public static ILogger logger { get; set; }

        public MessageHub(ILoggerFactory factory)
        {
            logger = factory.CreateLogger("MessageHub");
        }



        public async Task CreateMessage(string poolName, string messageKey, string messageValue)
        {
            Dictionary<string, Message> messages = new Dictionary<string, Message>();
            if (DataMap.ContainsKey(poolName))
            {
                messages = DataMap[poolName].messages;
                if (messages.ContainsKey(messageKey))
                {
                    messages.Remove(messageKey);
                }
                messages.Add(messageKey, new Message(messageKey, messageValue));
            }
            await pushPool();
            await Clients.Caller.SendAsync("ServerResponse", "OK");
        }

        public async Task DeleteMessage(string poolName,string messageKey)
        {
            Dictionary<string, Message> messages = new Dictionary<string, Message>();
            if (DataMap.ContainsKey(poolName))
            {
                messages = DataMap[poolName].messages;                
                if(messages.ContainsKey(messageKey))
                {
                    messages.Remove(messageKey);
                    await pushPool();
                    await Clients.Caller.SendAsync("ServerResponse", "删除消息成功");
                    return;
                }
                await Clients.Caller.SendAsync("ServerResponse", "删除消息失败，找不到该条消息");
                return;
            }            
            await Clients.Caller.SendAsync("ServerResponse", "删除消息失败，找不到池");
        }

        public async Task CreatePool(string poolName, string poolMode, string des, string sort)
        {
            Pool pool = new Pool(poolName, poolMode, des, sort);
            if (DataMap.ContainsKey(pool.poolName))
            {
                DataMap[pool.poolName] = pool;

                await pushPool();
                await Clients.Caller.SendAsync("ServerResponse", "创建池成功");
                return;
            }

            DataMap.Add(poolName, pool);
            await pushPool();
            await Clients.Caller.SendAsync("ServerResponse", "创建池成功");
        }

        public async Task DeletePool(string poolName)
        {
            if (DataMap.ContainsKey(poolName))
            {
                DataMap.Remove(poolName);
                await Clients.Caller.SendAsync("ServerResponse", "删除池成功");
                await pushPool();
                return;
            }
            await Clients.Caller.SendAsync("ServerResponse", "找不到该池，无法删除");

        }

        private async Task pushPool()
        {            
            await Clients.All.SendAsync("PoolList",DataMap.Values);
        }
    }
}
