using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutomobileRentalManagementAPI.Domain
{
    public abstract class Document : IDocument
    {
        public ObjectId? DocumentId { get; set; }

        public DateTime? CreatedAt => DocumentId.Value.CreationTime;
    }

    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId? DocumentId { get; set; }
        DateTime? CreatedAt { get; }
    }
}
