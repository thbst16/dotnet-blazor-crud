namespace Blazorcrud.Shared.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public string FileName { get; set; } = default!;
        public DateTime UploadTimestamp {get; set;} = default!;
        public DateTime? ProcessedTimestamp {get; set;} = default!;
        public string FileContent {get; set;} = default!;
    }
}