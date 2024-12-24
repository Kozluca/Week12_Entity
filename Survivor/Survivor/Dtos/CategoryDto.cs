namespace Survivor.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; }
        // public List<Competitor>? Competitors { get; set; }
    }
}
