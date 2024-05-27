

namespace TailorProTrack.Application.Core
{
    public class PaginationMetaData
    {

        public PaginationMetaData(int totalCount, int currentPage, int itemsPerPage )
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)itemsPerPage);
            HasNext = CurrentPage < TotalPages;
            HasPrevious = (CurrentPage > 1 && CurrentPage <= TotalPages);
        }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious {  get; set; }
    }
}
