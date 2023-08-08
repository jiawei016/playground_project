namespace api_voucher_client.Context.Datas
{
    public class VoucherModels
    {
        public int Id { get; set; }
        public string VoucherName { get; set; } = "";
        public int VoucherValue { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}
