using api_voucher_system.Context;
using api_voucher_system.Context.Datas;
using api_voucher_system.Controllers.Attributes;
using api_voucher_system.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_voucher_system.Controllers
{
    [ApiController]
    [RouteCustomValue(ApplicationConstants.api_namespace)]
    [Route("[RouteCustomValue]/[controller]/[action]")]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IServiceProvider serviceProvider)
        {
            _voucherService = serviceProvider.GetRequiredService<IVoucherService>();
        }
        [HttpPost]
        public async Task<IActionResult> ProduceVoucher(VoucherModels _voucher)
        {
            try
            {
                bool _status = await _voucherService.ProduceNewVoucher(_voucher);
                return Ok(_status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
