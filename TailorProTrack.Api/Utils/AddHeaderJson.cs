using System.Runtime.CompilerServices;
using System.Text.Json;
using TailorProTrack.Application.Core;

namespace TailorProTrack.Api.Utils
{
    public static class AddHeaders{

        public static void AddHeaderPaginationJson(this HttpResponse response, PaginationMetaData metadata)
        {
            response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        }
    }
}
