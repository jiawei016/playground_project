using api_voucher_system.Context.Datas;

namespace api_voucher_system.Services.Repositories.Interfaces
{
    public interface IVoucherService
    {
        public Task<bool> ProduceNewVoucher(VoucherModels _voucher);
    }
}
