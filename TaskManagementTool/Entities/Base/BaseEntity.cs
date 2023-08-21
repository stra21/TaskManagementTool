using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagementTool.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletionDate { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
