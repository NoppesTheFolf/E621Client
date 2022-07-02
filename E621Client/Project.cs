namespace Noppes.E621
{
    /// <summary>
    /// Contains info about this project.
    /// </summary>
    internal static class Project
    {
        private const string Name = "E621Client";

        private const string Version = "0.8.0";

        private const string DevelopedBy = "NoppesTheFolf and contributors";

        private const string Url = "https://github.com/NoppesTheFolf/E621Client";

        private const string Platform = "GitHub";

        public static E621UserAgentPart AsUserAgentPart() =>
            new E621UserAgentPart(Name, Version, DevelopedBy, Platform, Url);
    }
}
