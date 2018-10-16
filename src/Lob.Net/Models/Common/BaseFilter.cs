using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    abstract public class BaseFilter
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public bool IncludeTotalCount { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }

        internal virtual IDictionary<string, string> GetFilterDictionary()
        {
            var dict = new Dictionary<string, string>();
            if (Offset.HasValue)
            {
                dict["offset"] = Offset.Value.ToString();
            }

            if (Limit.HasValue)
            {
                dict["limit"] = Limit.Value.ToString();
            }

            if (IncludeTotalCount)
            {
                dict["include[]"] = "total_count";
            }

            if (Metadata?.Keys.Count > 0)
            {
                foreach (var kvp in Metadata)
                {
                    dict[$"metadata[{kvp.Key}]"] = kvp.Value;
                }
            }

            if (CreatedAfter.HasValue || CreatedBefore.HasValue)
            {
                var obj = new
                {
                    gt = CreatedAfter?.ToString("o"),
                    lt = CreatedBefore?.ToString("o")
                };
                dict["date_created"] = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }

            return dict;
        }
    }
}
