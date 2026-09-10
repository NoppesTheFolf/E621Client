using Flurl.Http;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Implementation for the database exports area
    public partial class E621Client
    {
        /// <inheritdoc/>
        public Task<ICollection<DbExport>> GetDbExportsAsync()
        {
            return RequestAsync("/db_exports.json", request => request
                .GetJsonAsync<ICollection<DbExport>>());
        }

        /// <inheritdoc/>
        public async Task<Stream> GetDbExportStreamAsync(DbExport export)
        {
            var stream = await GetStreamAsync(export.Url.AbsoluteUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            return new GZipStream(stream, CompressionMode.Decompress);
        }
    }
}
