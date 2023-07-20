using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker_service_subscriber.Models
{
    public class ItemModel
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Content { get; set; }
        public int IsProcessed { get; set; }
    }
}
