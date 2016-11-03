using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Account;
using WitKeyDu.Core.Data.Repositories.Forums;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    /// 论坛核心模块实现
    /// </summary>
    public abstract class ForumModuleService:CoreServiceBase,IForumModuleService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected ISystemUserRepository SysUserRepository { get; set; }

        /// <summary>
        /// 获取或设置 论坛帖信息数据访问对象
        /// </summary>
        [Import]
        protected IForumRepository ReleaseForumRepository { get; set; }
        /// <summary>
        /// 获取或设置 论坛帖类型信息数据访问对象
        /// </summary>
        [Import]
        protected IForumTypeRepository ForumTypeRepository { get; set; }
        /// <summary>
        /// 获取或设置 论坛评论信息数据访问对象
        /// </summary>
        [Import]
        protected IForumCommentRepository ForumCommentRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取论坛帖查询数据集
        /// </summary>
        public IQueryable<Forum> Forums
        {
            get { return ReleaseForumRepository.Entities; }
        }

        /// <summary>
        /// 获取论坛帖数据查询数据集
        /// </summary>
        public IQueryable<SystemUser> SysUsers
        {
            get { return SysUserRepository.Entities; }
        }

        /// <summary>
        /// 获取论坛帖类型数据查询数据集
        /// </summary>
        public IQueryable<ForumType> ForumTypes
        {
            get { return ForumTypeRepository.Entities; }
        }

        /// <summary>
        /// 获取论坛评论查询数据集
        /// </summary>
        public IQueryable<ForumComment> ForumComments
        {
            get { return ForumCommentRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult ReleaseForum(Forum ForumInfo)
        {
            try
            {
                PublicHelper.CheckArgument(ForumInfo, "ForumInfo");
                ReleaseForumRepository.Insert(ForumInfo);
                return new OperationResult(OperationResultType.Success, "论坛帖发布成功。", ForumInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }
    }
}
