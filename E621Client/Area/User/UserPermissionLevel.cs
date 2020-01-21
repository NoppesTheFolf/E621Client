namespace Noppes.E621
{
    /// <summary>
    /// Permission levels used by e621.
    /// </summary>
    public enum UserPermissionLevel
    {
        Unactivated = 0,
        Blocked = 10,
        Member = 20,
        Privileged = 30,
        Contributor = 33,
        FormerStaff = 34,
        Janitor = 35,
        Moderator = 40,
        Admin = 50
    }
}
