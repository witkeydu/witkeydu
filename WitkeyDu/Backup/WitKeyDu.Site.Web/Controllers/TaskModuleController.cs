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
using WitKeyDu.Core.Models.Tasks;
using Webdiyer.WebControls.Mvc;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Site.Web.Controllers
{
    [Export]
    public class TaskModuleController : Controller
    {
        //
        // GET: /Tasks/
        #region
        [Import]
        ITaskModuleSiteContract TaskModuleSiteContract { get; set; }

        [Import]//最好换成autofac，不要  MEF
        public ITaskTypeSiteContract TaskTypeContract { get; set; }

        [Import]
        public ITaskPlanSiteContract TaskPlanSiteContract { get; set; }

        [Import]//最好换成autofac，不要  MEF
        public IPlanPaceSiteContract PlanPaceSiteContract { get; set; }

        [Import]
        IStoreModuleSiteContract StoreModuleSiteContract { get; set; }

        private static List<PlanPaceView> planList = new List<PlanPaceView>();

        private static string imgurl = "";

        #endregion
        #region 发布任务视图
        [HttpGet]
        public ActionResult ReleaseTask()
        {

            var categoryList = GetCategoryList(0);
            ViewData["categoryList"] = categoryList;
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("ReleaseTask", "TaskModule");
            ReleaseTaskModel model = new ReleaseTaskModel
            {
                ReturnUrl = returnUrl,
                TaskStartTime=DateTime.Now,
                TaskEndTime=DateTime.Now.AddDays(15),
            };
            return View(model);
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
        public ActionResult ReleaseTask(ReleaseTaskModel model)
        {
            try
            {
                model.TaskLogo = imgurl;
                OperationResult result = TaskModuleSiteContract.ReleaseTask(model);

                string msg = result.Message ?? result.ResultType.ToDescription();

                if (result.ResultType == OperationResultType.Success)
                {
                    return Redirect(model.ReturnUrl);
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
        public ActionResult TaskList(int? id)
        {
            var categoryList = GetCategoryList(0);
            ViewData["categoryList"] = categoryList;

            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            if (Request["TaskTypeID"] != null)
            {
                int TaskTypeID = int.Parse(Request["TaskTypeID"]);
                ViewBag.TaskTypeName = Request["TaskTypeName"];
                var taskListViews = TaskModuleSiteContract.Tasks.Where<Task, int>(m => m.TaskState != "已完成" && m.TaskTypeID == TaskTypeID, pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new TaskListView
                {
                    Id = m.Id,
                    TaskLogo = m.TaskLogo,
                    TaskName = m.TaskName,
                    TaskReward = m.TaskReward,
                    TaskStartTime = m.TaskStartTime,
                    TaskEndTime = m.TaskEndTime,
                    DayNum = m.DayNum,
                    TaskState = m.TaskState
                });
                PagedList<TaskListView> model = new PagedList<TaskListView>(taskListViews, pageIndex, pageSize, total);
                return View(model);
            }
            else
            {
                var taskListViews = TaskModuleSiteContract.Tasks.Where<Task, int>(m => m.TaskState != "已完成", pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new TaskListView
                {
                    Id = m.Id,
                    TaskLogo = m.TaskLogo,
                    TaskName = m.TaskName,
                    TaskReward = m.TaskReward,
                    TaskStartTime = m.TaskStartTime,
                    TaskEndTime = m.TaskEndTime,
                    DayNum = m.DayNum,
                    TaskState = m.TaskState
                });
                ViewBag.TaskTypeName = "选择类型";
                PagedList<TaskListView> model = new PagedList<TaskListView>(taskListViews, pageIndex, pageSize, total);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult TaskDetail()
        {
            int idValue = int.Parse(Request["TaskID"]);
            Task task = TaskModuleSiteContract.Tasks.SingleOrDefault(m => m.Id == idValue);
            TaskView taskDetail = new TaskView();
            ViewBag.ID = task.Id;
            ViewBag.TaskReward = task.TaskReward;
            ViewBag.TaskName = task.TaskName;
            ViewBag.TaskStartTime = task.TaskStartTime;
            ViewBag.TaskEndTime = task.TaskEndTime;
            ViewBag.TaskLogo = task.TaskLogo;
            ViewBag.TaskContent = task.TaskContent;
            ViewBag.TaskState = task.TaskState;
            var taskPlan = TaskPlanSiteContract.TaskPlans.Where(m => m.TaskID == idValue).Select(m=>new TaskPlanView{
                 ID=m.Id,
                 SystemUserID=m.SystemUserID,
            });
            List<StoreView> storeList = new List<StoreView>();
            foreach (TaskPlanView model in taskPlan)
            {
               Store store = StoreModuleSiteContract.Stores.SingleOrDefault(m => m.SystemUserID == model.SystemUserID);
               StoreView  storemodel =new StoreView();
               storemodel.taskPlanId = model.ID;
               storemodel.Id=store.Id;
               storemodel.StoreLogo=store.StoreLogo;
               storemodel.StoreName=store.StoreName;
               storemodel.StoreContract = store.StoreContract;
               storeList.Add(storemodel);
            }
            return View(storeList);
        }

        [HttpGet]
        public ActionResult TaskPlan()
        {
            TaskPlanView model = new TaskPlanView();
            model.TaskID = int.Parse(Request["taskID"]);
            Task task = TaskModuleSiteContract.Tasks.SingleOrDefault(m => m.Id == model.TaskID);
            if (task.TaskState != "招标中")
            {
                return Redirect("TaskDetail?TaskID=" + model.TaskID + "");
            }
            model.PlanStartTime = DateTime.Now;
            model.PlanEndTime = DateTime.Now.AddDays(2);
            return View(model);
        }
        [HttpPost]
        public ActionResult ReleasePanPace(PlanPaceView model)
        {
            PlanPaceView plan = new PlanPaceView
            {
                PlanStartTime = model.PlanStartTime,
                Remark = model.Remark,
                CompleteContent = model.CompleteContent,
                PlanEndTime = model.PlanEndTime
            };
            planList.Add(plan);
            var list = " <div>";
            foreach (PlanPaceView view in planList)
            {
                list += "<div class='plan-module col-md-12 col-xs-12 col-xm-12'><div class='pace-border'><div class='pace-width'>" + view.CompleteContent + "</div><div class='pace-width'> 备注信息：" + view.Remark + "</div><div class='pace-width text-right'>" + view.PlanStartTime.ToLongDateString().ToString() + "-" + view.PlanEndTime.ToLongDateString().ToString() + "</div></div></div>";
            }
            list += "</div>";
            return Json(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TaskPlan(TaskPlanView model)
        {
            try
            {
                SystemUser user=((SystemUser)Session["SystemUser"]);
                Store store = StoreModuleSiteContract.Stores.SingleOrDefault(m => m.SystemUserID == user.Id);
                if (store != null)
                {
                    model.TaskID = int.Parse(Request["TaskID"]);
                    model.PlanCode = Guid.NewGuid().ToString();
                    OperationResult result = TaskPlanSiteContract.ReleaseTaskPlan(model);

                    foreach (PlanPaceView view in planList)
                    {
                        view.PlanCode = model.PlanCode;
                        OperationResult releasePlanPaceResult = PlanPaceSiteContract.ReleasePlanPace(view);
                    }

                    string msg = result.Message ?? result.ResultType.ToDescription();
                    planList = new List<PlanPaceView>();
                    ModelState.AddModelError("", msg);
                    return View();
                }
                TaskPlanView taskPlan = new TaskPlanView();
                taskPlan.TaskID = int.Parse(Request["taskID"]);
                Task task = TaskModuleSiteContract.Tasks.SingleOrDefault(m => m.Id == taskPlan.TaskID);
                if (task.TaskState != "招标中")
                {
                    return Redirect("TaskDetail?TaskID=" + taskPlan.TaskID + "");
                }
                taskPlan.PlanStartTime = DateTime.Now;
                taskPlan.PlanEndTime = DateTime.Now.AddDays(2);
                return View(taskPlan);

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult TaskPlanDetail()
        {
            int taskPlanId = int.Parse(Request["TaskPlanId"]);
            TaskPlan taskPlan = TaskPlanSiteContract.TaskPlans.SingleOrDefault(m => m.Id == taskPlanId);
            ViewBag.ID = taskPlan.Id;
            ViewBag.PlanCode=taskPlan.PlanCode;
            ViewBag.SystemUserID = taskPlan.SystemUserID;
            ViewBag.PlanContent=taskPlan.PlanContent;
            ViewBag.PlanPrice=taskPlan.PlanPrice;
            int idValue = taskPlan.TaskID;
            Task task = TaskModuleSiteContract.Tasks.SingleOrDefault(m => m.Id == idValue);
            TaskView taskDetail = new TaskView();
            ViewBag.TaskID = task.Id;
            ViewBag.TaskReward = task.TaskReward;
            ViewBag.TaskName = task.TaskName;
            ViewBag.TaskState = task.TaskState;
            IQueryable<PlanPaceView> PlanPaceList = PlanPaceSiteContract.PlanPaces.Where(m => m.PlanCode == taskPlan.PlanCode).Select(m => new PlanPaceView
            {
                PlanStartTime = m.PlanStartTime,
                PlanEndTime = m.PlanEndTime,
                CompleteContent = m.CompleteContent,
                Remark = m.Remark
            });
            return View(PlanPaceList);
        }


        [HttpPost]
        public ActionResult SelectTaskPlan(int TaskID,int TaskPlanID,int SystemUserID)
        {
            Task task = TaskModuleSiteContract.Tasks.Single(m => m.Id == TaskID);
            task.Employer = SystemUserID; 
            task.TaskPlanID = TaskPlanID; 
            task.EmployeTime = DateTime.Now;
            task.TaskState = "开发中";
            TaskModuleSiteContract.UpdateTask(task);
            return View();
        }
        [HttpGet]
        public ActionResult FinishedTask(int? id)
        {
            var categoryList = GetCategoryList(0);
            ViewData["categoryList"] = categoryList;

            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            if (Request["TaskTypeID"] != null)
            {
                int TaskTypeID = int.Parse(Request["TaskTypeID"]);
                ViewBag.TaskTypeName = Request["TaskTypeName"];
                var taskListViews = TaskModuleSiteContract.Tasks.Where<Task, int>(m => m.TaskState == "已完成" && m.TaskTypeID == TaskTypeID, pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new TaskListView
                {
                    Id = m.Id,
                    TaskLogo = m.TaskLogo,
                    TaskName = m.TaskName,
                    TaskReward = m.TaskReward,
                    TaskStartTime = m.TaskStartTime,
                    TaskEndTime = m.TaskEndTime,
                    DayNum = m.DayNum,
                    TaskState = m.TaskState
                });
                PagedList<TaskListView> model = new PagedList<TaskListView>(taskListViews, pageIndex, pageSize, total);
                return View(model);
            }
            else
            {
                var taskListViews = TaskModuleSiteContract.Tasks.Where<Task, int>(m => m.TaskState == "已完成", pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new TaskListView
                {
                    Id = m.Id,
                    TaskLogo = m.TaskLogo,
                    TaskName = m.TaskName,
                    TaskReward = m.TaskReward,
                    TaskStartTime = m.TaskStartTime,
                    TaskEndTime = m.TaskEndTime,
                    DayNum = m.DayNum,
                    TaskState = m.TaskState
                });
                ViewBag.TaskTypeName = "选择类型";
                PagedList<TaskListView> model = new PagedList<TaskListView>(taskListViews, pageIndex, pageSize, total);
                return View(model);
            }
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
