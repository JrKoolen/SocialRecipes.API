namespace SocialRecipes.Domain.Dto.General
{
    public class RecipeDto
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
        public byte[] Image { get; set; }
    }
}
