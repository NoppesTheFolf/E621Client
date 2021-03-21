using System.Collections.Generic;
using System.Text;

namespace Noppes.E621
{
    /// <summary>
    /// User-Agent sent with each request to e621/e926.
    /// </summary>
    public class E621UserAgent
    {
        private readonly List<E621UserAgentPart> _userAgentParts;

        public E621UserAgent(string productName, string productVersion, string username,
            string platform, string? location = null)
        {
            _userAgentParts = new List<E621UserAgentPart>
            {
                new E621UserAgentPart(productName, productVersion, username, platform, location),
                Project.AsUserAgentPart()
            };
        }

        public override string ToString()
        {
            var userAgentBuilder = new StringBuilder();

            for (var i = 0; i < _userAgentParts.Count; i++)
            {
                _userAgentParts[i].AppendString(userAgentBuilder);

                if (i != _userAgentParts.Count - 1)
                    userAgentBuilder.Append(" using ");
            }

            return userAgentBuilder.ToString();
        }
    }
}
