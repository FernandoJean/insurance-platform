namespace InsuranceQuoteService.Domain.Models
{
    public sealed record Pagination
    {
        public Pagination(int? pageIndex, int? pageSize)
        {
            var index = pageIndex ?? 0;
            PageIndex = index < 0 ? 0 : index;

            var size = pageSize ?? 10;
            PageSize = size < 1 ? 10 : size;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}