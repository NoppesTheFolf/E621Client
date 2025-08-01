using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the Flag part of the E621Client (added by Jdbye)
    public partial interface IE621Client
    {
        /// <summary>
        /// Retrieves flags for one or more posts.
        /// </summary>
        /// <param name="postIds">The IDs of the posts to retrieve the flags for.</param>
        /// <param name="limit">
        /// The maximum number of post flags to retrieve in a single call. Maximum defined at <see cref="E621Constants.PostFlagsMaximumLimit"/>.
        /// </param>
        public Task<ICollection<PostFlag>> GetPostFlagsAsync(IEnumerable<int>? postIds = null, int limit = E621Constants.PostFlagsMaximumLimit);
    }
}
