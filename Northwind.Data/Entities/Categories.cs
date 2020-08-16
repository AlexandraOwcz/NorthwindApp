using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Northwind.Data.Entities
{
    public class Categories
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