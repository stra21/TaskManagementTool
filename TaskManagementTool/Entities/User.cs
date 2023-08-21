using MongoDB.Bson.Serialization.Attributes;
using TaskManagementTool.Core.Entities.Base;

namespace TaskManagementTool.Core.Entities;

public class User :BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Gender { get; set; }//enum
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; }

}
public class Organization :BaseEntity
{
    public string OrganizationName { get; set; }
    public string? EmailAddress { get; set; }
    public int BusinessType { get; set; }//Enum Business Types : IT,Project Management,Personal
    public string? WebsiteUrl { get; set; }
}
public class Project :BaseEntity
{
    public string ProjectName { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Visibility { get; set; }//Enum Public/Private. Public projects can be searched for and can be joined by anyone. Private projects require direct invitation
    public bool NewMembersRequireApproval { get; set; }
    public int ApproverRole { get; set; }//Enum RolesEnum. Only a user with the specified role on a given project is allowed to approve.
    public List<string> Tags { get; set; }//For visibility option. Should be tokenized and lemmatized. Can use TFIDF or Levenshtein's Distance.
    public int ProjectMethodology { get; set; }//Enum Methodologies: Scrum,Kanban,Sprints? Can be a future feature.
    public int ProjectStatus { get; set; }//Enum Status: Finished/InProgress/Cancelled.
}
public class OrganizationProject:BaseEntity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ProjectId { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string OrganizationId { get; set; }
}
public class ProjectCollaborators : BaseEntity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UserId { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ProjectId { get; set; }
    public int Role { get; set; }//Enum RolesEnum: Owner/Manager/TeamLeader/TeamMember
}
public class Task :BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? AssigneeId { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? ProjectId { get; set; }
    public int Status { get; set; }//Enum Task Statuses: Finished/InProgress/New/Rejected/Returned
}
public class TaskHistory:BaseEntity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string TaskId { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? AssigneeId { get; set; }
    public int Status { get; set; }//Enum Task Statuses: Finished/InProgress/New/Rejected/Returned
}
public class Comment:BaseEntity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? TaskId { get; set; }
    public string? CommentContent { get; set; }
    public Attachment Attachment { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? ParentComment { get; set; }
}
public class Attachment:BaseEntity
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string TaskId { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string UploaderId { get; set; }
    public string AttachmentPath { get; set; }
    public string AttachmentType { get; set; }//Must be selected from Mime constants, e.g: img/jpeg.
}
public class AuditTrail:BaseEntity
{
    public string Operation { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string PerformedBy { get; set; }
    public DateTime ActionDate { get; set; }
}