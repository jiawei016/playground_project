namespace api_voucher_system.Controllers.Attributes
{
    public sealed class RouteCustomValueAttribute : Attribute
    {
        public RouteCustomValueAttribute(string value) => Value = value;

        public string Value { get; }
    }
}
