using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using SocialRecipes.Domain.Dto.ExampleResponse;
using System;
using System;


namespace SocialRecipes.Domain.SwaggerExamples
{

    public class LoginResponseExample : IExamplesProvider<LoginResponse>
    {
        public LoginResponse GetExamples()
        {
            return new LoginResponse
            {
                Token = "Example JWT token"
            };
        }
    }
}
