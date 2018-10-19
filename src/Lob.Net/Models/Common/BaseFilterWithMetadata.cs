using System.Collections.Generic;

namespace Lob.Net.Models
{
    abstract public class BaseFilterWithMetadata : BaseFilter
    {
        public IDictionary<string, string> Metadata { get; set; }

        internal override IDictionary<string, string> GetFilterDictionary()
        {
            var dict = base.GetFilterDictionary();

            if (Metadata?.Keys.Count > 0)
            {
                foreach (var kvp in Metadata)
                {
                    dict[$"metadata[{kvp.Key}]"] = kvp.Value;
                }
            }

            return dict;
        }
    }
}
