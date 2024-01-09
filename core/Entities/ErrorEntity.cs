namespace core.Entities
{
    public class ErrorEntity : BaseEntity
    {
        public string ErrorTitle { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Version { get; set; }

    }
}
