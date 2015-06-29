using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    public class MainController : BaseController
    {


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

    #region 模型
    public class Menus : Models.Functions
    {
        public List<Models.Functions> FunctionList { get; set; }
    }

    #endregion

}
