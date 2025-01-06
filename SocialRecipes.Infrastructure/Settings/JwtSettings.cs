using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialRecipes.Infrastructure.Settings
{
    public class JwtSettings
    {
        private string Secret = "T8x!g5#Lk92z@Q7P$G1%XcMZ5L!7DfNlR";
        private string Issuer = "http://localhost";
        private string Audience = "http://localhost";

        public JwtSettings()
        {
            
        }
        public string GetSecret()
        {
            return Secret;
        }
        public string GetIssuer()
        {
            return Issuer;
        }
        public string GetAudience()
        {
            return Audience;
        }
    }
}
