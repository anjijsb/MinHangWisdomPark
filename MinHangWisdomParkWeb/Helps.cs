using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MinHangWisdomParkWeb
{
    public class Helps
    {
        /// <summary>
        /// 菜单显示
        /// </summary>
        /// <returns></returns>
        public static List<MinHangWisdomParkWeb.Controllers.Menus> MenuList()
        {
            using (Models.ajIIPdbEntities1 dal = new Models.ajIIPdbEntities1())
            {
                List<MinHangWisdomParkWeb.Controllers.Menus> menus = new List<MinHangWisdomParkWeb.Controllers.Menus>();

                foreach (var ii in dal.Functions.Where(n => n.MasterMenu == true))
                {
                    menus.Add(new MinHangWisdomParkWeb.Controllers.Menus
                    {
                        FunctionID = ii.FunctionID,
                        FunctionName = ii.FunctionName,
                        Superior = ii.Superior,
                        MasterMenu = ii.MasterMenu,
                        Type = ii.Type,
                        FunctionList = dal.Functions.Where(m => m.Superior == ii.FunctionID).ToList()
                    });
                }
                return menus;
            }
        }
    }

}