namespace Model.Base
{
    public class SystemException<T> where T : class
    {
        public SystemException()
        {
            ResponseInfo<T> responseInfo = new ResponseInfo<T>();
        }

        public ResponseInfo<T> ResponseInfo { get; set; }
    }
}