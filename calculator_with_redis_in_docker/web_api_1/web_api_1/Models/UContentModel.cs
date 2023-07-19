namespace web_api_1.Models
{
    public class UContentModel
    {
        public URedis_Content _URedis_Content;
        public UCassandra_Content _UCassandra_Content;
        public class URedis_Content
        {
            public string? redis_id { get; set; }
            public string? redis_value { get; set; }
        }
        public class UCassandra_Content
        {
            public string? query { get; set; }
        }
    }
}
