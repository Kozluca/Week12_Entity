namespace Code_First_Relation.Data
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<Post> Posts { get; set; }
    }
}
