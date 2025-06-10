namespace BounceBall.Models
{
    public partial class User
    {
        public class RequestDto<T>
        {
            public T Filter { get; set; }
            public int PageIndex { get; set; } = 0;
            public int PageSize { get; set; } = 10;
        }

    }
}
