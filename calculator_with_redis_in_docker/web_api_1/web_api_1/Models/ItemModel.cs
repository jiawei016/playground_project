using System.ComponentModel.DataAnnotations;

namespace web_api_1.Models
{
    public class ItemModel
    {
        [Key]
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Content { get; set; }
        public int IsProcessed { get; set; }
    }
}
