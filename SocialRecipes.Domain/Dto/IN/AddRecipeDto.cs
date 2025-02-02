﻿using SocialRecipes.Domain.Dto.General;

namespace SocialRecipes.Domain.Dto.IN
{
    public class AddRecipeDto
    {
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Description { get; set; } = "";
        public int UserId { get; set; } = 0;
        public string Status { get; set; } = "";
        public string Image { get; set; } = "";
    }
}
