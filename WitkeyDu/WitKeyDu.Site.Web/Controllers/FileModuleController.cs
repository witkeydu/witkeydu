using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.Security;
using WitKeyDu.Site.Models;
using System.ComponentModel.Composition;
using WitKeyDu.Component.Tools;
using WitKeyDu.Component.Tools.Extensions;
using WitKeyDu.Core.Models.Files;
using Webdiyer.WebControls.Mvc;
using System.IO;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Site.Web.Controllers
{
    [Export]
    public class FileModuleController : Controller
    {
        //
        // GET: /UploadFile/
        #region  文件属性、导入接口对象
        [Import]
        IFileModuleSiteContract FileModuleSiteContract { get; set; }

        [Import]
        public IAccountSiteContract AccountContract { get; set; }

        [Import]
        IFileResourceSiteContract FileResourceSiteContract { get; set; }

        [Import]
        public IFileTypeSiteContract FileTypeContract { get; set; }
        List<FileTypeView> ForumTypeView = new List<FileTypeView>();
        #endregion
        #region 文件视图

        /// <summary>
        /// 分享文件请求页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReleaseFile()
        {
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("ReleaseFile", "ForuFile");
            FileView model = new FileView
            {
                FileCode=Guid.NewGuid().ToString(),
                ReturnUrl = returnUrl,
            };
            return View(model);
        }

        /// <summary>
        /// 文件分享处理数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReleaseFile(FileView model)
        {
            OperationResult result = FileModuleSiteContract.ReleaseFile(model);
            string msg = result.Message ?? result.ResultType.ToDescription();
            if (result.ResultType == OperationResultType.Success)
            {
                return RedirectToAction("ReleaseFile", "FileModule");
            }
            ModelState.AddModelError("", msg);
            return View();
        }

        /// <summary>
        /// 文件列表请求页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileList(int? id)
        {
            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            IQueryable fileModel = FileModuleSiteContract.Files.Where<WitKeyDu.Core.Models.Files.File, int>(m => true, pageIndex, pageSize, out total, sortConditions); 
            List<FileView> fileList = new List<FileView>();
            foreach (WitKeyDu.Core.Models.Files.File m in fileModel)
            {
                SystemUser  SysUser=AccountContract.SysUsers.FirstOrDefault(user=>user.Id==m.SystemUserID);
                FileView file = new FileView
                {
                     FileCode=m.FileCode,
                     FileName=m.FileName,
                     FileIntroduction=m.FileIntroduction,
                     HeadImg=SysUser.HeadImage,
                     UserName=SysUser.UserName,
                     UserKey=SysUser.Id   
                };
                fileList.Add(file);
            }
            PagedList<FileView> model = new PagedList<FileView>(fileList, pageIndex, pageSize, total);
            return View(model);
        }
        /// <summary>
        /// 文件详情请求页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileDetail(FileView model)
        {
            string fileCode = Request["fileCode"].ToString();
            WitKeyDu.Core.Models.Files.File file = FileModuleSiteContract.Files.FirstOrDefault(m => m.FileCode == fileCode);
            SystemUser SysUser = AccountContract.SysUsers.FirstOrDefault(user => user.Id == file.SystemUserID);
            FileView fileInfo = new FileView { 
                FileCode = file.FileCode, 
                FileIntroduction = file.FileIntroduction, 
                FileName = file.FileName,
                UserKey = file.SystemUserID,
                HeadImg=SysUser.HeadImage,
                UserName=SysUser.UserName, 
            };
            ViewBag.fileInfo = fileInfo;
            List<FileResource> fileResourceInfo=FileResourceSiteContract.FileResources.Where(m => m.FileCode == fileCode).ToList();
            List<FileResourceView> fileResourceInfoView = new List<FileResourceView>();
            foreach (FileResource fileitem in fileResourceInfo)
            {
                FileResourceView fileResource = new FileResourceView
                {
                    FileCode = fileitem.FileCode,
                    FileSrc = fileitem.FileSrc,
                    FileName = SplitChar(fileitem.FileSrc)
                };
                fileResourceInfoView.Add(fileResource);
            }
            return View(fileResourceInfoView);
        }

        public string SplitChar(string splitParam)
        {
            string[] para = splitParam.Split('/');
            return para[para.Length - 1];
        }

        /// <summary>
        /// 使用OutputStream.Write分块下载文件  
        /// </summary>
        /// <param name="filePath"></param>
        public ActionResult DownFile(string filePath, string fileName)
        {
            filePath = Server.MapPath(filePath);
            if (!System.IO.File.Exists(filePath))
            {
                return View();
            }
            FileInfo info = new FileInfo(filePath);
            //指定块大小   
            long chunkSize = 4096;
            //建立一个4K的缓冲区   
            byte[] buffer = new byte[chunkSize];
            //剩余的字节数   
            long dataToRead = 0;
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                dataToRead = stream.Length;

                //添加Http头   
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + Server.UrlEncode(fileName));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (System.Web.HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        System.Web.HttpContext.Current.Response.Flush();
                        System.Web.HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //防止client失去连接   
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                System.Web.HttpContext.Current.Response.Close();
            }
            return View();

        }
        #endregion
    }
}