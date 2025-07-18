using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the IQDB part of the E621Client.
    public partial interface IE621Client
    {
        /// <summary>
        /// Reverse search an image using an image located at the given URL. Note that e621 will not
        /// download files domains that aren't whitelisted. If you provided a URL that e621 does not
        /// trust, a <see cref="E621ClientForbiddenException"/> will be thrown. If you provided a
        /// URL that is whitelisted, but e621 is unable to retrieve the image for whatever reason, a
        /// null value will be returned.
        /// </summary>
        /// <param name="url">The url of the image.</param>
        /// <param name="activeOnly">Whether or not to only return posts which are not deleted.</param>
        public Task<ICollection<IqdbPost>?> QueryIqdbByUrlAsync(string url, int scoreCutoff = 75, bool activeOnly = true);

        /// <summary>
        /// Reverse search an image using a stream that contains image data. If the stream doesn't
        /// contain valid data, an empty list will be returned.
        /// </summary>
        /// <param name="stream">The stream containing the image data.</param>
        /// <param name="activeOnly">Whether or not to only return posts which are not deleted.</param>
        public Task<ICollection<IqdbPost>> QueryIqdbByStreamAsync(Stream stream, int scoreCutoff = 75, bool activeOnly = true);

        /// <summary>
        /// Reverse search an image using an image stored on a filesystem. A <see
        /// cref="ArgumentException"/> will be thrown in case there doesn't exist an image at the
        /// given path. You SHOULD make sure the file extension of the file your submitting matches
        /// any of the file extensions defined in <see cref="E621Constants.IqdbAllowedFormatExtensions"/>.
        /// However, e621 will simply return no matches in case an invalid file is uploaded.
        /// </summary>
        /// <param name="path">The path where the image is located.</param>
        /// <param name="activeOnly">Whether or not to only return posts which are not deleted.</param>
        public Task<ICollection<IqdbPost>> QueryIqdbByFileAsync(string path, int scoreCutoff = 75, bool activeOnly = true);
    }
}
