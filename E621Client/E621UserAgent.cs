using System.Text;

namespace Noppes.E621
{
    /// <summary>
    /// The User-Agent sent with each request.
    /// </summary>
    internal class E621UserAgent
    {
        private static readonly E621UserAgentPart ProjectUserAgentPart = new E621UserAgentPart(Project.Name, Project.Version, Project.DevelopedBy, Project.Platform, Project.Url);

        private readonly E621UserAgentPart _consumerUserAgentPart;

        public E621UserAgent(string productName, string productVersion, string username, string platform, string? location = null)
        {
            _consumerUserAgentPart = new E621UserAgentPart(productName, productVersion, username, platform, location);
        }

        public override string ToString()
        {
            var userAgentBuilder = new StringBuilder();

            _consumerUserAgentPart.AppendString(userAgentBuilder);
            userAgentBuilder.Append(" using ");
            ProjectUserAgentPart.AppendString(userAgentBuilder);

            return userAgentBuilder.ToString();
        }
    }
}
