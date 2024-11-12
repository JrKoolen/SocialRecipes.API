namespace SocialRecipes.DAL.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public byte[] Image { get; set; }

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
