namespace BooXtation.Helpers
{
    public class PaginationData<T>
    {
        public int PageIndex { get; set; }//4
        public int PageSize { get; set; } // 100 , 100 , 100 , 20
        public int Count { get; set; } //Count of all data in backEnd (DB) not in the list 
        public IReadOnlyList<T> Data { get; set; }

    }
}
