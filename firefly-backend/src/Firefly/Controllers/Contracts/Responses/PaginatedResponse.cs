using System.Collections.Generic;

namespace Firefly.Controllers.Contracts.Responses
{
    /// <summary>
    /// A generic response contract for paginated data.
    /// </summary>
    public class PaginatedResponse
    {
        /// <summary>
        /// The paginated data.
        /// </summary>
        public IEnumerable<object> Data { get; set; }

        /// <summary>
        /// Indicates the total number of elements in the collection, before pagination was applied.
        /// </summary>
        public int TotalElements { get; set; }

        /// <summary>
        /// Creates a paginated data response.
        /// </summary>
        /// <param name="data">The paginated data.</param>
        /// <param name="totalElements">The total element count.</param>
        public PaginatedResponse(IEnumerable<object> data, int totalElements)
        {
            Data = data;
            TotalElements = totalElements;
        }
    }
}
