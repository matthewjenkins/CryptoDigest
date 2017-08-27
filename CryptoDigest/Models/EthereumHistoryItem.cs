using System;

namespace CryptoDigest.Models
{
    using CsvHelper.Configuration;
    using System.Globalization;

    public class EthereumHistoryItem : ICurrencyHistoryItem
    {
        public string Currency => "ETH";
        public DateTime Date { get; set; }

        public long TimeStamp { get; set; }

        public decimal Value { get; set; }
    }

    public sealed class EthereumHistoryItemMap : CsvClassMap<EthereumHistoryItem>
    {
        public EthereumHistoryItemMap()
        {
            this.Map(m => m.Date).Name("Date(UTC)").TypeConverterOption(DateTimeStyles.AdjustToUniversal);
            this.Map(m => m.TimeStamp).Name("UnixTimeStamp");
            this.Map(m => m.Value).Name("Value");
            this.Map(m => m.Currency).Ignore(true);
        }
    }
}