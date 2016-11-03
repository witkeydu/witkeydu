using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// OAuth基础用户接口
    /// </summary>
    public interface IUserInterface
    {
        dynamic GetUserInfo();
        void EndSession();
    }

}
