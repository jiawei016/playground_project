using api_voucher_system.Context;
using api_voucher_system.Context.Datas;
using api_voucher_system.Context.Tables;
using api_voucher_system.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api_voucher_system.Services.Repositories
{
    public class VoucherService : IVoucherService
    {
        private readonly IKafkaService _kafkaService;
        private ApplicationDbContext _dbContext;
        public VoucherService(IServiceProvider serviceProvider)
        {
            _kafkaService = serviceProvider.GetRequiredService<IKafkaService>();
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }
        public async Task<bool> ProduceNewVoucher(VoucherModels _voucher)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    tblVoucher _tblVoucher = new tblVoucher
                    {
                        VoucherName = _voucher.VoucherName,
                        VoucherValue = _voucher.VoucherValue,
                        CreatedAt = DateTime.UtcNow,
                    };

                    _dbContext.tblVoucher.Add(_tblVoucher);
                    _dbContext.SaveChanges();

                    string _json = JsonConvert.SerializeObject(_tblVoucher);
                    tblProducerPlayer _tblProducerPlayer = new tblProducerPlayer
                    {
                        Module = "ProduceNewVoucher",
                        Message = _json,
                        CreatedAt = DateTime.UtcNow,
                    };

                    _dbContext.tblProducerPlayer.Add(_tblProducerPlayer);
                    await _dbContext.SaveChangesAsync();

                    await _kafkaService.ProduceMessage(_json);

                    await transaction.CommitAsync();

                    return true;
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
