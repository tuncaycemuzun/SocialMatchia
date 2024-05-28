namespace SocialMatchia.Common.Features.ResponseModel
{
    public class PaginationModel<T>
    {
        public T? Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
