using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 信息发布管理
    /// </summary>
    public class InformationDeliveryController : Controller
    {
        /// <summary>
        ///新闻信息
        /// </summary>
        /// <returns></returns>
        public ActionResult News()
        {
            return View();
        }

        /// <summary>
        /// 园区信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Park()
        {
            return View();
        }

        /// <summary>
        /// 广告
        /// </summary>
        /// <returns></returns>
        public ActionResult Advertisement()
        {
            return View();
        }
    }
}
