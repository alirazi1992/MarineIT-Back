namespace MarineIT.Domain.Entities
{
    public class KnowledgeArticle
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public int Version { get; set; } = 1;
        public DateTime LastUpdatedUtc { get; set; } = DateTime.UtcNow;
    }
}
