using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Lob.Net.Models
{
    abstract public class ItemFilter : BaseFilter
    {
        public bool? Scheduled { get; set; }
        public DateTime? SendAfter { get; set; }
        public DateTime? SendBefore { get; set; }
        public MailType? MailType { get; set; }
        public ListSortBy SortBy { get; set; }

        internal override IDictionary<string, string> GetFilterDictionary()
        {
            var dict = base.GetFilterDictionary();

            if (Scheduled.HasValue)
            {
                dict["scheduled"] = Scheduled.Value ? "true" : "false";
            }

            if (SendAfter.HasValue || SendBefore.HasValue)
            {
                var obj = new
                {
                    gt = SendAfter?.ToString("o"),
                    lt = SendBefore?.ToString("o")
                };
                dict["send_date"] = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }

            if (MailType.HasValue)
            {
                // TODO this is mouthful
                dict["mail_type"] = MailType.Value == Models.MailType.UspsFirstClass ? "usps_first_class" : "usps_standard";
            }

            if (SortBy != null)
            {
                var sortByKey = SortBy.SortBy == Models.SortBy.DateCreated ? "date_created" : "send_date";
                dict[$"sort_by[{sortByKey}]"] = SortBy.SortDirection == SortDirection.Ascending ? "asc" : "desc";
            }

            return dict;
        }
    }
}
