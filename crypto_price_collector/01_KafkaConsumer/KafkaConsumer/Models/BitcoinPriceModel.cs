using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Models
{
    public class BitcoinPriceModel
    {
        [Key]
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
