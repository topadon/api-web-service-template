using API.Gateway.App_Start;
using Model.Base;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace API.Gateway.Controllers
{
    public class BaseController : ApiController
    {
        private string _baseAddress = System.Configuration.ConfigurationManager.AppSettings["APIMainURL"];
        private string baseAddress
        {
            set { _baseAddress = value; }
            get { return _baseAddress; }
        }
        private string _isEncryption = System.Configuration.ConfigurationManager.AppSettings["isEncryption"];
        private string isEncryption
        {
            set { _isEncryption = value; }
            get { return _isEncryption; }
        }

        protected string apiPathAndQuery { get; set; }
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            HttpRequestMessage request = controllerContext.Request;
            var controller = request.GetRouteData().Values["controller"];
            var acction = request.GetRouteData().Values["action"];
            var RequestURLParams = request.RequestUri.Query;

            apiPathAndQuery = string.Format("/api/v1/{0}/{1}{2}", controller, acction, RequestURLParams);
        }

        protected T GetDataFromAPINotAuthen<T>(string pathAndQuery) where T : new()
        {
            T ObjReturn = new T();
            bool isSuccess = false;
            var _obj = GetObjFormAPI<T>(ref isSuccess, baseAddress, pathAndQuery);
            if (_obj != null && _obj is T)
            {
                ObjReturn = _obj;
            }
            else if (_obj != null && _obj is SystemException<string>)
            {
                ObjReturn = _obj;
            }
            return ObjReturn;
        }

        protected T PostDataToAPINotAuth<T>(string _requstUri, object objPost) where T : new()
        {
            T ObjReturn = new T();
            bool isSuccess = false;
            var _obj = PostObjToAPI<T>(ref isSuccess, objPost, baseAddress, _requstUri);
            if (_obj != null && _obj is T)
            {
                ObjReturn = _obj;
            }
            else if (_obj != null && _obj is SystemException<string>)
            {
                ObjReturn = _obj;
            }
            return ObjReturn;
        }

        protected T GetObjFormAPI<T>(ref bool isSuccess, string baseAddress, string requstUri) where T : new()
        {
            T objResult;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(AppConfig.RequestTimeout);
                    requstUri = baseAddress + requstUri;
                    HttpResponseMessage _response = client.GetAsync(requstUri).Result;

                    if (_response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }

                    objResult = _response.Content.ReadAsAsync<T>().Result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objResult;
        }

        protected T PostObjToAPI<T>(ref bool isSuccess, object obj, string baseAddress, string requstUri) where T : new()
        {
            T objResult;

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(AppConfig.RequestTimeout);
                    requstUri = baseAddress.TrimEnd('/') + requstUri;
                    var _response = client.PostAsync(requstUri, new StringContent(JsonConvert.SerializeObject(obj).ToString(), Encoding.UTF8, "application/json")).Result;

                    if (_response.IsSuccessStatusCode)
                    {
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }

                    objResult = _response.Content.ReadAsAsync<T>().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objResult;
        }
    }
}