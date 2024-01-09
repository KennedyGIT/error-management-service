namespace API.Dtos
{
    public class ErrorDto
    {
        public int Id { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Version { get; set; }
    }
}
