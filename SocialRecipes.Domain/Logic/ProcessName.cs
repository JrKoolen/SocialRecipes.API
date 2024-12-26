using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialRecipes.Domain.Logic
{
    public class ProcessName
    {
        // why am i using return instead of throw???
        // i want to show the user/ api / controller what whent wrong in the process of creating the account.
        readonly char[] invalidChars = new char[] { '@', '#', '$', '%', '^', '&', '*', '(', ')', '!', '~', '<', '>', '/', '\\', '|', '[', ']', '{', '}', ';', ':', '"', '\'', '?' };
        private string name { get; set;}
        private int minLength = 4;
        private int maxLength = 12;
        bool containsInvalidChars;

        public ProcessName(string name)
        {
            this.name = name;
            this.containsInvalidChars = name.Any(ch => invalidChars.Contains(ch));
        }

        public string Process()
        {
            switch (name) 
            {
                case null:
                    return "Name cannot be null.";
                case string n when n.Length < minLength || n.Length > maxLength:
                    return minLength + " characters is the minimum length for a name. maximum length is " + maxLength;
                case string n when n.Any(ch => invalidChars.Contains(ch)):
                    return "Name cannot contain special characters.";
                default:
                    return name + " is valid";
            }
        }
    }
}
