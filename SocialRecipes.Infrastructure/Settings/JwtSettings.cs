using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialRecipes.Infrastructure.Settings
{
    public class JwtSettings
    {
        private readonly string secret;
        private readonly string issuer;
        private readonly string audience;

        public JwtSettings() 
        {
            this.secret =  "T8x!g5#Lk92z@Q7P$G1%XcMZ5L!7DfNlR";
            this.issuer = "http://localhost";
            this.audience = "http://localhost";
        }

        public JwtSettings(string Secret, string Issuer, string Audience)
        {
            this.secret = Secret;   
            this.issuer = Issuer;
            this.audience = Audience;
        }
        public string GetSecret()
        {
            return secret;
        }
        public string GetIssuer()
        {
            return issuer;
        }
        public string GetAudience()
        {
            return audience;
        }
    }
}
