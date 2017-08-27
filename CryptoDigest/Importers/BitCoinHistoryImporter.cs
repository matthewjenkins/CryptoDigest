namespace CryptoDigest.Importers
{
    using CryptoDigest.Models;

    public sealed class BitCoinHistoryImporter : WebCsvImporter<BitCoinHistoryItem, BitCoinHistoryItemMap>
    {
        public BitCoinHistoryImporter()
        {
            this.FileLocation = "https://api.blockchain.info/charts/market-price?timespan=all&format=csv";
        }

        public override string FileLocation { get; set; }
    }
}