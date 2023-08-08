namespace api_voucher_client.Controllers.Attributes
{
    public sealed class RouteCustomValueAttribute : Attribute
    {
        public RouteCustomValueAttribute(string value) => Value = value;

        public string Value { get; }
    }
}
