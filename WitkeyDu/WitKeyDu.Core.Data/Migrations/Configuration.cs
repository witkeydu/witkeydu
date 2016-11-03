using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using WitKeyDu.Component.Data;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Models.Security;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Core.Models.Files;


namespace WitKeyDu.Core.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            List<Role> roles = new List<Role>
            {
                new Role{ Name = "系统管理员", Description = "系统管理员，按级别进行分配管理权限。", RoleType = RoleType.Admin},
                new Role{ Name = "超级管理员", Description = "超级管理员，拥有整个系统的管理权限。", RoleType = RoleType.Admin},
                new Role{ Name = "系统用户", Description = "系统用户，按级别进行分配管理权限。", RoleType = RoleType.User},
                new Role{ Name = "普通会员", Description = "普通会员，按级别进行分配管理权限。", RoleType = RoleType.User},
                new Role{ Name = "高级会员", Description = "高级会员，按级别进行分配管理权限。", RoleType = RoleType.User}
            };
            DbSet<Role> roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(m => new { m.Name }, roles.ToArray());
            context.SaveChanges();

            List<SystemUser> Users = new List<SystemUser>
            {
                new SystemUser { UserName = "Admin",ContactNumber="13986751153", Password = "111", Email = "625891196@qq.com", NickName = "零零柒" },
                new SystemUser { UserName = "User",ContactNumber="13572232256", Password = "111", Email = "Shi.yonglong@qq.com", NickName = "零零柒" }
            };

            DbSet<SystemUser> SysUserSet = context.Set<SystemUser>();
            SysUserSet.AddOrUpdate(m => new { m.UserName}, Users.ToArray());
            context.SaveChanges();


            List<TaskType> TaskTypes = new List<TaskType>
            {
                new TaskType { SystemUserID=1, TaskTypeName="设计绘画", TaskParentTypeID=0, TaskTypeLogo="../Images/20160519748bzsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="软件开发", TaskParentTypeID=0, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="写作翻译", TaskParentTypeID=0, TaskTypeLogo="../Images/20160519748bzsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="市场营销", TaskParentTypeID=0, TaskTypeLogo="../Images/20160519748sjkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="人力资源", TaskParentTypeID=0, TaskTypeLogo="../Images/20160519748wxkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="文体教育", TaskParentTypeID=0, TaskTypeLogo="../Images/20160519748sjkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="平面设计", TaskParentTypeID=1, TaskTypeLogo="../Images/20160519748bzsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="UI设计", TaskParentTypeID=1, TaskTypeLogo="../Images/20160519748wxsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="工业设计", TaskParentTypeID=1, TaskTypeLogo="../Images/20160519748wxsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="拍摄知错", TaskParentTypeID=1, TaskTypeLogo="../Images/20160519748wxsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="手绘插画", TaskParentTypeID=1, TaskTypeLogo="../Images/20160519748wxsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="PPT设计", TaskParentTypeID=1, TaskTypeLogo="../Images/20160519748wxsj.png"},
                new TaskType { SystemUserID=1, TaskTypeName="网站建设", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="网店装修", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="APP开发", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="微信开发", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="软件系统", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="产品测试", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="IOS开发", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="游戏开发", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="数据库", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="H5前端", TaskParentTypeID=2, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="文稿撰写", TaskParentTypeID=3, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="数据录入", TaskParentTypeID=3, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="专业翻译", TaskParentTypeID=3, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="专业起名", TaskParentTypeID=3, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="活动策划", TaskParentTypeID=3, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="品牌策划", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="微信运营", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="微博运营", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="线下推广", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="SEO优化", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="广告推广", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="实习兼职", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="人事服务", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="人才招聘", TaskParentTypeID=4, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="家教辅导", TaskParentTypeID=5, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="外语培训", TaskParentTypeID=5, TaskTypeLogo="../Images/20160519748zdkf.png"},
                new TaskType { SystemUserID=1, TaskTypeName="考前培训", TaskParentTypeID=5, TaskTypeLogo="../Images/20160519748zdkf.png"},
            };
            DbSet<TaskType> TaskTypeSet = context.Set<TaskType>();
            TaskTypeSet.AddOrUpdate(m => new { m.TaskTypeName }, TaskTypes.ToArray());
            context.SaveChanges();
            List<ForumType> ForumTypes = new List<ForumType>
            {
                new ForumType { SystemUserID=1, ForumTypeName=".net技术",ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="编程语言", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="软件设计", ForumParentTypeID=0, ForumTypeLogo=""},
                new ForumType { SystemUserID=1, ForumTypeName="WEB前端", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="企业信息化", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="手机开发", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="软件工程", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="数据库技术", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="操作系统", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="求职面试", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="书籍推荐", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="搞怪趣事", ForumParentTypeID=0, ForumTypeLogo="1"},
                new ForumType { SystemUserID=1, ForumTypeName="生活黄页", ForumParentTypeID=0, ForumTypeLogo="1"},
            };
            DbSet<ForumType> ForumTypeSet = context.Set<ForumType>();
            ForumTypeSet.AddOrUpdate(m => new { m.ForumTypeName }, ForumTypes.ToArray());
            context.SaveChanges();
            List<FileType> FileTypes = new List<FileType>
            {
                new FileType { SystemUserID=1, FileTypeName=".net技术",FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="编程语言", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="软件设计", FileParentTypeID=0, FileTypeLogo=""},
                new FileType { SystemUserID=1, FileTypeName="WEB前端", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="企业信息化", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="手机开发", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="软件工程", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="数据库技术", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="操作系统", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="求职面试", FileParentTypeID=0, FileTypeLogo="1"},
                new FileType { SystemUserID=1, FileTypeName="书籍推荐", FileParentTypeID=0, FileTypeLogo="1"},
            };
            DbSet<FileType> FileTypeSet = context.Set<FileType>();
            FileTypeSet.AddOrUpdate(m => new { m.FileTypeName }, FileTypes.ToArray());
            context.SaveChanges();
        }
    }
}