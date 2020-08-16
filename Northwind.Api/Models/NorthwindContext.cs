using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Northwind.Api.Extensions;
using Northwind.Api.Models;

namespace Northwind.Data.Entities
{
    // partial
    public class NorthwindContext : INorthwindMongoDbContext
    {
        // Represents the Mongo database for performing operations. 
        // Generic GetCollection<T>(collection) method on the interface is used to gain access to data in a specific collection.
        private IMongoDatabase Database { get; set; } = null;

        // Reads the server instance for performing database operations. 
        // The constructor of this class is provided the MongoDB connection string.
        private MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        public IClientSessionHandle Session { get; set; }

        // https://www.brunobrito.net.br/aspnet-core-mongodb-unit-of-work/
        
        public NorthwindContext(IOptions<NorthwindDatabaseSettings> settings)
        {
            // Set Guid to CSharp style (with dash -) // ????
            // BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            RegisterConventions();
            ConfigureMongo(settings);
        }

        private void ConfigureMongo(IOptions<NorthwindDatabaseSettings> settings) 
        { 
            MongoClient = new MongoClient(settings.Value.ConnectionString);
            if (MongoClient != null)
                Database = MongoClient.GetDatabase(settings.Value.DatabaseName);
            if (!Database.Ping())
                throw new Exception("Could not connect to MongoDb");
                
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            // while (Session != null && Session.IsInTransaction)
            //     Thread.Sleep(TimeSpan.FromMilliseconds(100));

            GC.SuppressFinalize(this);
        }

        public Task AddCommand(Func<Task> func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }
        
    }

    public interface INorthwindMongoDbContext : IDisposable
    { 
        Task AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}