namespace Noppes.E621
{
    /// <summary>
    /// Permission levels used by e621. This decides whatever a user is, and is not, allowed to do
    /// on e621.
    /// </summary>
    public enum UserPermissionLevel
    {
        /// <summary>
        /// An account that has registered to e621, but of which the e-mail address hasn't been
        /// confirmed yet.
        /// </summary>
        Unactivated = 0,
        /// <summary>
        /// An account that has been banned from e621.
        /// </summary>
        Blocked = 10,
        /// <summary>
        /// The basic account type. Every user is a member upon registering.
        /// </summary>
        Member = 20,
        /// <summary>
        /// The next level up from member. Given out regularly to users who have proven that they're
        /// a positive addition to the community.
        /// </summary>
        Privileged = 30,
        /// <summary>
        /// The next level up from privileged. This account type is given to e621's power-users whom
        /// e621 completely trust with contributing quality content to the site.
        /// </summary>
        Contributor = 33,
        /// <summary>
        /// An account who was part of the staff team, but isn't anymore.
        /// </summary>
        FormerStaff = 34,
        /// <summary>
        /// Janitors are users that help process pending posts (the mod queue).
        /// </summary>
        Janitor = 35,
        /// <summary>
        /// Mods are generally considered to be admins-in-training, and have access to all but the
        /// most powerful site tools. Assuming everything goes okay, at the end of an undefined
        /// period of time, moderators will be eligible to become full-fledged admins.
        /// </summary>
        Moderator = 40,
        /// <summary>
        /// Almighty gods.
        /// </summary>
        Admin = 50
    }
}
