using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the posts part of the E621Client.
    public partial interface IE621Client
    {
        /// <summary>
        /// Retrieves a collection of posts based on the given parameters.
        /// <para>
        /// You can choose which posts to show and hide by filtering on tags. The maximum number of
        /// tags that can be used, is defined at <see cref="E621Constants.PostsMaximumTagSearchCount"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// Be aware that there is a limit on the number of posts which can be retrieved with a
        /// single call. This limit is defined at <see cref="E621Constants.PostsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the posts by specifying both an post ID and a position. The
        /// position parameter defines the position of the returned posts relative to the given post
        /// ID. Let's take post with ID 1000 as an example. Passing this ID in combination with <see
        /// cref="Position.Before"/> will cause the posts 999, 998, 997, etc. to be retrieved. Using
        /// <see cref="Position.After"/> will retrieve the posts 1001, 1002, 1003, etc. You should
        /// use this method if you don't need pagination or need to avoid the limit pagination
        /// comes with it. Moreover, this is the most efficient way to navigate through posts.
        /// </para>
        /// </summary>
        /// <param name="tags">Tags used to filter the result.</param>
        /// <param name="limit">The maximum number of posts returned in a single request.</param>
        /// <param name="id">ID of the post you want the retrieved posts the be relatively positioned to.</param>
        /// <param name="position">Relative position to the given post ID.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Task<IList<Post>> GetPostsAsync(int id, Position position, int? limit = null, string? tags = null);

        /// <summary>
        /// Retrieves a collection of posts based on the given parameters.
        /// <para>
        /// You can choose which posts to show and hide by filtering on tags. The maximum number of
        /// tags that can be used, is defined at <see cref="E621Constants.PostsMaximumTagSearchCount"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// Be aware that there is a limit on the number of posts which can be retrieved with a
        /// single call. This limit is defined at <see cref="E621Constants.PostsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the posts by specifying a page number. There is, however, a
        /// limit to the value. This limit is defined in <see cref="E621Constants.PostsMaximumPage"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// </summary>
        /// <param name="tags">Tags used to filter the result.</param>
        /// <param name="limit">The maximum number of posts returned in a single request.</param>
        /// <param name="page">Pagination, number of the page.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Task<IList<Post>> GetPostsAsync(string? tags = null, int? page = null, int? limit = null);

        /// <summary>
        /// Retrieves the post with the given ID. A null value will be returned if there doesn't
        /// exist a post with the given ID.
        /// </summary>
        public Task<Post?> GetPostAsync(int id);

        /// <summary>
        /// Retrieves the post of which the image its MD5 hash matches the given MD5 hash. A null
        /// value will be returned if no image matches the given hash or if the post attached to the
        /// image has been deleted.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public Task<Post?> GetPostAsync(string md5);
    }
}