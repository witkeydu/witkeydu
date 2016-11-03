using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using WitKeyDu.Component.Tools.Extensions;
using WitKeyDu.Site.Impl;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Forums;
using System.Text;
using System.Text.RegularExpressions;
using Webdiyer.WebControls.Mvc;

namespace WitKeyDu.Site.Web.Controllers
{
    [Export]
    public class ForumModuleController : Controller
    {
        //
        // GET: /Tasks/
        #region
        [Import]
        IForumModuleSiteContract ForumModuleSiteContract { get; set; }

        [Import]
        public IForumTypeSiteContract ForumTypeSiteContract { get; set; }
        [Import]
        public IForumCommentSiteContract ForumCommentSiteContract { get; set; }


        List<ForumTypeView> ForumTypeView = new List<ForumTypeView>();
        #endregion
        #region 发布帖子视图
        [HttpGet]
        public ActionResult ReleaseForum()
        {
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("ReleaseForum", "ForumModule");
            var perentList = ForumTypeSiteContract.ForumTypes.ToList();

            if (perentList.Count > 0)
            {
                foreach (var item in perentList)
                {
                    ForumTypeView ForumTypeModel = new ForumTypeView
                      {
                           ForumTypeID=item.Id,
                           ForumTypeName=item.ForumTypeName,
                           ForumParentTypeID=item.ForumParentTypeID,
                           ForumTypeLogo=item.ForumTypeLogo
                      };
                    ForumTypeView.Add(ForumTypeModel);
                }
            }


            //为前台页面DropDownList准备的数据  
            List<SelectListItem> SelectItems = new List<SelectListItem>();
            SelectItems.Add(new SelectListItem() { Text = "请选择", Value = "-1", Selected = true });

            foreach (ForumTypeView forumType in ForumTypeView)
            {
                SelectListItem item = new SelectListItem();
                item.Text = forumType.ForumTypeName;
                item.Value = forumType.ForumTypeID.ToString();
                item.Selected = false;
                SelectItems.Add(item);
            }
            ViewData["ForumTypeID"] = SelectItems;
            ReleaseForumModel model = new ReleaseForumModel
            {
                ReturnUrl = returnUrl,
            };
            return View(model);
        }

        private static string imgurl = "";
        [HttpPost]
        public ActionResult UploadAction()
        {
            //取服务器时间+8位随机码作为部分文件名，确保文件名无重复。
            string fileName = Guid.NewGuid().ToString();
            //遍历所有文件域
            foreach (string fieldName in Request.Files.AllKeys)
            {
                HttpPostedFileBase file = Request.Files[fieldName];
                string virtualPath = string.Format("~/Images/HeadImage/{0}.jpg", fileName);
                imgurl = string.Format("../../Images/HeadImage/{0}.jpg", fileName);
                file.SaveAs(Server.MapPath(virtualPath));
            }

            OperationResultType result = OperationResultType.Success;
            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReleaseForum(ReleaseForumModel model)
        {
            try
            {
                var perentList = ForumTypeSiteContract.ForumTypes.ToList();

                if (perentList.Count > 0)
                {
                    foreach (var item in perentList)
                    {
                        ForumTypeView ForumTypeModel = new ForumTypeView
                        {
                            ForumTypeID = item.Id,
                            ForumTypeName = item.ForumTypeName,
                            ForumParentTypeID = item.ForumParentTypeID,
                            ForumTypeLogo = imgurl
                        };
                        ForumTypeView.Add(ForumTypeModel);
                    }
                }
                List<SelectListItem> SelectItems = new List<SelectListItem>();
                foreach (ForumTypeView forumType in ForumTypeView)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = forumType.ForumTypeName;
                    item.Value = forumType.ForumTypeID.ToString();
                    item.Selected = false;
                    SelectItems.Add(item);
                }
                ViewData["ForumTypeID"] = SelectItems;
                model.ForumTypeID = Convert.ToInt32(Request["ForumTypeID"]);
                model.ForumLogo = imgurl;
                IndexManager.bookIndex.Mod(model);
                OperationResult result = ForumModuleSiteContract.ReleaseFroum(model);
                string msg = result.Message ?? result.ResultType.ToDescription();
                if (result.ResultType == OperationResultType.Success)
                {
                    return RedirectToAction("ReleaseForum", "ForumModule");
                }

                ModelState.AddModelError("", msg);
                return View();
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", e.Message);
                return View();
            }
        }


        [HttpGet]
        public ActionResult ForumList(int? id)
        {
            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            IQueryable forumList = ForumModuleSiteContract.Forums.Where<Forum, int>(m => true, pageIndex, pageSize, out total, sortConditions);
            List<ForumListView> forumListViews = new List<ForumListView>();
            foreach (Forum m in forumList)
            {
                ForumListView forum = new ForumListView
                {
                    Id = m.Id,
                    ForumLogo = m.ForumLogo,
                    ForumContent = GetContentSummary(m.ForumContent, 200, true),
                    ForumName = m.ForumName,
                    AddDate = m.AddDate,
                    ForumComment = ForumCommentSiteContract.ForumComments.Where(n=>n.ForumID==m.Id).Count(),
                    ForumPraise=0
                };
                forumListViews.Add(forum);
            }
            PagedList<ForumListView> model = new PagedList<ForumListView>(forumListViews, pageIndex, pageSize, total);
            return View(model);
        }
        /// <summary>

