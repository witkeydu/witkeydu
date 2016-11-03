using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using WitKeyDu.Site;
using System.ComponentModel.Composition;
using WitKeyDu.Site.Models;
using System.ComponentModel.Composition.Hosting;
using WitKeyDu.Site.Web;

namespace WebUploadTest
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
   [Export]
    public class Handler1 : IHttpHandler
    {
        #region 文件分片
       
        [Import]
        IFileResourceSiteContract FileResourceSiteContract  { get; set; }

        public void ProcessRequest(HttpContext context)
        {

            new CompositionContainer(MvcApplication.catalog).ComposeParts(this);
            context.Response.ContentType = "text/plain";
            string fileCode = context.Request["fileCode"];
            //如果进行了分片
            if (context.Request.Form.AllKeys.Any(m => m == "chunk"))
            {
                //取得chunk和chunks
                int chunk = Convert.ToInt32(context.Request.Form["chunk"]);
                int chunks = Convert.ToInt32(context.Request.Form["chunks"]);
                //根据GUID创建用该GUID命名的临时文件
                string path = context.Server.MapPath("~/Content/UploadFile/FileResource/" + context.Request["guid"]);
                FileStream addFile = new FileStream(path, FileMode.Append, FileAccess.Write);
                BinaryWriter AddWriter = new BinaryWriter(addFile);
                //获得上传的分片数据流
                HttpPostedFile file = context.Request.Files[0];
                Stream stream = file.InputStream;
                BinaryReader TempReader = new BinaryReader(stream);
                //将上传的分片追加到临时文件末尾
                AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
                //关闭BinaryReader文件阅读器
                TempReader.Close();
                stream.Close();
                AddWriter.Close();
                addFile.Close();
                TempReader.Dispose();
                stream.Dispose();
                AddWriter.Dispose();
                addFile.Dispose();
                //如果是最后一个分片，则重命名临时文件为上传的文件名
                if (chunk == (chunks - 1))
                {
                    //1.保存物理文件
                    FileInfo fileinfo = new FileInfo(path);
                    fileinfo.MoveTo(context.Server.MapPath("~/Content/UploadFile/FileResource/" +context.Request.Files[0].FileName));
                    //2.文件和上传者的信息绑定，并且存入数据库
                FileResourceView fileResource = new FileResourceView
                {
                    FileCode=fileCode,
                    FileSrc="~/Content/UploadFile/FileResource/" +context.Request.Files[0].FileName
                };
                FileResourceSiteContract.ReleaseFileResource(fileResource);
                }
            }
            else//没有分片直接保存
            {
                context.Request.Files[0].SaveAs(context.Server.MapPath("~/Content/UploadFile/FileResource/" +context.Request.Files[0].FileName));
                FileResourceView fileResource = new FileResourceView
                {
                    FileCode=fileCode,
                    FileSrc="~/Content/UploadFile/FileResource/" +context.Request.Files[0].FileName
                };
                FileResourceSiteContract.ReleaseFileResource(fileResource);
            }
            context.Response.Write("ok");
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
         
            }
        }
    }
}