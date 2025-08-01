using Dawn;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using Noppes.E621.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
        /// <inheritdoc />
        public Task<ICollection<PostFlag>> GetPostFlagsAsync(IEnumerable<int> postIds, int limit = E621Constants.PostFlagsMaximumLimit)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, E621Constants.PostFlagsMaximumLimit);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8621 // Nullability of reference types in return type doesn't match the target delegate (possibly because of nullability attributes).
            return RequestAsync("/post_flags.json", request =>
                    request
                    .AuthenticatedIfPossible(this)
                    .SetQueryParam("limit", limit)
                    .SetQueryParam("search[post_id]", string.Join(',', postIds))
                    .GetJsonAsync(token =>
                    {
                        // e621 API weirdness. e621 will sends back a regular array [ ... ] if post IDs exist, else { "post_flags": [] }
                        if (token.Type != JTokenType.Array)
                        {
                            var containerProperty = token["post_flags"];
                            token = containerProperty ?? token;
                        }

                        return token.ToObject<ICollection<PostFlag>>();
                    }));
#pragma warning restore CS8621 // Nullability of reference types in return type doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
