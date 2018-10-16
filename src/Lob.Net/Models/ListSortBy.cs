namespace Lob.Net.Models
{
    public class ListSortBy
    {
        public SortBy SortBy { get; }
        public SortDirection SortDirection { get; }

        public ListSortBy(SortBy sortBy, SortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }
    }
}
