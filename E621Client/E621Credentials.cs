namespace Noppes.E621
{
    /// <summary>
    /// The credentials used to gain access with to protected resources.
    /// </summary>
    internal class E621Credentials
    {
        public string Username { get; set; }

        public string ApiKey { get; set; }

        public E621Credentials(string username, string apiKey)
        {
            Username = username;
            ApiKey = apiKey;
        }
    }
}
