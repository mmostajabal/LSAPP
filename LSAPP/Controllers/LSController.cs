using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSService;

namespace LSAPP.Controllers
{
    public class LSController : Controller
    {
        // GET: LS
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadPackingList(UploadInf uploadInf)
        {
            return View();
        }


        public JsonResult SaveTCat(Cat cat)
        {
            bool flag = false;

            flag = LSSrv.SaveTCat(cat, Session["UserId"].ToString());

            return Json(flag);
        }
        /// <summary>
        /// FetchTCat
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <param name="pNC"></param>
        /// <returns></returns>
        public JsonResult FetchTCat(string technicalCode = "", string pNC = "")
        {
            Cat cat = new Cat();
            cat = LSSrv.FetchTCat(technicalCode, pNC);
            if (cat != null)
            {
                return Json(cat, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0);
            }
        }
        /// <summary>
        /// CheckDuplicateTechnicalCode
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <returns></returns>
        public JsonResult CheckDuplicateTechnicalCode (string technicalCode = "")
        {
            bool flag = false;
            Cat cat = new Cat();
            cat = LSSrv.FetchTCat(technicalCode, "");

            flag = (cat == null ? false : true);

            return Json(flag);
        }

        /// <summary>
        /// SaveTCustomer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public JsonResult SaveTCustomer(Customer customer)
        {
            bool flag = false;

            flag = LSSrv.SaveTCustomer(customer, Session["UserId"].ToString());

            return Json(flag);
        }

        public JsonResult SaveTGoodsPrice(GoodsPrice goodsPrice)
        {
            bool flag = false;
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();

            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);

            flag = LSSrv.SaveTGoodsPrice(goodsPrice, Session["UserId"].ToString(), CurDate, CurTime);

            return Json(flag);
        }
        /// <summary>
        /// SaveRequest
        /// </summary>
        /// <param name="vRequestClass"></param>
        /// <returns></returns>
        public JsonResult SaveRequest(VRequestClass vRequestClass)
        {
            string requestNo;
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();

            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);
            requestNo = LSSrv.SaveRequest(vRequestClass, Session["UserId"].ToString(), CurDate, CurTime);

            return Json(requestNo);
        }


        public JsonResult SaveInvoice(string technicalCode, string invoiceQty, string requestId, string requestDetialId, string requestNo, int customerId, string invoiceTitle)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();
            string invoiceNo;
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);
            invoiceNo = LSSrv.SaveInvoice(technicalCode, invoiceQty, requestId, requestDetialId, requestNo, customerId, invoiceTitle,Session["UserId"].ToString(), CurDate, CurTime);

            return Json(invoiceNo);
        }

        public JsonResult ConfirmInvoice(string invoiceDetailId)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();
            bool flag;
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);
            flag = LSSrv.ConfirmInvoice(invoiceDetailId, CurDate, CurTime, Session["UserId"].ToString());

            return Json(flag);

        }

        //public JsonResult OLD_SaveFactor(string invoiceDetailId, string technicalCode, string qty, string price, int customerId)
        //{
        //    ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
        //    String CurTime, CurDate;
        //    GlobalClass globalclass = new GlobalClass();
        //    bool flag = true;
        //    CurTime = globalclass.MakeCurTime();
        //    CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
        //    CurDate += DateTime.Now.Day.ToString();
        //    CurDate = ObjDate.ShamsiDate(CurDate);

        //    //flag = LSSrv.SaveFactor(invoiceDetailId, technicalCode, qty, price, customerId, CurDate, CurTime, Session["UserId"].ToString());

        //    return Json(flag);
        //}

        public JsonResult SaveFactor(string invoiceDetailId, string exitFromWarehouseHeaderId, string exitFromWarehouseDetailId)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();
            string factorNo = "";
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);

            factorNo = LSSrv.SaveFactor(invoiceDetailId, exitFromWarehouseHeaderId, exitFromWarehouseDetailId, CurDate, CurTime, Session["UserId"].ToString());

            IList<VExitWarehouseClass> vExitWarehouseClasses = LSSrv.FetchVExitWarehouseClass("", "", 0, 0, 0, factorNo);

            //return View("ShowFactor", vExitWarehouseClasses);
            return Json(true);

        }

        public ActionResult ShowFactor(string factorNo)
        {
            IList<VExitWarehouseClass> vExitWarehouseClasses = LSSrv.FetchVExitWarehouseClass("", "", 0, 0, 0, factorNo);

            return View(vExitWarehouseClasses);
        }
        public JsonResult SaveExitFromWarhouse(string technicalCode, string exitQty, string invoiceId, string invoiceDetailId, string invoiceNo, int customerId, string exitTitle)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();
            string  exitNo;
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);

            exitNo = LSSrv.SaveExitFromWarhouse(technicalCode, exitQty, invoiceId, invoiceDetailId, invoiceNo, customerId, exitTitle, CurDate, CurTime, Session["UserId"].ToString());
            return Json(exitNo);
        }
    }
}