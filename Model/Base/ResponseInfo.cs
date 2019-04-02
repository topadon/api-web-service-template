namespace Model.Base
{
    public class ResponseInfo<T> where T : class
    {
        public string ResponseCode { get; set; } = "00";
        public string ResponseMsgTh { get; set; } = "ทำรายการสำเร็จ";
        public string ResponseMsgEn { get; set; } = "Success";
        public T ResponseData { get; set; }
    }
}