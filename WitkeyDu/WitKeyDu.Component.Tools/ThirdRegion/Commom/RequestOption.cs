using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// 请求参数类
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public class RequestOption
    {
        #region 属性
        public string Name
		{
			get;
			internal set;
		}

		public object Value
		{
			get;
			internal set;
		}

		public bool IsBinary
		{
			get
			{
				if (Value != null && Value.GetType() == typeof(byte[])) return true;
			    return false;
			}
		}
        #endregion

        #region 构造方法
        public RequestOption()
		{ 
		
		}

        public RequestOption(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public RequestOption(string name, bool value)
		{
			Name = name;
			Value = value ? "1" : "0";
		}

		public RequestOption(string name, int value)
		{
			Name = name;
			Value = string.Format("{0}",value);
		}

		public RequestOption(string name, long value)
		{
			Name = name;
			Value = string.Format("{0}", value);
		}

		public RequestOption(string name, float value)
		{
			Name = name;
			Value = string.Format("{0}", value);
		}

		public RequestOption(string name, double value)
		{
			Name = name;
			Value = string.Format("{0}", value);
		}

		public RequestOption(string name, byte[] value)
		{
			Name = name;
			Value = value;
		}

        internal RequestOption(string name, object value)
		{
			Name = name;
			Value = value;
        }
        #endregion
    }

    #region 枚举类型
    public enum RequestMethod
    {
        Get,
        Post
    }

    public enum ResponseType
    {
        Code,
        Token
    }

    public enum GrantType
    {
        AuthorizationCode,
        Password,
        RefreshToken
    }
    #endregion
}
