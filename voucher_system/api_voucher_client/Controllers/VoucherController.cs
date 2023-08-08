using api_voucher_client.Context;
using api_voucher_client.Context.Datas;
using api_voucher_client.Controllers.Attributes;
using api_voucher_client.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_voucher_client.Controllers
{
    [ApiController]
    [RouteCustomValue(ApplicationConstants.api_namespace)]
    [Route("[RouteCustomValue]/[controller]/[action]")]
    public class VoucherController : ControllerBase
    {
        private readonly IRedisService _redisService;
        public VoucherController(IServiceProvider serviceProvider)
        {
            _redisService = serviceProvider.GetRequiredService<IRedisService>();
        }
        [HttpPost]
        public async Task<IActionResult> RedeemVoucher()
        {
            try
            {
                var _customResult = new CustomResult<VoucherModels>();
                string _result = await _redisService.GetRedisValue("redis_voucher_app");

                if (!string.IsNullOrEmpty(_result))
                {
                    VoucherModels voucherModels = JsonConvert.DeserializeObject<VoucherModels>(_result);

                    _customResult = new CustomResult<VoucherModels>
                    {
                        Result = voucherModels
                    };
                }
                return Ok(_customResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
