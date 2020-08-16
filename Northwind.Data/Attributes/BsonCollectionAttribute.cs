using System;

namespace Northwind.Data.Attributes
{
    // https://bd90.pl/uzywanie-mongodb-w-srodowisku-net-core/ 
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        private string _collectionName;
        public BsonCollectionAttribute(string collectionName)
        {
            _collectionName = collectionName;
        }
        public string CollectionName => _collectionName;
    }
}