namespace Survivor.Dtos
{
    public class CompetitorDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
