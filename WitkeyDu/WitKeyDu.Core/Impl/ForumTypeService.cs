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
    /// <summary>
    /// 帖子类型模块核心业务实现
    /// </summary>
    public abstract class ForumTypeService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 任务类型信息数据访问对象
        /// </summary>
        [Import]
        protected IForumTypeRepository ForumTypeRepository { get; set; }

        #endregion
        #region 共有属性
        /// <summary>
        /// 获取任务类型数据查询数据集
        /// </summary>
        public IQueryable<ForumType> ForumTypes
        {
            get { return ForumTypeRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult AddForumType(ForumType ForumTypeInfo)
        {
            try
            {
                PublicHelper.CheckArgument(ForumTypeInfo, "ForumTypeInfo");
                ForumTypeRepository.Insert(ForumTypeInfo);
                return new OperationResult(OperationResultType.Success, "任务类型添加成功。", ForumTypeInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }
    }
}
