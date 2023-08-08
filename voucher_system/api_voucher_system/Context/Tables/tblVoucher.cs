using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_voucher_system.Context.Tables
{
    public class tblVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VoucherName { get; set; } = "";
        public int VoucherValue { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}
