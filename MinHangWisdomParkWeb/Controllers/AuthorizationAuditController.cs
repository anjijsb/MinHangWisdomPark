using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 授权审核管理
    /// </summary>
    public class AuthorizationAuditController : Controller
    {
        #region 页面

        /// <summary>
        /// 园区项目
        /// </summary>
        /// <returns></returns>
        public ActionResult Park()
        {
            return View();
        }

        /// <summary>
        /// 人员入园
        /// </summary>
        /// <returns></returns>
        public ActionResult Personnel()
        {
            return View();
        }

        /// <summary>
        /// 车辆入园
        /// </summary>
        /// <returns></returns>
        public ActionResult Car()
        {
            return View();
        }

        /// <summary>
        /// 临时访问
        /// </summary>
        /// <returns></returns>
        public ActionResult Temporary()
        {
            return View();
        }

        /// <summary>
        /// 模板页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string Type,string Title)
        {
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title))
            {
                ViewBag.Type = Type.Replace("申请", "");
                ViewBag.Title = Title;
            }
            return View();
        }

        #endregion
    }
}
