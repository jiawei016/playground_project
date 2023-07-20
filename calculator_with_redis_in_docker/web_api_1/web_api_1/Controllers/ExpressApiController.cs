using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using web_api_1.Models;
using web_api_1.Services.Interfaces;
using web_api_1.Services.Modules.Interfaces;

namespace web_api_1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ExpressApiController : ControllerBase
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public ExpressApiController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetValueOf(string key)
        {
            var value = await _iUnitOfWork.ItemService.GetValueAsync(key);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> SetValueOf(string key, [FromBody] string value)
        {
            await _iUnitOfWork.ItemService.SaveValueAsync(key, value);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SetIsProcessed(string key, string Id)
        {
            await _iUnitOfWork.ItemService.SetIsProcessedAsync(key, Id);
            return Ok();
        }
    }
}
