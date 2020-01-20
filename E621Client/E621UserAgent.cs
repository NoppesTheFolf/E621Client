using System.Collections.Generic;
using System.Text;

namespace Noppes.E621
{
    /// <summary>
    /// User-Agent sent with each request to e621/e926.
    /// </summary>
    public class E621UserAgent
    {
        private List<E621UserAgentPart> UserAgentParts { get; }

        public E621UserAgent(string productName, string productVersion, string username,
            string platform, string? location = null)
        {
            UserAgentParts = new List<E621UserAgentPart>
            {
                new E621UserAgentPart(productName, productVersion, username, platform, location),
                Project.AsUserAgentPart()
            };
        }

        public override string ToString()
        {
            StringBuilder userAgentBuilder = new StringBuilder();

            for (int i = 0; i < UserAgentParts.Count; i++)
            {
                UserAgentParts[i].AppendString(userAgentBuilder);

                if (i != UserAgentParts.Count - 1)
                    userAgentBuilder.Append(" using ");
            }

            return userAgentBuilder.ToString();
        }
    }
}
