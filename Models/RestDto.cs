namespace BounceBall.Models
{
    public partial class User
    {
        public class RestDto<T>
        {
            public T Data { get; set; }
            public int RecordCount { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }

    }
}
