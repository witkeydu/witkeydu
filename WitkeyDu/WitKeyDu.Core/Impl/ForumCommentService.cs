using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Forums;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Impl
{
     public abstract class ForumCommentService : CoreServiceBase, IForumCommentService
     {
         #region 属性
         #region 受保护的属性

         /// <summary>
         /// 获取或设置 论坛评论信息数据访问对象
         /// </summary>
         [Import]
         protected IForumCommentRepository ForumCommentRepository { get; set; }
         #endregion
         #region 共有属性
         /// <summary>
         /// 获取论坛评论查询数据集
         /// </summary>
         public IQueryable<ForumComment> ForumComments
         {
             get { return ForumCommentRepository.Entities; }
         }

         #endregion
         #endregion
         public virtual OperationResult ReleaseForumComment(ForumComment ForumCommentInfo)
         {
             try
             {
                 PublicHelper.CheckArgument(ForumCommentInfo, "FileResourceInfo");
                 ForumCommentRepository.Insert(ForumCommentInfo);
                 return new OperationResult(OperationResultType.Success, "文件分享成功。", ForumCommentInfo);
             }
             catch (Exception ex)
             {
                 return new OperationResult(OperationResultType.Error, ex.Message.ToString());
             }
         }
    }
}
