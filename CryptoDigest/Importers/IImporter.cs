using System.Collections.Generic;

namespace CryptoDigest.Importers
{
    using CryptoDigest.Models;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public interface IImporter<out T> where T : class, ICurrencyHistoryItem
    {
        string FileLocation { get; set; }

        IEnumerable<T> Import();
    }
}