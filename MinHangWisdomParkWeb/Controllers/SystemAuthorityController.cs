﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 系统权限管理
    /// </summary>
    public class SystemAuthorityController : Controller
    {
        /// <summary>
        /// 管理员系统功能层级限定
        /// </summary>
        /// <returns></returns>
        public ActionResult Function()
        {
            return View();
        }

        /// <summary>
        /// 管理员系统数据对象范围树形层级限定
        /// </summary>
        /// <returns></returns>
        public ActionResult Data()
        {
            return View();
        }
    }
}
