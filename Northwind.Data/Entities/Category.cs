using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Northwind.Data.Attributes;

namespace Northwind.Data.Entities
{
    [BsonCollection("Categories")]
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        /*
        "CategoryID": 3,
        "CategoryName": "Confections",
        "Description": "Desserts",
        "Picture": "candies",
        "field4": "and sweet breads",
        "field5"
        
        */
    }
}
// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2