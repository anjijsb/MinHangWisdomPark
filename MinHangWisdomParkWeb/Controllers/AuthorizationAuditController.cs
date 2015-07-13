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
        #region 参数

        Models.ajIIPdbEntities1 dal = new Models.ajIIPdbEntities1();

        ApplyHelp applyhelp = new ApplyHelp();

        #endregion

        #region 旧 0.1

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

        #endregion

        #region 页面

        #region 业务申请授权
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string Type, string Title)
        {
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title))
            {
                ViewBag.Type = Type.Replace("申请", "");
                ViewBag.Title = Title;
            }
            return View();
        }


        #endregion


        #region 信息申请授权

        public ActionResult PeblishIndex(string Type, string Title)
        {
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title))
            {
                ViewBag.Type = Type.Replace("申请", "");
                ViewBag.Title = Title;
            }
            ViewBag.PeblishList1 = PeblishList("1");
            ViewBag.PeblishList2 = PeblishList("2");
            ViewBag.PeblishList3 = PeblishList("3");
            ViewBag.PeblishList4 = PeblishList("4");
            return View();
        }

        #endregion

        #region 报修申请授权

        /// <summary>
        /// 报修申请
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public ActionResult RepairIndex(string Type, string Title)
        {
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title))
            {
                ViewBag.Title = Title;
            }
            ViewBag.RepairList1 = RepairList(1);
            ViewBag.RepairList2 = RepairList(2);
            return View();
        }


        #endregion





        #endregion


        #region 数据

        /// <summary>
        /// 报修
        /// </summary> 
        /// <returns></returns>                                                              
        private List<ShenHe> RepairList(int RepairType)
        {
            var aa = (from c in dal.tbConfirmState
                      from r in dal.tbRepair
                      from a in dal.tbApplyBill
                      from u in dal.mtUser
                      where a.ApplyID == c.ApplyID && a.ObjectID == r.RepairID && c.ConfirmerID == GlobalParameter.UserId && a.Updater == u.UserId && r.RepairType == RepairType
                      select new ShenHe
                      {
                          ApplyID = a.ApplyID,
                          Title = r.RepairTitle,
                          Content = r.RepairContent,
                          Updater = u.UserName,
                          Time = a.UpdateTime
                      }).ToList();
            return aa;
        }

        /// <summary>
        ///提取信息发布个类别数据
        /// </summary>
        /// <param name="PeblishType"></param>
        /// <returns></returns>
        private List<ShenHe> PeblishList(string PeblishType)
        {
            var sq = (from p in dal.tbPeblish
                      from a in dal.tbApplyBill
                      from c in dal.tbConfirmState
                      from u in dal.mtUniversalCode
                      from u1 in dal.mtUser
                      where p.PeblishID == a.ObjectID &&
                      u.UniversalType == "StateType" &&
                      a.StateType == u.CodeID &&
                      p.Updater == GlobalParameter.UserId &&
                      p.PeblishType == PeblishType &&
                      a.ApplyType == "Peblish" &&
                      a.ApplyID == c.ApplyID &&
                      p.Updater == u1.UserId &&
                      c.ConfirmerID == GlobalParameter.UserId
                      select new ShenHe
                      {
                          ApplyID = a.ApplyID,
                          Title = p.PeblishTitle,
                          Time = p.CreateTime,
                          Updater = u1.UserName,
                          Content = p.PeblishContent
                      }).ToList();
            return sq;
        }



        #endregion


        #region 功能

        /// <summary>
        /// 审核功能
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="content"></param>
        /// <param name="bools"></param>
        /// <returns></returns>
        public JsonResult ShenHe(string applyid, string content, string bools)
        {
            try
            {
                int numapplyid, numbools;
                int.TryParse(applyid, out numapplyid);
                int.TryParse(bools, out numbools);
                applyhelp.UpdateComfirmStart(numapplyid, numbools, content);
                return Json(new { msg = "ok" });
            }
            catch (Exception)
            {
                return Json(new { msg = "no" });
                throw;
            }
        }

        #endregion


    }


    #region 模型

    public class ShenHe
    {
        public int ApplyID { get; set; }
        public string Title { get; set; }
        public DateTime? Time { get; set; }
        public string Updater { get; set; }
        public string Content { get; set; }
    }

    #endregion
}
