using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the database exports area
    public partial interface IE621Client
    {
        /// <summary>
        /// Get all the database exports which can be downloaded.
        /// </summary>
        /// <remarks>
        /// Reading the downloaded database exports requires the separate
        /// <see href="https://www.nuget.org/packages/Noppes.E621Client.DbExport">Noppes.E621Client.DbExport</see>
        /// package. See the <see href="https://github.com/NoppesTheFolf/E621Client#database-exports">project page</see>
        /// for instructions on how to use it.
        /// </remarks>
        public Task<ICollection<DbExport>> GetDbExportsAsync();

        /// <summary>
        /// Get a decompressed <see cref="Stream"/> for the given database export. The returned
        /// stream can be saved to a file or read in memory.
        /// </summary>
        /// <remarks>
        /// Reading the returned stream requires the separate
        /// <see href="https://www.nuget.org/packages/Noppes.E621Client.DbExport">Noppes.E621Client.DbExport</see>
        /// package. See the <see href="https://github.com/NoppesTheFolf/E621Client#database-exports">project page</see>
        /// for instructions on how to use it.
        /// </remarks>
        public Task<Stream> GetDbExportStreamAsync(DbExport export);
    }
}
