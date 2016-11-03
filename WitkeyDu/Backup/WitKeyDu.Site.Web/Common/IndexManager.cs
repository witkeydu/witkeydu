using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Site.Models;

namespace WitKeyDu.Site.Web
{
    public class IndexManager {
        public static readonly IndexManager bookIndex = new IndexManager();
        public static readonly string indexPath = HttpContext.Current.Server.MapPath("~/IndexData");
        private IndexManager() {
        }
        //请求队列 解决索引目录同时操作的并发问题
        private Queue<ReleaseForumModel> bookQueue = new Queue<ReleaseForumModel>();
        /// <summary>
        /// 新增Books表信息时 添加邢增索引请求至队列
        /// </summary>
        /// <param name="books"></param>
        public void Add(ReleaseForumModel model)
        {
            ReleaseForumModel bvm = new ReleaseForumModel();
            bvm.ForumName = model.ForumName;
            bvm.ForumContent = model.ForumContent;
            bvm.ForumLogo = model.ForumLogo;
            bvm.ForumTypeID = model.ForumTypeID;
            bookQueue.Enqueue(bvm);
        }
        /// <summary>
        /// 删除Books表信息时 添加删除索引请求至队列
        /// </summary>
        /// <param name="bid"></param>
        //public void Del(int bid) {
        //    BookViewMode bvm = new BookViewMode();
        //    bvm.Id = bid;
        //    bvm.IT = IndexType.Delete;
        //    bookQueue.Enqueue(bvm);
        //}
        /// <summary>
        /// 修改Books表信息时 添加修改索引(实质上是先删除原有索引 再新增修改后索引)请求至队列
        /// </summary>
        /// <param name="books"></param>
        public void Mod(ReleaseForumModel model)
        {
            ReleaseForumModel bvm = new ReleaseForumModel();
            bvm.ForumName = model.ForumName;
            bvm.ForumContent = model.ForumContent;
            bvm.ForumLogo = model.ForumLogo;
            bvm.ForumTypeID = model.ForumTypeID;
            bookQueue.Enqueue(bvm);
        }

        public void StartNewThread() {
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueToIndex));
        }

        //定义一个线程 将队列中的数据取出来 插入索引库中
        private void QueueToIndex(object para) {
            while(true) {
                if(bookQueue.Count > 0) {
                    CRUDIndex();
                } else {
                    Thread.Sleep(3000);
                }
            }
        }
        /// <summary>
        /// 更新索引库操作
        /// </summary>
        private void CRUDIndex() {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isExist = IndexReader.IndexExists(directory);
            if(isExist) {
                if(IndexWriter.IsLocked(directory)) {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
            while(bookQueue.Count > 0) {
                Document document = new Document();
                ReleaseForumModel model = bookQueue.Dequeue();
                //if(book.IT == IndexType.Insert) {
                //    document.Add(new Field("id", book.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                //    document.Add(new Field("ForumName", book.ForumName, Field.Store.YES, Field.Index.ANALYZED,
                //                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                //    document.Add(new Field("ForumContent", book.ForumContent, Field.Store.YES, Field.Index.ANALYZED,
                //                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                //    writer.AddDocument(document);
                //} else if(book.IT == IndexType.Delete) {
                //    writer.DeleteDocuments(new Term("id", book.Id.ToString()));
                //} else if(book.IT == IndexType.Modify) {
                //    //先删除 再新增
                //    writer.DeleteDocuments(new Term("id", book.Id.ToString()));
                    document.Add(new Field("ForumTypeID", model.ForumTypeID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("ForumName", model.ForumName, Field.Store.YES, Field.Index.ANALYZED,
                                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("ForumContent", model.ForumContent, Field.Store.YES, Field.Index.ANALYZED,
                                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                    writer.AddDocument(document);
                }
            writer.Close();
            directory.Close();
            }
    }
}