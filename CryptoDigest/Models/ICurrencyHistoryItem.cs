using System;

namespace CryptoDigest.Models
{
    public interface ICurrencyHistoryItem
    {
        string Currency { get; }
        DateTime Date { get; set; }
        decimal Value { get; set; }
    }
}