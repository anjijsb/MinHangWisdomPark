using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 官网
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }


        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult login()
        {
            return View();
        }

        /// <summary>
        /// 论坛管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LunTan()
        {
            return View();
        }

        /// <summary>
        /// 临时入口
        /// </summary>
        /// <returns></returns>
        public ActionResult RuKou()
        {
            return View();
        }
    }
}
