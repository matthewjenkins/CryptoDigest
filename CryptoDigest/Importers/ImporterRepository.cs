using System;
using System.Collections.Generic;

namespace CryptoDigest.Importers
{
    using CryptoDigest.Models;
    using System.Collections.Concurrent;
    using System.Linq;

    public static class ImporterRepository
    {
        private static readonly object _lock = new object();

        private static readonly IDictionary<Type, IImporter<ICurrencyHistoryItem>> _singleton =
            new ConcurrentDictionary<Type, IImporter<ICurrencyHistoryItem>>();

        static ImporterRepository()
        {
            var importers = GetImporters();
            foreach (var importer in importers)
            {
                RegisterImporter(importer);
            }
        }

        public static IEnumerable<ICurrencyHistoryItem> GetAllCurrencyHistoryItems()
        {
            lock (_lock)
            {
                foreach (var importer in _singleton)
                {
                    var records = importer.Value.Import();
                    foreach (var item in records)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static T GetImporter<T>()
                            where T : IImporter<ICurrencyHistoryItem>, new()
        {
            lock (_lock)
            {
                if (_singleton.ContainsKey(typeof(T)))
                {
                    return (T)_singleton[typeof(T)];
                }

                return new T();
            }
        }

        private static IEnumerable<Type> GetImporters()
        {
            var desiredType = typeof(IImporter<ICurrencyHistoryItem>);

            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(
                type => desiredType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);
        }

        private static void RegisterImporter(Type type)
        {
            lock (_lock)
            {
                var instance = (IImporter<ICurrencyHistoryItem>)Activator.CreateInstance(type);
                _singleton[type] = instance;
            }
        }
    }
}