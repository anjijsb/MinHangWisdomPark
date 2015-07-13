using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MinHangWisdomParkWeb.Controllers
{
    /// <summary>
    /// 信息发布管理
    /// </summary>
    public class InformationDeliveryController : Controller
    {

        #region 参数

        Models.ajIIPdbEntities1 dal = new Models.ajIIPdbEntities1();

        ApplyHelp applyhelp = new ApplyHelp();

        #endregion

        #region v0.1
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

        #endregion

        #region v0.2

        /// <summary>
        /// 模板页面
        /// </summary>
        /// <param name="type">信息类型</param>
        /// <returns></returns>
        public ActionResult Index(string Type, string Title, string CodeID)
        {
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(CodeID))
            {
                int codeid = int.Parse(CodeID);
                ViewBag.Type = Type.Replace("发布管理", "");
                ViewBag.CodeID = dal.mtUniversalCode.FirstOrDefault(m => m.UniversalType == "PeblishType" && m.CodeID == codeid).CodeID;
                ViewBag.Title = Title;
                ViewBag.PeblishList = ShenQin(CodeID);
            }
            return View();
        }



        #region 图片上传
        [HttpPost]
        public JsonResult UpLoadPhoto(HttpPostedFileBase file)
        {
            var res = CheckImg(file);
            string imgurl = "";
            string strerror = "";
            string imgname = "";
            if (res == "ok")
            {
                var fileName = file.FileName;//Path.GetExtension() 也许可以解决这个问题，先不管了。
                int i = fileName.LastIndexOf('.');//取得文件名中最后一个"."的索引    
                string fileextenName = fileName.Substring(i).ToLower();
                string newFileName = Guid.NewGuid().ToString() + fileextenName;
                var pathtemp = Path.Combine(Server.MapPath("~/Uploads/"), newFileName);//先存入临时文件夹
                var scrtemp = Path.Combine("~/Uploads/", newFileName);//图片展示的地址
                imgname = fileName.Replace(fileextenName, "");//图片名称

                var list = Session["Imgscr"] as List<string>;
                var slist = Session["ImgServerscr"] as List<string>;
                if (list != null)
                {
                    list.Add(scrtemp);
                }
                else
                {
                    list = new List<string> { scrtemp };
                    Session["Imgscr"] = list;
                }
                if (slist != null)
                {
                    slist.Add(pathtemp);
                }
                else
                {
                    slist = new List<string> { pathtemp };
                    Session["ImgServerscr"] = slist;
                }

                file.SaveAs(pathtemp);
                //Response.Write("");
                imgurl = "/Uploads/" + newFileName + "";
            }
            else
            {
                strerror = res;
            }
            var Result = new { ErrorInfo = strerror, imgUrl = imgurl, imgId = InsertFiles(imgurl, imgname).ToString() };


            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        private string CheckImg(HttpPostedFileBase file)
        {
            if (file == null) return "图片不能空！";
            if (file.ContentLength / 1024 > 8000)
            {
                return "图片太大";
            }
            if (file.ContentLength / 1024 < 10)
            {
                return "图片太小！";
            }
            var image = GetExtensionName(file.FileName).ToLower();
            if (image != ".bmp" && image != ".png" && image != ".gif" && image != ".jpg" && image != ".jpeg")// 这里你自己加入其他图片格式，最好全部转化为大写再判断，我就偷懒了
            {
                return "格式不对";
            }

            var scrtemp = Path.Combine("/Uploads/", file.FileName);//图片展示的地址
            var list = Session["Imgscr"] as List<string>;
            if (list != null && list.Find(n => n == scrtemp) != null)
            {
                return "同样的照片已经存在！";
            }

            return "ok";
        }
        public string GetExtensionName(string fileName)
        {
            if (fileName.LastIndexOf("\\", StringComparison.Ordinal) > -1)//在不同浏览器下，filename有时候指的是文件名，有时候指的是全路径，所有这里要要统一。
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);//IndexOf 有时候会受到特殊字符的影响而判断错误。加上这个就纠正了。
            }
            return Path.GetExtension(fileName.ToLower());
        }

        #endregion


        #region 功能

        /// <summary>
        /// 插入files表
        /// </summary>
        /// <param name="url"></param>
        public int InsertFiles(string url, string name)
        {
            try
            {
                MinHangWisdomParkWeb.Models.tbFiles file = new MinHangWisdomParkWeb.Models.tbFiles
                {
                    FileType = "img",
                    FilePath = url,
                    FileName = name
                };
                dal.tbFiles.Add(file);
                dal.SaveChanges();
                return file.FileID;
            }
            catch (Exception e)
            {
                throw;
            }

        }


        /// <summary>
        /// 删除files表数据及文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void DeleteFiles(string url)
        {
            try
            {
                dal.tbFiles.Remove(dal.tbFiles.FirstOrDefault(m => m.FilePath == url));
                dal.SaveChanges();
                FileHelper.DeleteFile(Server.MapPath(url));
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="PeblishType"></param>
        /// <param name="PeblishTitle"></param>
        /// <param name="PublishContent"></param>
        /// <param name="PublishImg"></param>
        public JsonResult InsertPeblishApply(string PeblishType, string PeblishTitle, string PublishContent, string[] PublishImg, string DateTimeNew, string DateTimeOld)
        {
            try
            {

                if (DateTime.Parse(DateTimeNew) < DateTime.Parse(DateTimeOld))
                {
                    return Json(new { msg = "time" });
                }
                else
                {
                    applyhelp.InsertApplyBill(InsertPeblish(PeblishType, PeblishTitle, PublishContent, PublishImg), "Peblish", DateTimeNew);
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
        /// 插入peblish数据并返回插入数据ID
        /// </summary>
        /// <param name="PeblishType"></param>
        /// <param name="PeblishTitle"></param>
        /// <param name="PublishContent"></param>
        /// <param name="PublishImg"></param>
        /// <returns></returns>
        private string InsertPeblish(string PeblishType, string PeblishTitle, string PublishContent, string[] PublishImg)
        {

            try
            {
                MinHangWisdomParkWeb.Models.tbPeblish peblish = new MinHangWisdomParkWeb.Models.tbPeblish
                {
                    PeblishID = (int.Parse((dal.tbPeblish.Max(m => m.PeblishID) == null ? "0" : dal.tbPeblish.Max(m => m.PeblishID))) + 1).ToString().PadLeft(12, '0'),
                    PeblishType = PeblishType,
                    PeblishTitle = PeblishTitle,
                    PeblishContent = PublishContent,
                    Updater = GlobalParameter.UserId
                };
                if (PublishImg != null)
                {
                    peblish.FileIDs = string.Join(",", PublishImg);
                }
                dal.tbPeblish.Add(peblish);
                dal.SaveChanges();
                return peblish.PeblishID;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 提取当前类别数据
        /// </summary>
        /// <param name="PeblishType"></param>
        /// <returns></returns>
        public List<ShenQin> ShenQin(string PeblishType)
        {
            var sq = (from p in dal.tbPeblish
                      from a in dal.tbApplyBill
                      from c in dal.tbConfirmState
                      from u in dal.mtUniversalCode
                      where p.PeblishID == a.ObjectID &&
                      u.UniversalType == "StateType" &&
                      a.StateType == u.CodeID &&
                      p.Updater == GlobalParameter.UserId &&
                      p.PeblishType == PeblishType &&
                      a.ApplyType == "Peblish" &&
                      a.ApplyID == c.ApplyID
                      select new ShenQin
                      {
                          PeblishID = p.PeblishID,
                          Title = p.PeblishTitle,
                          Time = p.CreateTime,
                          Content = p.PeblishContent,
                          StateName = u.CodeName
                      }).ToList();

            return sq;
        }


        #endregion



        #endregion



    }

    public class ShenQin
    {
        public string PeblishID { get; set; }
        public string Title { get; set; }
        public DateTime? Time { get; set; }
        public string Content { get; set; }
        public string StateName { get; set; }
    }

}
