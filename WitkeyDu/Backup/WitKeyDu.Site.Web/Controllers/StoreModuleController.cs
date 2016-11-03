using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using WitKeyDu.Component.Tools;
using WitKeyDu.Component.Tools.Extensions;
using WitKeyDu.Site.Models;
using Webdiyer.WebControls.Mvc;
using WitKeyDu.Core.Models.Stores;

namespace WitKeyDu.Site.Web.Controllers
{
    [Export]
    public class StoreModuleController : Controller
    {
        //
        // GET: /StoreModule/
        #region
        [Import]
        IStoreModuleSiteContract StoreModuleSiteContract { get; set; }
        [Import]
        IProjectSiteContract ProjectSiteContract { get; set; }
        [Import]//最好换成autofac，不要  MEF
        public ITaskTypeSiteContract TaskTypeContract { get; set; }
        private static string imgurl = "";
        #endregion
        #region 店铺信息
        public ActionResult CreateStore()
        {
            return View();
        }

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
        public ActionResult CreateStore(StoreView model)
        {
            try
            {
                model.StoreLogo = imgurl;
                OperationResult result = StoreModuleSiteContract.ReleaseStore(model);

                string msg = result.Message ?? result.ResultType.ToDescription();

                if (result.ResultType == OperationResultType.Success)
                {
                    return View();
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
        public ActionResult StoreList(int? id)
        {
            int pageIndex = id ?? 1;
            const int pageSize = 100;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            IQueryable storeModel = StoreModuleSiteContract.Stores.Where<WitKeyDu.Core.Models.Stores.Store, int>(m => true, pageIndex, pageSize, out total, sortConditions);
            List<StoreView> storeList = new List<StoreView>();
            foreach (WitKeyDu.Core.Models.Stores.Store m in storeModel)
            {
                StoreView store = new StoreView
                {
                     Id=m.Id,
                     StoreLogo=m.StoreLogo,
                     StoreContent=m.StoreContent,
                     StoreContract=m.StoreContract,
                     StoreName=m.StoreName
                };
                storeList.Add(store);
            }
            PagedList<StoreView> model = new PagedList<StoreView>(storeList, pageIndex, pageSize, total);
            return View(model);
        }

        [HttpGet]
        public ActionResult StoreDetail(int? id)
        {
            int StoreID = int.Parse(Request["StoreID"]);
            Store store = StoreModuleSiteContract.Stores.Single(m => m.Id == StoreID);
            StoreView storemodel = new StoreView();
            ViewBag.Id = store.Id;
            ViewBag.StoreLogo = store.StoreLogo;
            ViewBag.StoreName = store.StoreName;
            ViewBag.StoreContract = store.StoreContract;
            ViewBag.StoreContent = store.StoreContent;
            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            var projectList = ProjectSiteContract.Projects.Where<Project, int>(m => m.IsDeleted == false, pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new ProjectView
            {
                ID = m.Id,
                ProjectPrice = m.ProjectPrice,
                ProjectLogo = m.ProjectLogo,
                ProjectName = m.ProjectName,
                ShowUrl=m.ShowUrl
            });
            PagedList<ProjectView> model = new PagedList<ProjectView>(projectList, pageIndex, pageSize, total);
            return View(model);
        }
        
        [HttpGet]
        public ActionResult ReleaseProject()
        {
            var categoryList = GetCategoryList(0);
            ViewData["categoryList"] = categoryList;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ReleaseProject(ProjectView model)
        {
            try
            {
                model.ProjectLogo = imgurl;
                OperationResult result = ProjectSiteContract.ReleaseProject(model);

                string msg = result.Message ?? result.ResultType.ToDescription();

                if (result.ResultType == OperationResultType.Success)
                {
                    return Redirect("ReleaseProject");
                }

                var categoryList = GetCategoryList(0);
                ViewData["categoryList"] = categoryList;
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
        public ActionResult ProjectDetail()
        {
            int idValue = int.Parse(Request["ProjectID"]);
            Project project = ProjectSiteContract.Projects.SingleOrDefault(m => m.Id == idValue);
            TaskView taskDetail = new TaskView();
            ViewBag.ID = project.Id;
            ViewBag.ProjectPrice = project.ProjectPrice;
            ViewBag.ProjectName = project.ProjectName;
            ViewBag.ProjectLogo = project.ProjectLogo;
            ViewBag.ProjectIntroduction = project.ProjectIntroduction;
            ViewBag.ShowUrl = project.ShowUrl;
            return View();
        }
        [HttpPost]
        public ActionResult ProjectDetail(int i)
        {
            return View();
        }
        #endregion
        public List<TaskTypeView> GetCategoryList(int Id)
        {
            List<TaskTypeView> uvModel = new List<TaskTypeView>();


            var perentList = TaskTypeContract.TaskTypes.Where(t => t.TaskParentTypeID == Id).ToList();

            if (perentList.Count > 0)
            {
                foreach (var item in perentList)
                {
                    TaskTypeView userViewModel = new TaskTypeView
                    {

                        TaskTypeID = item.Id,
                        TaskTypeName = item.TaskTypeName,
                        ChildrenTaskType = new List<TaskTypeView>()
                    };
                    List<TaskTypeView> tempList = GetCategoryList(item.Id);
                    if (tempList.Count > 0)
                    {
                        userViewModel.ChildrenTaskType = tempList;
                    }
                    uvModel.Add(userViewModel);
                }
            }
            return uvModel;
        }
    }
}
