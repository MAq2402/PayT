using System;
using System.Collections.Generic;
using System.Text;

namespace PayT.Infrastructure.Types
{
    public class EventStoreSettings
    {
        public EventStoreSettings(string stream, string uri)
        {
            Stream = stream;
            Uri = uri;
        }

        public string Stream { get; }
        public string Uri { get; }
    }

    public class MongoSettings
    {
        public MongoSettings(string dbName, string collectionName)
        {
            DbName = dbName;
            CollectionName = collectionName;
        }

        public string DbName { get; }
        public string CollectionName { get; }
    }

    public class Settings
    {
        public Settings(EventStoreSettings eventStoreSettings, MongoSettings mongoSettings)
        {
            EventStoreSettings = eventStoreSettings;
            MongoSettings = mongoSettings;
        }

        public EventStoreSettings EventStoreSettings { get; }
        public MongoSettings MongoSettings { get; }
    }
}