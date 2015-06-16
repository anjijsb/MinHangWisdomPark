using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 数据查询分析
    /// </summary>
    public class DataQueryController : Controller
    {
        #region 页面

        /// <summary>
        /// 营收
        /// </summary>
        /// <returns></returns>
        public ActionResult Revenue()
        {
            return View();
        }

        #endregion
    }
}
