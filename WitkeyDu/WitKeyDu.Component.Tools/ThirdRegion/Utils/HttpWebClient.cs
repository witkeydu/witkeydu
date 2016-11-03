using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// Description: 简单Http客户端请求类
    /// </summary>
    public class HttpWebClient
    {
        #region //字段
        private WebClient webClient;
        private ArrayList arrRequestBytes;
        private WebHeaderCollection requestHeads;
        private ArrayList arrRequestBodys;

        private ArrayList arrTraceInfos;
        private Encoding encoding = Encoding.UTF8;
        private String boundary = String.Empty;
        private String requestUrl;
        private HttpWebResponseResult webResponseResult;
        private Boolean isMultipart;
        private Boolean isPost;
        private Boolean isTraceEnabled;
        #endregion

        #region //属性
        public WebHeaderCollection Headers
        {
            get { return requestHeads; }
        }
        public Boolean IsTraceEnabled
        {
            get{
                return isTraceEnabled;
            }
            set{
                isTraceEnabled = value;
            }

        }
        #endregion

        #region //构造方法
        public HttpWebClient()
        {
            ResetStatus();
        }
        #endregion

        #region //方法

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="requestUrl">请求url</param>
        /// <param name="responseText">响应</param>
        /// <returns></returns>
        public HttpWebResponseResult Get(String requestUrl)
        {
            this.isPost = false;
            this.requestUrl = requestUrl;
            this.webResponseResult = new HttpWebResponseResult();

            webClient = new WebClient();
            webClient.Proxy = null;

            #region //请求头部处理
            requestHeads.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
            foreach (var key in requestHeads.AllKeys)
            {
                webClient.Headers.Add(key, requestHeads.Get(key));
            }
            #endregion

            #region //请求数据
            byte[] responseBytes = null;
            String url = GetFields();
            #endregion

            #region //发送请求
            try
            {
                responseBytes = webClient.DownloadData(url);
                webResponseResult.IsSuccess = true;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    Stream responseStream = ex.Response.GetResponseStream();
                    responseBytes = new byte[responseStream.Length];
                    responseStream.Read(responseBytes, 0, responseBytes.Length);
                }
                else
                {
                    String errorMessage = ex.InnerException.Message;
                    responseBytes = encoding.GetBytes(errorMessage);
                }
                webResponseResult.IsSuccess = false;
            }
            #endregion

            webResponseResult.ResponseText = System.Text.Encoding.UTF8.GetString(responseBytes);
            webResponseResult.RequestTraceInfo = TraceInfo();

            ResetStatus();

            return webResponseResult;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="requestUrl">请求url</param>
        /// <param name="responseText">响应</param>
        /// <returns></returns>
        public HttpWebResponseResult Post(String requestUrl)
        {
            this.isPost = true;
            this.requestUrl = requestUrl;
            this.webResponseResult = new HttpWebResponseResult();

            webClient = new WebClient();
            webClient.Proxy = null;

            #region //请求头部处理
            requestHeads.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
            if (isMultipart)
            {
                requestHeads.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            }
            else
            {
                requestHeads.Add("Content-Type", "application/x-www-form-urlencoded");
            }

            foreach (var key in requestHeads.AllKeys)
            {
                webClient.Headers.Add(key, requestHeads.Get(key));
            }
            #endregion

            #region //请求数据处理
            byte[] responseBytes;
            byte[] bytes = null;
            if (isMultipart)
            {
                bytes = PostMultipartFields();
            }
            else
            {
                bytes = PostFields();
            }
            #endregion

            #region //发送请求
            try
            {
                responseBytes = webClient.UploadData(this.requestUrl, "POST", bytes);
                webResponseResult.IsSuccess = true;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    Stream responseStream = ex.Response.GetResponseStream();
                    responseBytes = new byte[responseStream.Length];
                    responseStream.Read(responseBytes, 0, responseBytes.Length);
                }
                else
                {
                    String errorMessage = ex.InnerException.Message;
                    responseBytes = encoding.GetBytes(errorMessage);
                }
                webResponseResult.IsSuccess = false;
            }
            #endregion

            webResponseResult.ResponseText = System.Text.Encoding.UTF8.GetString(responseBytes);
            webResponseResult.RequestTraceInfo = TraceInfo();

            ResetStatus();

            return webResponseResult;
        }


        /// <summary>
        /// 设置表单数据字段
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="isMultipart">是否多字段类型</param>
        /// <returns></returns>
        public void SetField(String fieldName, String fieldValue, Boolean isMultipart=false)
        {
            this.isMultipart = isMultipart;
            string httpRowData = String.Empty;

            if (this.isMultipart)
            {
                String httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                httpRowData = String.Format(httpRow, fieldName, fieldValue);
                arrRequestBytes.Add(encoding.GetBytes(httpRowData));
            }
            else
            {
                String httpRow = "{0}={1}";
                httpRowData = String.Format(httpRow, Uri.EscapeDataString(fieldName), Uri.EscapeDataString(fieldValue));
            }

            Trace(httpRowData);
        }

        /// <summary>
        /// 设置表单文件数据
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="filename">字段值</param>
        /// <param name="contentType">内容内型</param>
        /// <param name="fileBytes">文件字节流</param>
        /// <returns></returns>
        public void SetField(String fieldName, Byte[] fileBytes, String filename, String contentType)
        {
            isMultipart = true;
            if (String.IsNullOrEmpty(filename)) filename = Guid.NewGuid().ToString().Replace("-","");
            if (String.IsNullOrEmpty(contentType)) contentType = "application/octet-stream";

            string end = "\r\n";
            string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\nContent-Transfer-Encoding: binary\r\n\r\n";
            string httpRowData = String.Format(httpRow, fieldName, filename, contentType);
            
            byte[] headerBytes = encoding.GetBytes(httpRowData);
            byte[] endBytes = encoding.GetBytes(end);
            byte[] fileDataBytes = new byte[headerBytes.Length + fileBytes.Length + endBytes.Length];

            headerBytes.CopyTo(fileDataBytes, 0);
            fileBytes.CopyTo(fileDataBytes, headerBytes.Length);
            endBytes.CopyTo(fileDataBytes, headerBytes.Length + fileBytes.Length);

            arrRequestBytes.Add(fileDataBytes);

            Trace(httpRowData + "...binary data...");
        }
        #endregion

        #region //帮助方法
        /// <summary>
        /// Get请求数据
        /// </summary>
        /// <returns></returns>
        private String GetFields()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.requestUrl);
            if(arrRequestBodys.Count > 0){
                String qurey = String.Join("&", arrRequestBodys.ToArray());
                sb.AppendFormat("?{0}", qurey);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Post请求数据
        /// </summary>
        /// <returns></returns>
        private byte[] PostFields()
        {
            String postData = String.Join("&", arrRequestBodys.ToArray());
            byte[] bytes = encoding.GetBytes(postData);
            return bytes;
        }

        /// <summary>
        /// Post请求数据
        /// </summary>
        /// <returns></returns>
        private byte[] PostMultipartFields()
        {
            int length = 0;
            int readLength = 0;
            string endBoundary = "--" + boundary + "--\r\n";
            byte[] endBoundaryBytes = encoding.GetBytes(endBoundary);

            arrRequestBytes.Add(endBoundaryBytes);

            foreach (byte[] b in arrRequestBytes)
            {
                length += b.Length;
            }

            byte[] bytes = new byte[length];

            foreach (byte[] b in arrRequestBytes)
            {
                b.CopyTo(bytes, readLength);
                readLength += b.Length;
            }

            return bytes;
        }

        private String TraceInfo()
        {
            StringBuilder sbRaw = new StringBuilder();
            sbRaw.AppendFormat("==================== REQUEST BEGIN ====================\r\n");
            sbRaw.AppendFormat("++++++++++ Request Line Info ++++++++++ \r\n{0} {1} {2}\r\n", isPost ? "POST" : "GET", this.requestUrl, "HTTP/1.1");
            sbRaw.Append("++++++++++ Request Header Info ++++++++++ \r\n");
            foreach (var key in requestHeads.AllKeys)
            {
                sbRaw.AppendFormat("{0}={1}\r\n", key, requestHeads.Get(key));
            }

            sbRaw.Append("++++++++++ Request Body Info ++++++++++ \r\n");
            if (isPost)
            {
                if (isMultipart)
                {
                    foreach (var item in arrTraceInfos)
                    {
                        sbRaw.Append(item);
                    }
                }
                else
                {
                    sbRaw.Append(String.Join("&", arrTraceInfos.ToArray()));
                }
            }
            else
            {
                sbRaw.Append(String.Join("&", arrTraceInfos.ToArray()));
            }
            sbRaw.AppendFormat("\r\n==================== REQUEST END ====================\r\n");
            return sbRaw.ToString();
        }

        private void Trace(String line)
        {
            arrRequestBodys.Add(line);
            if (isTraceEnabled) arrTraceInfos.Add(line);
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        private void ResetStatus()
        {
            string flag = DateTime.Now.Ticks.ToString("x");
            this.boundary = "---------------------------" + flag;

            this.arrRequestBytes = new ArrayList();
            this.requestHeads = new WebHeaderCollection();
            this.arrRequestBodys = new ArrayList();
            this.arrTraceInfos = new ArrayList();
            this.isMultipart = false;
            this.isPost = false;
        }
        #endregion
    }

    public class HttpWebResponseResult
    {
        #region 属性
        public String ResponseText { get; set; }
        public String RequestTraceInfo{get;set;}
        public Boolean IsSuccess { get; set; }
        #endregion

        public HttpWebResponseResult()
        {
            IsSuccess = false;
        }
    }
}
