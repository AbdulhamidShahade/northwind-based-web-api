namespace NorthwindBasedWebAPI.Models.Base
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
