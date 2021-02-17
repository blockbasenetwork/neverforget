namespace BlockBase.Dapps.NeverForgetBot.Data.Entities.Base
{
    public abstract class AuditEntity
    {
        public string CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public string? DeletedAt { get; set; }
    }
}
