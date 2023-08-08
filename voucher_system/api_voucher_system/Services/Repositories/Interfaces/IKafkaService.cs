namespace api_voucher_system.Services.Repositories.Interfaces
{
    public interface IKafkaService
    {
        public Task<bool> ProduceMessage(string _message_data);
    }
}
