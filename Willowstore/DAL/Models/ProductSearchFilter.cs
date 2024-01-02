namespace Willowstore.DAL.Models
{
    public class ProductSearchFilter
    {
        public enum SortByColumn { ReleaseDate };
        
        public enum SortDirection { Asc, Desc };

        public int Top { get; set; } = 24;

        public int? AuthorId { get; set; }

        public SortByColumn SortBy { get; set; } = SortByColumn.ReleaseDate;

        public SortDirection Direction = SortDirection.Desc;

        public int? CategoryId { get; set; } = null;
    }
}
