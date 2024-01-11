namespace TailorProTrack.Application.Core
{
    public class PaginationParams
    {
        private const int _maxItemsPerPage = 50;
        public int Page {  get; set; }
        private int _itemsPerPage;
        public int ItemsPerPage 
        { 
            get => _itemsPerPage; 
            set => _itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}
