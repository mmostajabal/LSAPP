using LSService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace LSAPP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["UserId"] = "79203112";
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UploadInf()
        {
            ViewBag.Message = "آپلود سفارشات";
            // ViewData["UploadDocTypeId"] = new SelectList(LSSrv.FetchTUploadDocType(), "UploadDocTypeId", "DocTypeDesc");
            return View();
        }
        /// <summary>
        /// UploadInf
        /// </summary>
        /// <param name="uploadInf"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadInfAnsShowResult(UploadInf uploadInf, HttpPostedFileBase file)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();

            string fileName = "";
            int ind;
            int uploadInfId;
            DataTable tempDT;
            string orderNo;
            IList<OrderHaveError> orderHaveErrors;
            int numberTransfer;
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();

            CurDate = ObjDate.ShamsiDate(CurDate);
            //  UploadFile
            fileName = UploadFile(file, CurDate, CurTime);
            //  Save TUploadInf
            uploadInfId = LSSrv.SaveTUploadInf(uploadInf, fileName, 1);

            tempDT = FetchInf(fileName);
            //  Transfer Inf
            Session["UserId"] = "79203112";
            numberTransfer = LSSrv.TransferInf(tempDT, uploadInf.UploadDocTypeId, uploadInf.OrderNo, CurDate, CurTime, Session["UserId"].ToString(), uploadInfId, out orderHaveErrors);

            //ViewData["UploadDocTypeId"] = new SelectList(LSSrv.FetchTUploadDocType(), "UploadDocTypeId", "DocTypeDesc");
            ViewBag.NumberTransfer = " تعداد انتقال يافته " + numberTransfer.ToString() + " شماره سفارش " + uploadInf.OrderNo.ToString();

            return View(orderHaveErrors);
        }
        /// <summary>
        /// TransferInf
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="uploadDocTypeId"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        private DataTable FetchInf(string fileName)
        {
            OleDbConnection cnExcel = new OleDbConnection();
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter cmdDA = new OleDbDataAdapter();
            DataTable tempDT = new DataTable();
            string target, sqlStr;

            target = Path.Combine(Server.MapPath("~/Documents"), fileName);
            cnExcel.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + target +
                       ";Extended Properties=Excel 8.0;";
            cnExcel.Open();
            cmdExcel.Connection = cnExcel;

            sqlStr = " Select * From [sheet1$]";
            cmdExcel.CommandText = sqlStr;

            cmdDA.SelectCommand = cmdExcel;
            cmdDA.Fill(tempDT);

            return tempDT;
        }

        /// <summary>
        /// UploadFile
        /// </summary>
        /// <param name="file"></param>
        /// <param name="CurDate"></param>
        /// <param name="CurTime"></param>
        /// <returns></returns>
        public string UploadFile(HttpPostedFileBase file, string CurDate, string CurTime)
        {
            String FilePath, FileExtension, fileNames, FileDate, FileTime, CopyFileName;
            CopyFileName = "";
            FilePath = null;
            fileNames = "";

            FileDate = CurDate.Replace("/", "");
            FileTime = CurTime.Replace(":", "");


            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            FileExtension = Path.GetExtension(file.FileName);
            CopyFileName = fileName + "-" + FileDate + "-" + FileTime + FileExtension;
            var physicalPath = Path.Combine(Server.MapPath("~/Documents"), CopyFileName);
            file.SaveAs(physicalPath);

            return CopyFileName;
        }

        //public JsonResult SaveCatAndOrder(int[] OrderHaveErrorId)
        /// <summary>
        /// SaveCatAndOrder
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public ActionResult SaveCatAndOrder(string orderHaveErrorId)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            string[] strOrderHaveErrorId;
            int[] OrderHaveErrorId;
            GlobalClass globalclass = new GlobalClass();
            int numberTrans = 0, ind;
            string fileName = "";
            string strId = orderHaveErrorId;
            int numberTransfer;

            //if (strId.IndexOf(',') != -1)
            //{
            strOrderHaveErrorId = strId.Split(',');
            OrderHaveErrorId = new int[strOrderHaveErrorId.Length];
            /*}
            else
            {
                strOrderHaveErrorId = new String[1];
                strOrderHaveErrorId[0] = strId;
                OrderHaveErrorId = new int[1];
            }*/


            for (ind = 0; ind < strOrderHaveErrorId.Length; ind++)
            {
                OrderHaveErrorId[ind] = System.Convert.ToInt32(strOrderHaveErrorId[ind]);
            }
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();

            CurDate = ObjDate.ShamsiDate(CurDate);
            numberTrans = LSSrv.SaveCatAndOrder(OrderHaveErrorId, CurDate, CurTime, Session["UserId"].ToString());
            ViewBag.numberTrans = " تعداد انتقال يافته " + numberTrans.ToString();
            return View();
        }
        /// <summary>
        /// UploadPackingList
        /// </summary>
        /// <param name="uploadInf"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult UploadPackingList(UploadInf uploadInf, HttpPostedFileBase file)
        {
            ViewBag.Message = "آپلود پکینگ لیست";

            return View();
        }

        /// <summary>
        ///     UploadPackingList
        /// </summary>
        /// <returns></returns>
        public ActionResult doUploadPackingList(UploadInf uploadInf, HttpPostedFileBase file)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();

            string fileName = "";
            int ind;
            int uploadInfId;
            DataTable tempDT;
            string packingListNo;
            IList<PackingListError> packingListErrors;
            int numberTransfer;
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();

            CurDate = ObjDate.ShamsiDate(CurDate);
            //  UploadFile
            fileName = UploadFile(file, CurDate, CurTime);

            //  Save TUploadInf
            uploadInfId = LSSrv.SaveTUploadInf(uploadInf, fileName, 2);
            //  FetchInf
            tempDT = FetchInf(fileName);
            Session["UserId"] = "79203112";
            numberTransfer = LSSrv.TransferPackingListInf(tempDT, uploadInf.OrderNo, CurDate, CurTime, Session["UserId"].ToString(), uploadInfId, out packingListErrors, out packingListNo);

            //ViewData["UploadDocTypeId"] = new SelectList(LSSrv.FetchTUploadDocType(), "UploadDocTypeId", "DocTypeDesc");
            ViewBag.NumberTransfer = " تعداد انتقال يافته " + numberTransfer.ToString() + " شماره سفارش " + packingListNo.ToString();
            return View(packingListErrors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showtitle"></param>
        /// <returns></returns>
        public ActionResult SearchOrder(string showtitle = "1", string orderNo = "")
        {
            ViewBag.ShowTitle = showtitle;
            ViewData["ShowTitle"] = showtitle;
            ViewData["OrderNo"] = orderNo;

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchOrderNo"></param>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult ResultOfSearchOrderNo(string SearchOrderNo, string TechnicalCode)
        {
            IList<Order> orders = LSSrv.FetchTOrder(SearchOrderNo, TechnicalCode);
            return View(orders);
        }
        /// <summary>
        /// SaveCatAndPackingList
        /// </summary>
        /// <param name="packingListErrorId"></param>
        /// <param name="TechnicalCodeInOrder"></param>
        /// <param name="ErrorType"></param>
        /// <returns></returns>
        public ActionResult SaveCatAndPackingList(string packingListErrorId, string TechnicalCodeInOrder, string ErrorType)
        {

            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            int[] PackingListErrorId, errorTypeId;
            GlobalClass globalclass = new GlobalClass();
            int numberTrans = 0, ind;
            string fileName = "";
            string[] strPackingListErrorId, strTechnicalCodeInOrder, strErrorType;

            int numberTransfer;

            strPackingListErrorId = packingListErrorId.Split(',');
            PackingListErrorId = new int[strPackingListErrorId.Length];

            strTechnicalCodeInOrder = TechnicalCodeInOrder.Split(',');

            strErrorType = ErrorType.Split(',');
            errorTypeId = new int[strErrorType.Length];

            for (ind = 0; ind < strPackingListErrorId.Length; ind++)
            {
                PackingListErrorId[ind] = System.Convert.ToInt32(strPackingListErrorId[ind]);
                errorTypeId[ind] = System.Convert.ToInt32(strErrorType[ind]);
            }

            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();

            CurDate = ObjDate.ShamsiDate(CurDate);
            Session["UserId"] = "79203112";
            numberTrans = LSSrv.SaveCatAndPackingList(PackingListErrorId, strTechnicalCodeInOrder, errorTypeId, CurDate, CurTime, Session["UserId"].ToString());

            ViewBag.numberTrans = " تعداد انتقال يافته " + numberTrans.ToString();

            return View();
        }


        public ActionResult SearchPackingList()
        {
            ViewBag.ShowTitle = "1";
            ViewData["PackingListNo"] = "";

            return View();
        }
        /// <summary>
        /// ResultOfSearchPackingListNo
        /// </summary>
        /// <param name="SearchPackingListNo"></param>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult ResultOfSearchPackingListNo(string SearchPackingListNo = "", string TechnicalCode = "")
        {
            IList<PackingListAndOrder> packingListAndOrders = LSSrv.FetchPackingListAndOrder(SearchPackingListNo, TechnicalCode, "", "", "", "");

            return View(packingListAndOrders);

        }

        // 
        public ActionResult Entrance2WareHouse()
        {
            ViewBag.Message = "ورود به انبار";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="CaseNo"></param>
        /// <param name="InvoiceNo"></param>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult Entrance2WareHouseList(string OrderNo, string CaseNo, string InvoiceNo, string TechnicalCode)
        {
            IList<PackingListAndOrder> packingListAndOrders = LSSrv.FetchPackingListAndOrder("", TechnicalCode, InvoiceNo, CaseNo, TechnicalCode, OrderNo, 1);
            return View(packingListAndOrders);
        }

        [HttpPost]
        public ActionResult Entrance2WareHouse(string ReceiveQty, string PackingListId, string OrderId)
        {
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            int[] packingListId, orderId;
            double[] receiveQty;
            GlobalClass globalclass = new GlobalClass();
            string[] strReceiveQty, strPackingListId, strOrderId;
            int ind;
            bool flag = true;
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();

            CurDate = ObjDate.ShamsiDate(CurDate);
            Session["UserId"] = "79203112";
            //  Receive QTY
            strReceiveQty = ReceiveQty.Split(',');
            receiveQty = new Double[strReceiveQty.Length];
            for (ind = 0; ind < strReceiveQty.Length; ind++)
            {
                receiveQty[ind] = System.Convert.ToDouble(strReceiveQty[ind]);
            }

            strOrderId = OrderId.Split(',');
            orderId = new int[strOrderId.Length];

            strPackingListId = PackingListId.Split(',');
            packingListId = new int[strPackingListId.Length];

            for (ind = 0; ind < strPackingListId.Length; ind++)
            {
                packingListId[ind] = System.Convert.ToInt32(strPackingListId[ind]);
                orderId[ind] = System.Convert.ToInt32(strOrderId[ind]);
            }


            flag = LSSrv.SaveTInventoryDoc(receiveQty, packingListId, orderId, 1, CurDate, CurTime, Session["UserId"].ToString());



            return View();
        }

        public ActionResult Entrance2WareHouseRep()
        {
            ViewBag.Message = " گزارش ورود به انبار";
            return View();
        }

        public ActionResult Entrance2WareHouseListRep(string OrderNo, string CaseNo, string InvoiceNo, string TechnicalCode)
        {
            IList<PackingListAndOrder> packingListAndOrders = LSSrv.FetchPackingListAndOrderRep("", TechnicalCode, InvoiceNo, CaseNo, TechnicalCode, OrderNo, -1);
            return View(packingListAndOrders);
        }
        /// <summary>
        /// DefGoods
        /// </summary>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult DefGoods(string TechnicalCode = "")
        {
            Cat cat;
            if (TechnicalCode == "")
            {
                cat = new Cat();
            }
            else
            {
                cat = LSSrv.FetchTCat(TechnicalCode, "");
                cat.FlagUpdate = 1;
            }
            ViewData["IG"] = new SelectList((IEnumerable)LSSrv.FetchTItemGroup(0), "ItemGroupId", "ItemGroupDesc");

            ViewData["IT"] = new SelectList((IEnumerable)LSSrv.FetchTItemType(0), "ItemTypeId", "ItemTypeDesc");
            ViewData["GG"] = new SelectList((IEnumerable)LSSrv.FetchTGoodsGrade(0), "GoodsGradeId", "GoodsGradeDesc");
            ViewData["TT"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(1, 0), "BaseInfId", "BaseInfDesc");
            ViewData["LG"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(2, 0), "BaseInfId", "BaseInfDesc");
            ViewData["UT"] = new SelectList((IEnumerable)LSSrv.FetchTUsageType(0), "UsageTypeId", "UsageTypeDesc");
            ViewData["U"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(3, 0), "BaseInfId", "BaseInfDesc");

            return View(cat);
        }

        public ActionResult ShowGoods()
        {
            ViewData["IG"] = new SelectList((IEnumerable)LSSrv.FetchTItemGroup(0), "ItemGroupId", "ItemGroupDesc");

            ViewData["IT"] = new SelectList((IEnumerable)LSSrv.FetchTItemType(0), "ItemTypeId", "ItemTypeDesc");
            ViewData["GG"] = new SelectList((IEnumerable)LSSrv.FetchTGoodsGrade(0), "GoodsGradeId", "GoodsGradeDesc");
            ViewData["TT"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(1, 0), "BaseInfId", "BaseInfDesc");
            ViewData["LG"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(2, 0), "BaseInfId", "BaseInfDesc");
            ViewData["UT"] = new SelectList((IEnumerable)LSSrv.FetchTUsageType(0), "UsageTypeId", "UsageTypeDesc");
            return View();
        }
        /// <summary>
        /// CatList
        /// </summary>
        /// <param name="TechnicalCode"></param>
        /// <param name="ItemName"></param>
        /// <param name="PersianName"></param>
        /// <param name="UsageTypeId"></param>
        /// <param name="ItemGroupId"></param>
        /// <param name="ItemTypeId"></param>
        /// <param name="GoodsGradeId"></param>
        /// <param name="TrackingTypeId"></param>
        /// <param name="LabelingTypeId"></param>
        /// <returns></returns>
        public ActionResult CatList(string TechnicalCode = "", string ItemName = "", string PersianName = "", int UsageTypeId = 0, int ItemGroupId = 0, int ItemTypeId = 0, int GoodsGradeId = 0, int TrackingTypeId = 0, int LabelingTypeId = 0)
        //public ActionResult CatList1()
        {
            IList<VCatClass> vCatClasses = LSSrv.FetchVCat(TechnicalCode, ItemName, PersianName, UsageTypeId, ItemGroupId, ItemTypeId, GoodsGradeId, TrackingTypeId, LabelingTypeId);

            return View(vCatClasses);
            //return View();
        }

        public ActionResult GoodInCars(string TechnicalCode)
        {
            IList<CatCarType> catCarTypes = null;
            catCarTypes = LSSrv.FetchTCatCarType(TechnicalCode, 0);


            return View(catCarTypes);
        }

        public ActionResult ShowCustomer()
        {


            //return View(customeres);
            return View();
        }
        /// <summary>
        /// CustomerList
        /// </summary>
        /// <param name="CustomerTypeId"></param>
        /// <param name="CustomerContractTypeId"></param>
        /// <returns></returns>
        public ActionResult CustomerList(int CustomerTypeId, int CustomerContractTypeId)
        {
            IList<VCustomerClass> vCustomeres = null;
            vCustomeres = LSSrv.FetchVCustomer(0, 0);

            return View(vCustomeres);
        }
        /// <summary>
        /// AddCustomer
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public ActionResult AddCustomer(int CustomerId)
        {
            IList<Customer> customers;
            Customer customer;

            ViewData["CC"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(4, 0), "BaseInfId", "BaseInfDesc");
            ViewData["CT"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(5, 0), "BaseInfId", "BaseInfDesc");


            if (CustomerId == 0)
            {
                customer = new Customer();
            }
            else
            {
                customers = LSSrv.FetchTCustomer(CustomerId, 0, 0);

                if (customers.Count> 0)
                {
                    customer = customers[0];                     
                    customer.FlagUpdate = 1;
                }
                else
                {
                    customer = new Customer();
                }
            }
            return View(customer);
        }
        /// <summary>
        /// GoodsAndPrice
        /// </summary>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult GoodsAndPrice(string TechnicalCode)
        {
            GoodsPrice goodsPrice;
            ViewData["Cat"] = LSSrv.FetchTCat(TechnicalCode, "");
            ViewData["Currency"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(6, 0), "BaseInfId", "BaseInfDesc");
            IList<GoodsPrice> goodsPrices = LSSrv.FetchGoodsPrice(TechnicalCode);
            if(goodsPrices.Count > 0)
            {
                goodsPrice = goodsPrices[0];
            }
            else
            {
                goodsPrice = new GoodsPrice();
            }

            return View(goodsPrice);
        }
        /// <summary>
        /// PrevGoodsPriceList
        /// </summary>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult PrevGoodsPriceList(string TechnicalCode)
        {
            IList<VGoodsPriceArcClass> vGoodsPriceArcClasses = LSSrv.FetchVGoodsPriceArc(TechnicalCode);

            return View(vGoodsPriceArcClasses);
        }


        public  ActionResult GoodsRequest()
        {
            ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");
            ViewData["RequestStatus"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(8, 0), "BaseInfId", "BaseInfDesc");

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public ActionResult RequestList(int CustomerId, short RequestHeaderIsActive, short iconType = 0)
        {
            IList<RequestHeader> requestHeader = LSSrv.FetchTRequestHeader("", "", "", ( CustomerId == null ? -1 : CustomerId), RequestHeaderIsActive);
            foreach(var rh in requestHeader)
            {
                rh.IconType = iconType;
            }
            return View(requestHeader);
        }

        public ActionResult AddRequest(string RequestNo)
        {
            ViewData["RequestTypeId"] = new SelectList((IEnumerable)LSSrv.FetchTBaseInfint(7, 0), "BaseInfId", "BaseInfDesc");
            return View();
        }
        /// <summary>
        /// CurRequestList 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="RequestNo"></param>
        /// <returns></returns>
        public ActionResult CurRequestList(int CustomerId, string RequestNo)
        {
            IList<VRequestClass> vRequestClasses = LSSrv.FetchVRequest(CustomerId, "", RequestNo, "", "", 0, 1, 0);


            return View(vRequestClasses);
        }


        public ActionResult Invoice()
        {
            ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");

            return View();
        }
        public ActionResult InvoiceRequestList(int CustomerId, short RequestHeaderIsActive, string RequestNo)
        {
            IList<RequestHeader> requestHeader = LSSrv.FetchTRequestHeader(RequestNo, "", "", (CustomerId == null ? -1 : CustomerId), 24);
            return View(requestHeader);
        }
        public ActionResult CreateInvoice(int CustomerId, string RequestNo)
        {
            //ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");

            //IList<VRequestClass> vRequestClasses = LSSrv.FetchVRequest(CustomerId, "", RequestNo, "", "", 0, 24, 0);


            return View();
        }
        /// <summary>
        /// RequestList4Invoice
        /// </summary>
        /// <returns></returns>
        public ActionResult RequestList4Invoice(int CustomerId, string RequestNo)
        {
            IList<VRequestClass> vRequestClasses = LSSrv.FetchVRequest(CustomerId, "", RequestNo, "", "", 0, 24, 0);

            return View(vRequestClasses);
        }
        /// <summary>
        /// ConfirmInvoice
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmInvoice()
        {
            ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");
            return View();
        }
        /// <summary>
        /// ConfirmInvoiceList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="RequestNo"></param>
        /// <returns></returns>
        public ActionResult ConfirmInvoiceList(int CustomerId, string RequestNo, short InvoiceHeaderIsActive, short InvoiceDetailIsActive )
        {
            IList<VInvoiceClass> vInvoiceClasses = LSSrv.FetchV_Invoice(RequestNo, "", CustomerId, InvoiceDetailIsActive, InvoiceHeaderIsActive);

            return View(vInvoiceClasses);
        }

        /// <summary>
        /// ConfirmInvoice
        /// </summary>
        /// <returns></returns>
        public ActionResult Factor()
        {

            ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");
            return View();
        }
        /// <summary>
        /// ConfirmInvoiceList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="RequestNo"></param>
        /// <returns></returns>
        //public ActionResult InvoiceList4Factor(int CustomerId, string RequestNo, short InvoiceDetailIsActive)
        //{
        //    IList<VInvoiceClass> vInvoiceClasses = LSSrv.FetchV_Invoice(RequestNo, "", CustomerId, InvoiceDetailIsActive, InvoiceDetailIsActive);

        //    return View(vInvoiceClasses);
        //}

        public ActionResult WarehouseDraft()
        {
            ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");
            return View();
        }

        /// <summary>
        /// WarehouseDraftList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="RequestNo"></param>
        /// <returns></returns>
        public ActionResult WarehouseDraftList(int CustomerId, string RequestNo, short InvoiceHeaderIsActive, short InvoiceDetailIsActive)
        {
            IList<VInvoiceClass> vInvoiceClasses = LSSrv.FetchV_Invoice(RequestNo, "", CustomerId, InvoiceDetailIsActive, InvoiceHeaderIsActive);
            vInvoiceClasses = vInvoiceClasses.GroupBy(g => new { g.InvoiceNo, g.InvoiceTitle, g.InvoiceDate, g.InvoiceTime, g.CustomerId, g.CustomerName, g.RequestNo }).Select(s => new VInvoiceClass() { InvoiceNo = s.Key.InvoiceNo, InvoiceTitle = s.Key.InvoiceTitle, InvoiceDate = s.Key.InvoiceDate, InvoiceTime = s.Key.InvoiceTime, CustomerId = s.Key.CustomerId, CustomerName = s.Key.CustomerName, RequestNo = s.Key.RequestNo }).ToList();
            return View(vInvoiceClasses);
        }


        public ActionResult PrintDraft(int CustomerId, string InvoiceNo)
        {
            bool flag;
            ShamsiDate.SDate ObjDate = new ShamsiDate.SDate();
            String CurTime, CurDate;
            GlobalClass globalclass = new GlobalClass();
            
            CurTime = globalclass.MakeCurTime();
            CurDate = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            CurDate += DateTime.Now.Day.ToString();
            CurDate = ObjDate.ShamsiDate(CurDate);

            IList<VInvoiceClass> vInvoiceClasses = LSSrv.FetchV_Invoice("", InvoiceNo, 0, 31, 24);
            flag = LSSrv.ChangeStatus2Print(vInvoiceClasses, CurDate, CurTime, Session["UserId"].ToString());
            return View(vInvoiceClasses);
        }

        public ActionResult ExitWarehouse()
        {
            ViewData["Customer"] = new SelectList((IEnumerable)LSSrv.FetchVCustomer(0, 0), "CustomerId", "CustomerName");

            return View();
        }

        public ActionResult ExitWarehouseList(int CustomerId, string InvoiceNo, short InvoiceHeaderIsActive, short InvoiceDetailIsActive, string RequestNo = "")
        {
            IList<VInvoiceClass> vInvoiceClasses = LSSrv.FetchV_Invoice(RequestNo, InvoiceNo, CustomerId, InvoiceDetailIsActive, InvoiceHeaderIsActive);
            vInvoiceClasses = vInvoiceClasses.GroupBy(g => new { g.InvoiceNo, g.InvoiceTitle, g.InvoiceDate, g.InvoiceTime, g.CustomerId, g.CustomerName, g.RequestNo }).Select(s => new VInvoiceClass() { InvoiceNo = s.Key.InvoiceNo, InvoiceTitle = s.Key.InvoiceTitle, InvoiceDate = s.Key.InvoiceDate, InvoiceTime = s.Key.InvoiceTime, CustomerId = s.Key.CustomerId, CustomerName = s.Key.CustomerName, RequestNo = s.Key.RequestNo }).ToList();
            return View(vInvoiceClasses);
        }

        public ActionResult ExitfromwarehouseCurInvoice(int CustomerId, string InvoiceNo)
        {
            return View();
        }

        public ActionResult ExitfromwarehouseCurInvoiceList(int CustomerId, string InvoiceNo)
        {
            IList<VInvoiceClass> vInvoiceClasses = LSSrv.FetchV_Invoice("", InvoiceNo, CustomerId, 32, 24);
            
            return View(vInvoiceClasses);
        }

        /// <summary>
        /// ConfirmInvoiceList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="RequestNo"></param>
        /// <returns></returns>
        public ActionResult ExitWarehouseWithoutFactoList(int CustomerId, string ExitNo, string InvoiceNo, short ExitHeaderIsActive, short ExitDetailIsActive)
        {
            IList<VExitWarehouseHeaderClass> vExitFromWarehouseClasses = LSSrv.FetchV_ExitWarehouseHeader(ExitNo, InvoiceNo, CustomerId, ExitHeaderIsActive, ExitDetailIsActive);

            return View(vExitFromWarehouseClasses);
        }
        /// <summary>
        /// ExitfromwarehouseCurDoc4IssueFactor
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ExitNo"></param>
        /// <returns></returns>
        public ActionResult ExitfromwarehouseCurDoc4IssueFactor(int CustomerId, string ExitNo)
        {
            IList<VExitWarehouseClass> vExitWarehouseClasses = LSSrv.FetchVExitWarehouseClass(ExitNo, "", 0, 24, 24, "");
            return View(vExitWarehouseClasses);
        }

    }


}