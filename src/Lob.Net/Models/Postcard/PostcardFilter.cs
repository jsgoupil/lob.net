using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class PostcardFilter : ItemFilter
    {
        public string Size { get; set; }

        internal override IDictionary<string, string> GetFilterDictionary()
        {
            var dict = base.GetFilterDictionary();

            if (Size != null)
            {
                dict["size"] = Size;
            }

            return dict;
        }
    }
}
