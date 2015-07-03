using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
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

        #region 参数

        Models.ajIIPdbEntities1 dal = new Models.ajIIPdbEntities1();

        ApplyHelp applyhelp = new ApplyHelp();

        #endregion

        #region 页面

        /// <summary>
        /// 模板页面
        /// </summary>
        /// <param name="type">信息类型</param>
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



        #region 报修

        /// <summary>
        /// 报修申请界面
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public ActionResult Repair(string Type, string Title)
        {

            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title))
            {
                ViewBag.TypeList = Helps.UniversalCodeList("RepairType");
                ViewBag.Title = Title;
            }

            return View();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="PeblishType"></param>
        /// <param name="PeblishTitle"></param>
        /// <param name="PublishContent"></param>
        /// <returns></returns>
        public JsonResult Insert(string RepairType, string RepairTitle, string RepairContent, string DateTimeNew, string DateTimeOld)
        {
            try
            {
                if (DateTime.Parse(DateTimeNew) < DateTime.Parse(DateTimeOld))
                {
                    return Json(new { msg = "time" });
                }
                else
                {
                    Models.tbRepair repair = new Models.tbRepair
                    {
                        RepairID = (int.Parse((dal.tbRepair.Max(m => m.RepairID) == null ? "0" : dal.tbRepair.Max(m => m.RepairID))) + 1).ToString().PadLeft(12, '0'),
                        RepairType = int.Parse(RepairType),
                        RepairTitle = RepairTitle,
                        RepairContent = RepairContent,
                        StateCode = 1,
                    };

                    applyhelp.InsertApplyBill(InsertRepair(repair), "Repair", DateTimeNew);

                    return Json(new { msg = "ok" });
                }
            }
            catch (Exception)
            {
                return Json(new { msg = "no" });
                throw;
            }
        }

        /// <summary>
        /// 插入repair表数据
        /// </summary>
        /// <param name="repair"></param>
        /// <returns></returns>
        public string InsertRepair(Models.tbRepair repair)
        {
            dal.tbRepair.Add(repair);
            dal.SaveChanges();
            return repair.RepairID;
        }


        #endregion


        #region  状态

        /// <summary>
        /// 业务申请状态查询
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public ActionResult ApplyState(string Title)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                ViewBag.Title = Title;
            }

            var bb = (from r in dal.tbRepair
                      from u in dal.mtUniversalCode
                      from u1 in dal.mtUniversalCode
                      from b in dal.tbApplyBill
                      where b.StateType == u.CodeID
                      && u.UniversalType == "StateType"
                      && u1.UniversalType == "RepairType"
                      && u1.CodeID == r.RepairType
                      && b.ObjectID == r.RepairID
                      && b.Updater == GlobalParameter.UserId
                      select new ApplyStateListJian
                      {
                          RepairID = r.RepairID,
                          RepairTitle = r.RepairTitle,
                          Time = r.CreateTime,
                          StateName = u.CodeName,
                          TypeName = u1.CodeName,
                          RepairContent = r.RepairContent
                      }).ToList();

            var aa = bb.OrderByDescending(a => a.Time).ToList();
            ViewBag.ApplyList = aa;

            return View();
        }


        #endregion


        #region

        /// <summary>
        /// 离入园申请
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public ActionResult CarsAndItems(string Type, string Title)
        {
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title))
            {
                ViewBag.Type = Type.Replace("申请", "");
                ViewBag.Title = Title;
            }
            return View();
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        public void InsertCheck(string InOutFlag, string InOutType, string CheckTitle, string CheckContent)
        {
            try
            {
                Models.tbCheckInOut CheckInOut = new Models.tbCheckInOut
                {
                    CheckID = (int.Parse((dal.tbCheckInOut.Max(m => m.CheckID) == null ? "0" : dal.tbCheckInOut.Max(m => m.CheckID))) + 1).ToString().PadLeft(12, '0'),
                    Version = (int.Parse((dal.tbCheckInOut.Max(m => m.CheckID) == null ? "0" : dal.tbCheckInOut.Max(m => m.CheckID))) + 1),
                    InOutFlag = int.Parse(InOutFlag),
                    InOutType = InOutType,
                    CheckTitle = CheckTitle,
                    CheckContent = CheckContent,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                string CheckInOutID = InsertCheckInOut(CheckInOut);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 插入CheckInOut表
        /// </summary>
        /// <param name="CheckInOut"></param>
        /// <returns>返回id</returns>
        public string InsertCheckInOut(Models.tbCheckInOut CheckInOut)
        {
            try
            {
                dal.tbCheckInOut.Add(CheckInOut);
                dal.SaveChanges();
                return CheckInOut.CheckID;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 插入CheckInOuts表
        /// </summary>
        /// <param name="CheckInOuts"></param>
        public void InsertCheckInOuts(Models.tbCheckInOuts CheckInOuts)
        {

        }

        #endregion

        #endregion
    }


    #region 模型
    /// <summary>
    /// 状态用临时模型
    /// </summary>
    public class ApplyStateListJian
    {
        public string RepairID { get; set; }
        public string RepairTitle { get; set; }
        public DateTime? Time { get; set; }
        public string StateName { get; set; }
        public string TypeName { get; set; }
        public string RepairContent { get; set; }
    }

    #endregion
}
