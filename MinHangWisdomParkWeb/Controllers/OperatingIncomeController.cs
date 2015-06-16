using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 运行收入管理
    /// </summary>
    public class OperatingIncomeController : Controller
    {
        #region 页面
        /// <summary>
        /// 卡证发行
        /// </summary>
        /// <returns></returns>
        public ActionResult Card()
        {
            return View();
        }

        /// <summary>
        /// 广告收入
        /// </summary>
        /// <returns></returns>
        public ActionResult Advertisement()
        {
            return View();
        }

        /// <summary>
        /// 服务收入
        /// </summary>
        /// <returns></returns>
        public ActionResult Service()
        {
            return View();
        }

        #endregion
    }
}
