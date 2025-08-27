namespace InsuranceQuoteService.Domain.Models
{
    public sealed record PageModel<TResult>
    {
        public PageModel(long records, IEnumerable<TResult> result, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Result = result;
            PagesCount = CalculatePagesCount(records, pageSize);
            Total = CalculateTotal(records);
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PagesCount { get; }

        public object Total { get; }

        public IEnumerable<TResult> Result { get; }

        #region Private Methods

        private static int CalculatePagesCount(long records, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentException("O pageSize deve ser maior que zero.", nameof(pageSize));

            return ((int)records / pageSize) + ((records % pageSize == 0) ? 0 : 1);
        }

        private static object CalculateTotal(long records)
        {
            return (records < 1000) ? records : $"{records}";
        }

        #endregion Private Methods
    }
}