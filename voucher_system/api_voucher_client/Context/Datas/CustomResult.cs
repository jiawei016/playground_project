namespace api_voucher_client.Context.Datas
{
    public class CustomResult<T>
    {
        public int Status { get; set; } = 1;
        public string Message { get; set; } = "";
        public T Result { get; set; }
    }
}
