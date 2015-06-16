using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 业务申请
    /// </summary>
    public class BusinessApplicationController : Controller
    {
        #region 页面

        /// <summary>
        /// 施工单位
        /// </summary>
        /// <returns></returns>
        public ActionResult Company()
        {
            return View();
        }

        /// <summary>
        /// 入驻企业
        /// </summary>
        /// <returns></returns>
        public ActionResult Enterprise()
        {
            return View();

        }


        /// <summary>
        /// 访客在线业务申请/查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Visitor()
        {
            return View();
        }
        #endregion
    }
}
