using System.Collections.Generic;

namespace Lob.Net.Models
{
    public class LetterFilter : ItemFilter
    {
        public bool? Color { get; set; }

        internal override IDictionary<string, string> GetFilterDictionary()
        {
            var dict = base.GetFilterDictionary();

            if (Color.HasValue)
            {
                dict["color"] = Color.Value ? "true" : "false";
            }

            return dict;
        }
    }
}