        /// 提取摘要，是否清除HTML代码

        /// </summary>

        /// <param name="content"></param>

        /// <param name="length"></param>

        /// <param name="StripHTML">所返回摘要是否不包含Html标签：true不包含标签，false包含标签</param>

        /// <returns></returns>

        public string GetContentSummary(string content, int length, bool StripHTML)
        {
            if (string.IsNullOrEmpty(content) || length == 0)
                return "";
            if (StripHTML)
            {
                Regex re = new Regex("<[^>]*>");
                content = re.Replace(content, "");
                content = content.Replace("　", "").Replace(" ", "");
                if (content.Length <= length)
                    return content;
                else
                    return content.Substring(0, length) + "……";
            }
            else
            {
                if (content.Length <= length)

                    return content;
                int pos = 0, npos = 0, size = 0;
                bool firststop = false, notr = false, noli = false;
                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    if (pos >= content.Length)
                        break;
                    string cur = content.Substring(pos, 1);
                    if (cur == "<")
                    {
                        string next = content.Substring(pos + 1, 3).ToLower();
                        if (next.IndexOf("p") == 0 && next.IndexOf("pre") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            sb.Append("    ");
                        }
                        else if (next.IndexOf("/p") == 0 && next.IndexOf("/pr") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                                sb.Append("<br/>");
                        }
                        else if (next.IndexOf("br") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                                sb.Append("<br/>");
                        }
                        else if (next.IndexOf("img") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                                size += npos - pos + 1;
                            }
                        }
                        else if (next.IndexOf("li") == 0 || next.IndexOf("/li") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!noli && next.IndexOf("/li") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                    noli = true;
                                }
                            }
                        }
                        else if (next.IndexOf("tr") == 0 || next.IndexOf("/tr") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!notr && next.IndexOf("/tr") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                    notr = true;
                                }
                            }
                        }
                        else if (next.IndexOf("td") == 0 || next.IndexOf("/td") == 0)
                        {

                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));

                            }
                            else
                            {
                                if (!notr)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                }
                            }
                        }
                        else
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            sb.Append(content.Substring(pos, npos - pos));
                        }
                        if (npos <= pos)
                            npos = pos + 1;
                        pos = npos;
                    }
                    else
                    {
                        if (size < length)
                        {
                            sb.Append(cur);
                            size++;
                        }
                        else
                        {
                            if (!firststop)
                            {
                                sb.Append("……");
                                firststop = true;
                            }
                        }
                        pos++;
                    }
                }
                return sb.ToString();
            }

        }

        int idValue = 0;
        [HttpGet]
        public ActionResult ForumDetail(int? id)
        {
            idValue = int.Parse(Request["ForumID"]);
            Forum forum = ForumModuleSiteContract.Forums.SingleOrDefault(m => m.Id == idValue);
            ViewBag.ForumName = forum.ForumName;
            ViewBag.ForumContent = forum.ForumContent;
            ViewBag.ForumID = forum.Id;
            ViewData["commentInfo"] = GetCategoryList(0,id);
            return View();
        }


        public List<ForumCommentView> GetCategoryList(int Id,int?id)
        {
            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            List<ForumCommentView> commentModel = new List<ForumCommentView>();
            var commentList = ForumCommentSiteContract.ForumComments.Where<ForumComment,int>(m => m.ForumID == idValue && m.ParentCommentID == Id, pageIndex, pageSize, out total, sortConditions).ToList();

            if (commentList.Count > 0)
            {
                    foreach (var m in commentList)
                    {
                         ForumCommentView ViewModel = new ForumCommentView {
                         CommentContent=m.CommentContent,
                         CommentUserID=m.CommentUserID,
                         CommentUserName=m.CommentUserName,
                         ForumCommentID=m.Id,
                         UserName=m.UserName,
                         ForumID=m.ForumID,
                         HeadImage=m.HeadImage,
                         AndDate=m.AddDate,
                         ParentCommentID=m.ParentCommentID,
                         SystemUserID=m.SystemUserID,
                         ChildrenForumComment=new List<ForumCommentView>()
                     };
                    List<ForumCommentView> tempList = GetCategoryList(m.Id,id);
                    if (tempList.Count > 0)
                    {
                        ViewModel.ChildrenForumComment = tempList;
                    }
                    commentModel.Add(ViewModel);
                }
            }
            PagedList<ForumCommentView> model = new PagedList<ForumCommentView>(commentModel, pageIndex, pageSize, total);
            return model; 
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReleaseForumComment(ForumCommentView model)
        {
            try
            {
                idValue = int.Parse(Request["ForumID"]);
                OperationResult result = ForumCommentSiteContract.ReleaseFroumComment(model);
                string msg = result.Message ?? result.ResultType.ToDescription();
                if (result.ResultType == OperationResultType.Success)
                {
                    return Redirect("ForumDetail?ForumID=" + idValue + "");
                }

                ModelState.AddModelError("", msg);


                return Redirect("ForumDetail?ForumID=" + idValue + "");
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", e.Message);
                return Redirect("ForumDetail?ForumID=" + idValue + "");
            }
        }
        #endregion
    }
}
