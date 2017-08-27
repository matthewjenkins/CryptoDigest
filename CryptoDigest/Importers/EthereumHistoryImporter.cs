namespace CryptoDigest.Importers
{
    using CryptoDigest.Models;

    public class EthereumHistoryImporter : WebCsvImporter<EthereumHistoryItem, EthereumHistoryItemMap>
    {
        public EthereumHistoryImporter()
        {
            this.FileLocation = "https://etherscan.io/chart/etherprice?output=csv";
        }

        public sealed override string FileLocation { get; set; }
    }
}