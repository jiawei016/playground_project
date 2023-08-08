using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace api_voucher_system.Controllers.Attributes
{
    public sealed class CustomValueRoutingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            var value = action.Controller.ControllerType.GetCustomAttribute<RouteCustomValueAttribute>()?.Value;
            if (value is not null)
            {
                action.RouteValues.Add("RouteCustomValue", value);
            }
        }
    }
}
