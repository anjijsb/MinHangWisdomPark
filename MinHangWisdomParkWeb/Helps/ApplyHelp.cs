using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinHangWisdomParkWeb
{
    public class ApplyHelp
    {

        Models.ajIIPdbEntities1 dal = new Models.ajIIPdbEntities1();

        /// <summary>
        /// 插入AppleBill表数据
        /// </summary>
        /// <returns></returns>
        public void InsertApplyBill(string ObjectID, string ApplyType)
        {
            try
            {

                MinHangWisdomParkWeb.Models.tbApplyBill applybills = new Models.tbApplyBill
                {
                    ApplyType = ApplyType,
                    ObjectID = ObjectID,
                    ApplyDate = DateTime.Now,
                    Updater = GlobalParameter.UserId
                };
                dal.tbApplyBill.Add(applybills);
                dal.SaveChanges();
                InsertConfirmStart(applybills.ApplyID, 2, 1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 插入ConfirmSrart表
        /// </summary>
        public void InsertConfirmStart(int ApplyID, int NeedConfirmLevel, int ConfirmerAutoID)
        {
            var tem = dal.mtConfirmFlow.Where(m => m.ConfirmerAutoID == ConfirmerAutoID && m.ConfirmerLevelID == 1).FirstOrDefault();
            MinHangWisdomParkWeb.Models.tbConfirmState confirmstates = new Models.tbConfirmState
            {
                ApplyID = ApplyID,
                CurrConfirmLevel = 0,
                NeedConfirmLevel = NeedConfirmLevel,
                ConfirmerID = tem.UserId,
                ConfirmerAutoID = ConfirmerAutoID,
                CreateTime = DateTime.Now
            };
            dal.tbConfirmState.Add(confirmstates);
            dal.SaveChanges();
        }


    }
}