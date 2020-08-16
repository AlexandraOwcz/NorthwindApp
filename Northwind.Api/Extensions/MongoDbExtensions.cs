using System;
using MongoDB.Driver;

namespace Northwind.Api.Extensions
{
    public static class MongoDbExtensions
    {
        public static bool Ping(this IMongoDatabase db, int secondToWait = 1)
        {
            if (secondToWait <= 0)
                throw new ArgumentOutOfRangeException("secondToWait", secondToWait, "Must be at least 1 second");

            return db.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait(secondToWait * 1000);
        }
    }
}