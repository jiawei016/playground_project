using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using web_api_1.Models;
using web_api_1.Services.Interfaces;

namespace web_api_1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ExpressApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpressApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetValueOf(string key)
        {
            var value = await _unitOfWork.ExpressService.GetValueAsync(key);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> SetValueOf(string key, [FromBody] string value)
        {
            var _redis_model = new UContentModel.URedis_Content { 
                redis_id = key,
                redis_value = value
            };
            var _cassandra_model = new UContentModel.UCassandra_Content
            {
                query = $"Insert into usession.content(content_id, content_value) values ({key}, {value})"
            };
            var _model = new UContentModel { 
                _URedis_Content = _redis_model,
                _UCassandra_Content = _cassandra_model
            };
            await _unitOfWork.ExpressService.SetValueAsync(_model);
            return Ok();
        }
    }
}
