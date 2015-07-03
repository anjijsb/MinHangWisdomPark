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
        public void InsertApplyBill(string ObjectID, string ApplyType, string DateTimeNew)
        {
            try
            {

                MinHangWisdomParkWeb.Models.tbApplyBill applybills = new Models.tbApplyBill
                {
                    ApplyType = ApplyType,
                    ObjectID = ObjectID,
                    Updater = GlobalParameter.UserId,
                    ApplyDate = DateTime.Parse(DateTimeNew),
                    StateType = 1
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
            };
            dal.tbConfirmState.Add(confirmstates);
            dal.SaveChanges();
        }


        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <param name="bools"></param>
        /// <param name="content"></param>
        public void UpdateComfirmStart(int ApplyID, int bools, string content)
        {
            Models.tbConfirmState ConfirmState = dal.tbConfirmState.FirstOrDefault(m => m.ApplyID == ApplyID);
            Models.tbApplyBill ApplyBill = dal.tbApplyBill.FirstOrDefault(m => m.ApplyID == ApplyID);
            if (bools == 0)
            {
                ConfirmState.ConfirmerID = null;
                ConfirmState.CurrConfirmLevel = 0;
                ApplyBill.StateType = 3;
                dal.SaveChanges();
            }
            else if (bools == 1)
            {
                if (ConfirmState.CurrConfirmLevel > ConfirmState.NeedConfirmLevel)
                {
                    ConfirmState.ConfirmerID = null;
                    ApplyBill.StateType = 2;
                }
                else
                {
                    ConfirmState.CurrConfirmLevel += 1;
                    ConfirmState.ConfirmerID = dal.mtConfirmFlow.Where(m => m.ConfirmerAutoID == ConfirmState.ConfirmerAutoID && m.ConfirmerLevelID == ConfirmState.CurrConfirmLevel).FirstOrDefault().UserId;
                }
                dal.SaveChanges();
            }

        }

    }
}