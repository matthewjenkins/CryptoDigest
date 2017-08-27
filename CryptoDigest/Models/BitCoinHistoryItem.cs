using System;

namespace CryptoDigest.Models
{
    using CsvHelper.Configuration;

    public class BitCoinHistoryItem : ICurrencyHistoryItem
    {
        public string Currency => "BTC";
        public DateTime Date { get; set; }

        public decimal Value { get; set; }
    }

    public sealed class BitCoinHistoryItemMap : CsvClassMap<BitCoinHistoryItem>
    {
        public BitCoinHistoryItemMap()
        {
            this.Map(m => m.Date).Index(0);
            this.Map(m => m.Value).Index(1);
            this.Map(m => m.Currency).Ignore(true);
        }
    }
}