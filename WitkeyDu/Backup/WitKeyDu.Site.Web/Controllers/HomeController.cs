using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using WitKeyDu.Site.Models;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Site.Impl;

namespace WitKeyDu.Site.Web.Controllers
{
    [Export]
    public class HomeController : Controller
    {
        //
        // GET: /MainPage/

        [Import]//最好换成autofac，不要  MEF
        public ITaskTypeSiteContract TaskTypeContract { get; set; }

        [Import]
        public IAccountSiteContract AccountContract { get; set; }
        [Import]
        ITaskModuleSiteContract TaskModuleSiteContract { get; set; }

        public ActionResult Index()
        {
            string btnInsert = Request.QueryString["btnInsert"];
            string btnSearch = Request.QueryString["btnSearch"];
            if (!string.IsNullOrEmpty(btnInsert))
            {
                //数据新增至索引库
                //InsertToIndex();
            }
            if (!string.IsNullOrEmpty(btnSearch))
            {
                //搜索
                SearchFromIndexData();
            }
            ViewBag.UserCount = AccountContract.SysUsers.Count();
            ViewBag.FinishTaskCount = TaskModuleSiteContract.Tasks.Where(m => m.TaskState == "已完成").Count();
            if (ViewBag.FinishTaskCount != 0)
            {
                ViewBag.TotalAccount = TaskModuleSiteContract.Tasks.Where(m => m.TaskState == "已完成").Sum(m => m.TaskReward);
            }
            else
            {
                ViewBag.TotalAccount = 0;
            }
            var categoryList = GetCategoryList(0);
            return View(categoryList);
        }

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
                         TaskTypeLogo=item.TaskTypeLogo
                     };
                   //List<TaskTypeView> tempList = GetCategoryList(item.Id);
                   //if (tempList.Count > 0)
                   //{
                   //    userViewModel.ChildrenTaskType = tempList;
                   //}
                   uvModel.Add(userViewModel);
               }
           }
             return uvModel;
         }

  
        public void SearchFromIndexData()
        {

            string indexPath = System.Web.HttpContext.Current.Server.MapPath("~/IndexData");
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            PhraseQuery query = new PhraseQuery();
            //把用户输入的关键字进行分词
            foreach (string word in WitKeyDu.Site.Web.SplitContent.SplitWords(Request.QueryString["SearchKey"]))
            {
                query.Add(new Term("ForumContent", word));
            }
            //query.Add(new Term("content", "C#"));//多个查询条件时 为且的关系
            query.SetSlop(100); //指定关键词相隔最大距离

            //TopScoreDocCollector盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);//根据query查询条件进行查询，查询结果放入collector容器
            //TopDocs 指定0到GetTotalHits() 即所有查询结果中的文档 如果TopDocs(20,10)则意味着获取第20-30之间文档内容 达到分页的效果
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;

            //展示数据实体对象集合
            List<Forum> ForumResult = new List<Forum>();
            for (int i = 0; i < docs.Length; i++)
            {
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//根据文档id来获得文档对象Document


                Forum forum = new Forum();
                forum.ForumName = doc.Get("ForumName");
                //book.Title = doc.Get("title");
                ////book.ContentDescription = doc.Get("content");//未使用高亮
                ////搜索关键字高亮显示 使用盘古提供高亮插件
                forum.ForumContent = WitKeyDu.Site.Web.SplitContent.HightLight(Request.QueryString["SearchKey"], doc.Get("ForumContent"));
                forum.ForumTypeID = Convert.ToInt32(doc.Get("ID"));
                ForumResult.Add(forum);
            }
        }
    }
}
