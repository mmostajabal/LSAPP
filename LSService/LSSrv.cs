using FrameWorkSRV;
using LSData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LSService
{
    public class LSSrv
    {
        //public static IEnumerable FetchTUploadDocType()
        //{
        //    IList<UploadDocType> uploadDocTypes = null;
        //    IGenericRepository<TUploadDocType, UploadDocType> tUploadDocTypeRep = new TUploadDocTypeRepository();

        //    uploadDocTypes = tUploadDocTypeRep.GetAll(true).Item2.ToList();

        //    return uploadDocTypes;
        //}
        /// <summary>
        /// SaveTUploadInf
        /// </summary>
        /// <param name="uploadInf"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int SaveTUploadInf(UploadInf uploadInf, string fileName, int UploadDocTypeId)
        {
            IGenericRepository<TUploadInf, UploadInf> tUploadInfRep = new TUploadInfRepository();
            TUploadInf tUploadInf;
            uploadInf.DocPath = fileName;
            uploadInf.UploadDocTypeId = UploadDocTypeId;

            tUploadInf = tUploadInfRep.Cast<TUploadInf>(uploadInf, 1);

            tUploadInfRep.Add(tUploadInf);
            tUploadInfRep.Save();
            return tUploadInf.UploadInfId;
        }


        /// <summary>
        /// TransferInf
        /// </summary>
        /// <param name="tempDT"></param>
        /// <param name="uploadDocTypeId"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public static int TransferInf(DataTable tempDT, int uploadDocTypeId, string orderNo, string curDate, string curTime, string curUser, int uploadInfId, out IList<OrderHaveError> orderHaveErrors)
        {
            int numberTansfer = 0;
            //string  orderNo;
            int curYear;
            orderHaveErrors = new List<OrderHaveError>(); 
            curYear = System.Convert.ToInt32(curDate.Substring(0, 4));
            //orderNo = "";
            //switch (uploadDocTypeId)
            //{
            //    case 1:
            //orderNo = MakeOrderNo(curYear, 1);
            SaveOrder(tempDT, orderNo, curDate, curTime, curUser, uploadInfId, out numberTansfer, out orderHaveErrors);
            //break;
            //    case 2:
            //        break;
            //}

            return numberTansfer;
        }

        /// <summary>
        /// SaveOrder
        /// </summary>
        /// <param name="tempDT"></param>
        /// <param name="orderNo"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        private static void SaveOrder(DataTable tempDT, string orderNo, string curDate, string curTime, string curUser, int uploadInfId, out int numberTansfer, out IList<OrderHaveError> orderHaveErrors)
        {
            int ind;
            TOrder tOrder;
            TOrderHaveError tOrderHaveError;
            Cat cat;
            orderHaveErrors = null;

            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            IGenericRepository<TOrderHaveError, OrderHaveError> tOrderHaveErrorRep = new TOrderHaveErrorRepository();
            var predicateTTOrderHaveError = PredicateBuilder.True<TOrderHaveError>();

            numberTansfer = 0;
            for (ind = 0; ind < tempDT.Rows.Count; ind++)
            {
                //  FetchTCat
                if (tempDT.Rows[ind]["PartNo"].ToString().Trim() != "")
                {
                    cat = FetchTCat(tempDT.Rows[ind]["PartNo"].ToString().Trim(), "");
                    if (cat != null)
                    {
                        tOrder = new TOrder();
                        tOrder.TechnicalCode = tempDT.Rows[ind]["PartNo"].ToString().Trim();
                        tOrder.Quantity = System.Convert.ToDouble(tempDT.Rows[ind]["Quantity"].ToString().Trim());
                        tOrder.Price = System.Convert.ToDouble(tempDT.Rows[ind]["price"].ToString().Trim());
                        tOrder.PriceAED = System.Convert.ToDouble(tempDT.Rows[ind]["price AED"].ToString().Trim());
                        tOrder.Tariff = tempDT.Rows[ind]["Tariff"].ToString().Trim();
                        tOrder.NetWeight = System.Convert.ToDouble(tempDT.Rows[ind]["Net Weight"].ToString().Trim());
                        tOrder.GrossWeight = System.Convert.ToDouble(tempDT.Rows[ind]["Gross Weith"].ToString().Trim());
                        tOrder.OrderNo = orderNo;
                        tOrder.EntryDataDate = curDate;
                        tOrder.EntryDataTime = curTime;
                        tOrder.EntryDataPersonalId = curUser;
                        tOrder.UploadInfId = uploadInfId;
                        tOrderRep.Add(tOrder);
                        numberTansfer++;
                    }
                    else
                    {
                        tOrderHaveError = new TOrderHaveError();
                        tOrderHaveError.TechnicalCode = tempDT.Rows[ind]["PartNo"].ToString().Trim();
                        tOrderHaveError.Quantity = System.Convert.ToDouble(tempDT.Rows[ind]["Quantity"].ToString().Trim());
                        tOrderHaveError.Price = System.Convert.ToDouble(tempDT.Rows[ind]["price"].ToString().Trim());
                        tOrderHaveError.PriceAED = System.Convert.ToDouble(tempDT.Rows[ind]["price AED"].ToString().Trim());
                        tOrderHaveError.Tariff = tempDT.Rows[ind]["Tariff"].ToString().Trim();
                        tOrderHaveError.NetWeight = System.Convert.ToDouble(tempDT.Rows[ind]["Net Weight"].ToString().Trim());
                        tOrderHaveError.GrossWeight = System.Convert.ToDouble(tempDT.Rows[ind]["Gross Weith"].ToString().Trim());
                        tOrderHaveError.OrderNo = orderNo;
                        tOrderHaveError.EntryDataDate = curDate;
                        tOrderHaveError.EntryDataTime = curTime;
                        tOrderHaveError.EntryDataPersonalId = curUser;

                        tOrderHaveError.ItemName = tempDT.Rows[ind]["Part Name"].ToString().Trim();
                        tOrderHaveError.PersianName = tempDT.Rows[ind]["نام فارسی"].ToString().Trim();
                        tOrderHaveError.IsActive = 1;
                        tOrderHaveError.UploadInfId = uploadInfId;

                        tOrderHaveErrorRep.Add(tOrderHaveError);


                        //OrderHaveError orderHaveError = tOrderHaveErrorRep.Cast<OrderHaveError>(tOrderHaveError, 1);
                        //orderHaveError.IsSelected = true;
                        //orderHaveErrors.Add(orderHaveError);
                    }
                }
            }

            tOrderRep.Save();
            tOrderHaveErrorRep.Save();
            predicateTTOrderHaveError = predicateTTOrderHaveError.And(o => o.OrderNo == orderNo);
            orderHaveErrors = tOrderHaveErrorRep.FindBy(predicateTTOrderHaveError, true).Item2.ToList();
            foreach (var odhe in orderHaveErrors)
            {
                odhe.IsSelected = true;
            }
        }

   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderHaveErrorId"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public static int SaveCatAndOrder(int[] orderHaveErrorId, string curDate, string curTime, string curUser)
        {
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            IGenericRepository<TOrderHaveError, OrderHaveError> tOrderHaveErrorRep = new TOrderHaveErrorRepository();
            TCat tCat;
            TOrder tOrder;
            var predicateTTOrderHaveError = PredicateBuilder.True<TOrderHaveError>();
            IList<TOrderHaveError> tOrderHaveErrors;
            int numberTransfer = 0;
            predicateTTOrderHaveError = predicateTTOrderHaveError.And(c => orderHaveErrorId.Contains(c.OrderHaveErrorId));
            predicateTTOrderHaveError = predicateTTOrderHaveError.And(c => c.IsActive == 1);

            tOrderHaveErrors = tOrderHaveErrorRep.FindBy(predicateTTOrderHaveError, false).Item1.ToList();
            numberTransfer = tOrderHaveErrors.Count();

            foreach (var tohe in tOrderHaveErrors)
            {
                // tcat
                tCat = new TCat();
                tCat.TechnicalCode = tohe.TechnicalCode;
                tCat.ItemName = tohe.ItemName;
                tCat.PersianName = tohe.PersianName;

                tCatRep.Add(tCat);

                //  TOrder
                tOrder = new TOrder();
                tOrder.TechnicalCode = tohe.TechnicalCode;
                tOrder.Quantity = tohe.Quantity;
                tOrder.Price = tohe.Price;
                tOrder.PriceAED = tohe.PriceAED;
                tOrder.Tariff = tohe.Tariff;
                tOrder.NetWeight = tohe.NetWeight;
                tOrder.GrossWeight = tohe.GrossWeight;
                tOrder.OrderNo = tohe.OrderNo;
                tOrder.UploadInfId = tohe.UploadInfId;
                tOrder.EntryDataDate = curDate;
                tOrder.EntryDataTime = curTime;
                tOrder.EntryDataPersonalId = curUser;

                tOrderRep.Add(tOrder);

                tohe.IsActive = 2;
            }

            tCatRep.Save();
            tOrderRep.Save();
            tOrderHaveErrorRep.Save();


            return numberTransfer;
        }
        /// <summary>
        /// SaveCatAndPackingList
        /// </summary>
        /// <param name="packingListErrorId"></param>
        /// <param name="strTechnicalCodeInOrder"></param>
        /// <param name="errorTypeId"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int SaveCatAndPackingList(int[] packingListErrorId, string[] strTechnicalCodeInOrder, int[] errorTypeId, string curDate, string curTime, string curUser)
        {
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            IGenericRepository<TPackingList, PackingList> tPackingListRep = new TPackingListRepository();
            IGenericRepository<TPackingListError, PackingListError> tPackingListErrorRep = new TPackingListErrorRepository();

            TCat tCat;
            TOrder tOrder;
            TPackingList tPackingList;
            var predicateTPackingListError = PredicateBuilder.True<TPackingListError>();
            string curTechnicalCode = "";
            int indArr = -1;
            IList<TPackingListError> tPackingListErrors;
            int numberTransfer = 0;
            string technicalCodeOder = "";
            int orderId = 0;

            predicateTPackingListError = predicateTPackingListError.And(c => packingListErrorId.Contains(c.PackingListErrorId));
            predicateTPackingListError = predicateTPackingListError.And(c => c.IsActive == 1);

            tPackingListErrors = tPackingListErrorRep.FindBy(predicateTPackingListError, false).Item1.ToList();
            numberTransfer = tPackingListErrors.Count();

            foreach (var tple in tPackingListErrors)
            {
                indArr = Array.FindIndex(packingListErrorId, item => item == tple.PackingListErrorId);

                //  TOrder
                var predicateTOrder = PredicateBuilder.True<TOrder>();

                if (indArr != -1)
                {
                    // tcat
                    if (errorTypeId[indArr] == 1)
                    {
                        tCat = new TCat();
                        tCat.TechnicalCode = tple.TechnicalCode;
                        tCat.ItemName = tple.PartName;

                        tCatRep.Add(tCat);
                    }

                    curTechnicalCode = strTechnicalCodeInOrder[indArr];
                    predicateTOrder = predicateTOrder.And(c => c.OrderNo == tple.OrderNo);
                    predicateTOrder = predicateTOrder.And(c => c.TechnicalCode == curTechnicalCode);

                    tOrder = tOrderRep.FindBy(predicateTOrder, false).Item1.FirstOrDefault();
                    if (tOrder != null)
                    {
                        tOrder.PackingListTechnicalCode = tple.TechnicalCode;
                        tOrder.PackListQTY += tple.ShipQty;
                        technicalCodeOder = tOrder.TechnicalCode;
                        orderId = tOrder.OrderId;
                    }

                    //  TPackingList
                    tPackingList = new TPackingList();
                    tPackingList.TechnicalCode = tple.TechnicalCode;
                    tPackingList.InvoiceNo = tple.InvoiceNo;
                    tPackingList.CaseNo = tple.CaseNo;
                    tPackingList.CountryOfOrigin = tple.CountryOfOrigin;
                    tPackingList.ShipQty = tple.ShipQty;
                    tPackingList.HSCode = tple.HSCode;
                    tPackingList.UnitPrice = tple.UnitPrice;
                    tPackingList.GrossWeight = tple.GrossWeight;
                    tPackingList.NetWeight = tple.NetWeight;
                    tPackingList.OrderNo = tple.OrderNo;
                    tPackingList.PackingListNo = tple.PackingListNo;
                    tPackingList.TechnicalCodeOder = technicalCodeOder;
                    tPackingList.EntryDataDate = curDate;
                    tPackingList.EntryDataTime = curTime;
                    tPackingList.EntryDataPersonalId = curUser;
                    tPackingList.UploadInfId = tple.UploadInfId;
                    tPackingList.OrderId = orderId;

                    tPackingListRep.Add(tPackingList);

                    //  TPackingListError
                    tple.IsActive = 2;
                }
            }

            tCatRep.Save();
            tOrderRep.Save();
            tPackingListRep.Save();
            tPackingListErrorRep.Save();


            return numberTransfer;

        }

        public static bool SaveTInventoryDoc(double[] receiveQty, int[] packingListId, int[] orderId, int InventoryDocTypeId, string curDate, string curTime, string curUser)
        {
            IGenericRepository<TPackingList, PackingList> tPackingListRep = new TPackingListRepository();
            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            IGenericRepository<TInventoryDocHeader, InventoryDocHeader> tInventoryDocHeaderRep = new TInventoryDocHeaderRepository();
            IGenericRepository<TWareHouseStock, WareHouseStock> tWareHouseStockRep = new TWareHouseStockRepository();
            IList<TOrder> tOrders;
            IList<TPackingList> tPackingLists;
            IList<TInventoryDocDetail> tInventoryDocDetails = new List<TInventoryDocDetail>();
            TInventoryDocDetail tInventoryDocDetail;
            TOrder qPOrder;
            TPackingList qPackingList;
            TWareHouseStock wareHouseStock;
            string curTechnicalCode;
            int ind, curPackingListId, curOrderId;
            var predicateTPackingList = PredicateBuilder.True<TPackingList>();
            var predicateTOrder = PredicateBuilder.True<TOrder>();
            var predicateTWareHouseStock = PredicateBuilder.True<TWareHouseStock>();

            TInventoryDocHeader tInventoryDocHeader = new TInventoryDocHeader();

            string inventoryDocNo = "";
            predicateTPackingList = predicateTPackingList.And(c => packingListId.Contains(c.PackingListId));
            predicateTOrder = predicateTOrder.And(c => orderId.Contains(c.OrderId));

            //  tOrders 
            tOrders = tOrderRep.FindBy(predicateTOrder, false).Item1.ToList();
            //  tPackingLists 
            tPackingLists = tPackingListRep.FindBy(predicateTPackingList, false).Item1.ToList();

            //  MakeDocNo
            inventoryDocNo = MakeDocNo(System.Convert.ToInt32(curDate.Substring(0, 4)), InventoryDocTypeId);

            //  TInventoryDocHeader
            tInventoryDocHeader.DocNo = inventoryDocNo;
            tInventoryDocHeader.InventoryDocTypeId = InventoryDocTypeId;
            tInventoryDocHeader.IsActive = 1;
            tInventoryDocHeader.EntryDataDate = curDate;
            tInventoryDocHeader.EntryDataTime = curTime;
            tInventoryDocHeader.EntryDataPersonalId = curUser;

            for (ind = 0; ind < receiveQty.Length; ind++)
            {
                curPackingListId = packingListId[ind];
                curOrderId = orderId[ind];
                curTechnicalCode = "";

                //  Update PackingList
                qPackingList = tPackingLists.FirstOrDefault(o => o.PackingListId == curPackingListId);
                if (qPackingList != null)
                {
                    qPackingList.IsActive = (qPackingList.ConfirmShipQty + receiveQty[ind] >= qPackingList.ShipQty ? (short)2 : qPackingList.IsActive);
                    qPackingList.ConfirmShipQty += receiveQty[ind];
                    curTechnicalCode = qPackingList.TechnicalCode;
                }
                //  Update Order
                qPOrder = tOrders.FirstOrDefault(o => o.OrderId == curOrderId);
                if (qPOrder != null)
                {
                    qPOrder.IsActive = (qPOrder.RecieveOrderQuantity + receiveQty[ind] >= qPOrder.Quantity ? (short)2 : qPOrder.IsActive);
                    qPOrder.RecieveOrderQuantity += receiveQty[ind];
                }
                //  TInventoryDocDetail
                tInventoryDocDetail = new TInventoryDocDetail();

                tInventoryDocDetail.TechnicalCode = curTechnicalCode;
                tInventoryDocDetail.OrderId = curOrderId;
                tInventoryDocDetail.PackingListId = curPackingListId;
                tInventoryDocDetail.Qty = receiveQty[ind];
                tInventoryDocDetail.Explain = "";

                tInventoryDocDetails.Add(tInventoryDocDetail);

                //  TWareHouseStock
                wareHouseStock = tWareHouseStockRep.FindBy(o => o.TechnicalCode == curTechnicalCode, false).Item1.FirstOrDefault();
                if(wareHouseStock != null)
                {
                    wareHouseStock.StockQty += receiveQty[ind];
                    tWareHouseStockRep.Update(wareHouseStock);
                }
                else
                {
                    wareHouseStock = new TWareHouseStock();
                    wareHouseStock.TechnicalCode = curTechnicalCode;
                    wareHouseStock.StockQty = receiveQty[ind];

                    tWareHouseStockRep.Add(wareHouseStock);
                }

            }

            tInventoryDocHeader.TInventoryDocDetails = tInventoryDocDetails;

            tInventoryDocHeaderRep.Add(tInventoryDocHeader);

            tPackingListRep.Save();
            tOrderRep.Save();
            tInventoryDocHeaderRep.Save();
            tWareHouseStockRep.Save();
            return true;
        }



        /// <summary>
        /// CatList
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <param name="itemName"></param>
        /// <param name="persianName"></param>
        /// <param name="usageTypeId"></param>
        /// <param name="itemGroupId"></param>
        /// <param name="itemTypeId"></param>
        /// <param name="goodsGradeId"></param>
        /// <param name="trackingTypeId"></param>
        /// <param name="labelingTypeId"></param>
        /// <returns></returns>
        public static IList<VCatClass> FetchVCat(string technicalCode, string itemName, string persianName, int usageTypeId, int itemGroupId, int itemTypeId, int goodsGradeId, int trackingTypeId, int labelingTypeId)
        {
            IList<VCatClass> vcats = null;
            var predicateVCat = PredicateBuilder.True<VCat>();
            IGenericRepository<VCat, VCatClass> vCatRep = new VCatRepository();
            if (technicalCode != "")
            {
                predicateVCat = predicateVCat.And(c => c.TechnicalCode == technicalCode);
            }

            if (itemName != "")
            {
                predicateVCat = predicateVCat.And(c => c.ItemName.ToUpper().Contains(itemName.ToUpper()));
            }
            if (persianName != "")
            {
                predicateVCat = predicateVCat.And(c => c.PersianName.Contains(persianName));
            }
            if (usageTypeId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.UsageTypeId == usageTypeId);
            }
            if (usageTypeId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.UsageTypeId == usageTypeId);
            }
            if (itemGroupId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.ItemGroupId == itemGroupId);
            }

            if (itemTypeId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.ItemTypeId == itemTypeId);
            }

            if (goodsGradeId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.GoodsGradeId == goodsGradeId);
            }
            if (trackingTypeId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.TrackingTypeId == trackingTypeId);
            }

            if (labelingTypeId != 0)
            {
                predicateVCat = predicateVCat.And(c => c.LabelingTypeId == labelingTypeId);
            }

            vcats = vCatRep.FindBy(predicateVCat, true).Item2.ToList();

            return vcats;
        }

   
        /// <summary>
        /// TransferPackingListInf
        /// </summary>
        /// <param name="tempDT"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="v"></param>
        /// <param name="uploadInfId"></param>
        /// <param name="orderHaveErrors"></param>
        /// <param name="packingListNo"></param>
        /// <returns></returns> 
        public static int TransferPackingListInf(DataTable tempDT, string orderNo, string curDate, string curTime, string curUser, int uploadInfId, out IList<PackingListError> packingListErrors, out string packingListNo)
        {
            int numberTansfer = 0;
            //string  orderNo;
            int curYear;
            packingListErrors = new List<PackingListError>();
            curYear = System.Convert.ToInt32(curDate.Substring(0, 4));
            packingListNo = "";
            //  packingListNo 
            packingListNo = MakeOrderNo(curYear, 2);
            //  Save PackingList
            SavePackingList(tempDT, orderNo, packingListNo, curDate, curTime, curUser, uploadInfId, out numberTansfer,
                out packingListErrors);

            return numberTansfer;
        }


        /// <summary>
        /// SavePackingList
        /// </summary>
        /// <param name="tempDT"></param>
        /// <param name="orderNo"></param>
        /// <param name="packingListNo"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="curUser"></param>
        /// <param name="uploadInfId"></param>
        /// <param name="numberTansfer"></param>
        /// <param name="packingListErrors"></param>
        /// <returns></returns>
        private static bool SavePackingList(DataTable tempDT, string orderNo, string packingListNo, string curDate, string curTime, string curUser, int uploadInfId, out int numberTansfer, out IList<PackingListError> packingListErrors)
        {
            int ind;
            TPackingList tPackingList;
            TPackingListError tPackingListError;
            Cat cat;
            string curOrderNo, curTechnicalCode;
            packingListErrors = null;

            IGenericRepository<TPackingList, PackingList> tPackingListRep = new TPackingListRepository();
            IGenericRepository<TPackingListError, PackingListError> tPackingListErrorRep = new TPackingListErrorRepository();
            var predicateTPackingListError = PredicateBuilder.True<TPackingListError>();
            IList<Order> orders = null;
            numberTansfer = 0;
            for (ind = 0; ind < tempDT.Rows.Count; ind++)
            {
                //  FetchTCat
                if (tempDT.Rows[ind]["Part-Number"].ToString().Trim() != "")
                {
                    curOrderNo = tempDT.Rows[ind]["OrderNo"].ToString().Trim();
                    curOrderNo = (curOrderNo == "" ? orderNo : curOrderNo);
                    curTechnicalCode = tempDT.Rows[ind]["Part-Number"].ToString().Trim();
                    cat = FetchTCat(curTechnicalCode, "");
                    orders = FetchTOrder(curOrderNo, curTechnicalCode);
                    if (cat != null && orders.Count > 0)
                    {
                        tPackingList = new TPackingList();
                        tPackingList.TechnicalCode = curTechnicalCode;
                        tPackingList.InvoiceNo = tempDT.Rows[ind]["Invoice No"].ToString().Trim();
                        tPackingList.CaseNo = tempDT.Rows[ind]["Case No"].ToString().Trim();
                        tPackingList.CountryOfOrigin = tempDT.Rows[ind]["Country of Origin"].ToString().Trim();
                        tPackingList.ShipQty = System.Convert.ToDouble(tempDT.Rows[ind]["Ship Qty"].ToString().Trim());
                        tPackingList.HSCode = tempDT.Rows[ind]["H/S Code"].ToString().Trim();
                        tPackingList.UnitPrice = System.Convert.ToDouble(tempDT.Rows[ind]["Unit Price"].ToString().Trim());
                        tPackingList.GrossWeight = System.Convert.ToDouble(tempDT.Rows[ind]["G#Weight"].ToString().Trim());
                        tPackingList.NetWeight = System.Convert.ToDouble(tempDT.Rows[ind]["N#Weight"].ToString().Trim());
                        tPackingList.OrderNo = curOrderNo;
                        tPackingList.PackingListNo = packingListNo;
                        tPackingList.TechnicalCodeOder = orders[0].TechnicalCode;
                        tPackingList.OrderId = orders[0].OrderId;

                        tPackingList.EntryDataDate = curDate;
                        tPackingList.EntryDataTime = curTime;
                        tPackingList.EntryDataPersonalId = curUser;
                        tPackingList.UploadInfId = uploadInfId;

                        tPackingListRep.Add(tPackingList);
                        numberTansfer++;
                    }
                    else
                    {
                        curOrderNo = tempDT.Rows[ind]["OrderNo"].ToString().Trim();
                        curTechnicalCode = tempDT.Rows[ind]["Part-Number"].ToString().Trim();

                        tPackingListError = new TPackingListError();
                        tPackingListError.TechnicalCode = curTechnicalCode;
                        tPackingListError.InvoiceNo = tempDT.Rows[ind]["Invoice No"].ToString().Trim();
                        tPackingListError.CaseNo = tempDT.Rows[ind]["Case No"].ToString().Trim();
                        tPackingListError.PartName = tempDT.Rows[ind]["Part Name"].ToString().Trim();
                        tPackingListError.CountryOfOrigin = tempDT.Rows[ind]["Country of Origin"].ToString().Trim();
                        tPackingListError.ShipQty = System.Convert.ToDouble(tempDT.Rows[ind]["Ship Qty"].ToString().Trim());
                        tPackingListError.HSCode = tempDT.Rows[ind]["H/S Code"].ToString().Trim();
                        tPackingListError.UnitPrice = System.Convert.ToDouble(tempDT.Rows[ind]["Unit Price"].ToString().Trim());
                        tPackingListError.GrossWeight = System.Convert.ToDouble(tempDT.Rows[ind]["G#Weight"].ToString().Trim());
                        tPackingListError.NetWeight = System.Convert.ToDouble(tempDT.Rows[ind]["N#Weight"].ToString().Trim());
                        tPackingListError.OrderNo = (curOrderNo == "" ? orderNo : curOrderNo);
                        tPackingListError.PackingListNo = packingListNo;
                        tPackingListError.ErrorType = (short)(orders.Count == 0 ? 2 : 1);

                        tPackingListError.EntryDataDate = curDate;
                        tPackingListError.EntryDataTime = curTime;
                        tPackingListError.EntryDataPersonalId = curUser;
                        tPackingListError.UploadInfId = uploadInfId;
                        tPackingListError.IsActive = 1;
                        tPackingListErrorRep.Add(tPackingListError);

                        //OrderHaveError orderHaveError = tOrderHaveErrorRep.Cast<OrderHaveError>(tOrderHaveError, 1);
                        //orderHaveError.IsSelected = true;
                        //orderHaveErrors.Add(orderHaveError);
                    }
                }
            }

            tPackingListRep.Save();
            tPackingListErrorRep.Save();

            predicateTPackingListError = predicateTPackingListError.And(o => o.PackingListNo == packingListNo && o.IsActive == 1);
            packingListErrors = tPackingListErrorRep.FindBy(predicateTPackingListError, true).Item2.ToList();
            foreach (var odhe in packingListErrors)
            {
                odhe.IsSelected = true;
            }

            return true;
        }

      



        /// <summary>
        /// MakeOrderNo
        /// </summary>
        /// <param name="curYear"></param>
        /// <returns></returns>
        private static string MakeOrderNo(int curYear, int serialType)
        {
            string orderNo = "";
            int serialNo = 0;
            IGenericRepository<TSerial, OrderSerial> tOrderSerialRep = new TSerialRepository();
            var predicateTOrderSerial = PredicateBuilder.True<TSerial>();
            predicateTOrderSerial = predicateTOrderSerial.And(c => c.CurYear == curYear);
            predicateTOrderSerial = predicateTOrderSerial.And(c => c.SerialType == serialType);


            var q = tOrderSerialRep.FindBy(predicateTOrderSerial, false).Item1.FirstOrDefault();
            if (q != null)
            {
                serialNo = q.OrderSerialNo;
                q.OrderSerialNo = q.OrderSerialNo + 1;
            }
            else
            {
                serialNo = 1;
                TSerial orderSerial = new TSerial();
                orderSerial.OrderSerialNo = 2;
                orderSerial.CurYear = curYear;
                orderSerial.SerialType = serialType;

                tOrderSerialRep.Add(orderSerial);
            }

            tOrderSerialRep.Save();

            orderNo = curYear + serialNo.ToString().PadLeft(6, '0');

            return orderNo;
        }
        /// <summary>
        /// MakeDocNo
        /// </summary>
        /// <param name="curYear"></param>
        /// <param name="serialType"></param>
        /// <param name="docType"></param>
        /// <returns></returns>
        private static string MakeDocNo(int curYear, int docType)
        {
            string docNo = "";
            int serialNo = 0;
            IGenericRepository<TDocSerial, DocSerial> tDocSerialRep = new TDocSerialRepository();
            var predicateTDocSerial = PredicateBuilder.True<TDocSerial>();
            predicateTDocSerial = predicateTDocSerial.And(c => c.CurYear == curYear);
            predicateTDocSerial = predicateTDocSerial.And(c => c.InventoryDocTypeId == docType);


            var q = tDocSerialRep.FindBy(predicateTDocSerial, false).Item1.FirstOrDefault();
            if (q != null)
            {
                serialNo = q.DocSerialNo;
                q.DocSerialNo = q.DocSerialNo + 1;
            }
            else
            {
                serialNo = 1;
                TDocSerial docSerial = new TDocSerial();
                docSerial.DocSerialNo = 2;
                docSerial.CurYear = curYear;
                docSerial.InventoryDocTypeId = docType;

                tDocSerialRep.Add(docSerial);
            }

            tDocSerialRep.Save();

            docNo = curYear.ToString() + (docType < 10 ? "0" : "") + docType.ToString() + serialNo.ToString().PadLeft(6, '0');

            return docNo;
        }
        /// <summary>
        /// FetchTCat
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <param name="pNC"></param>
        /// <returns></returns>
        public static Cat FetchTCat(string technicalCode, string pNC)
        {
            Cat cat;
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            var predicateTCat = PredicateBuilder.True<TCat>();
            if (technicalCode != "")
            {
                predicateTCat = predicateTCat.And(c => c.TechnicalCode == technicalCode);
            }
            if (pNC != "")
            {
                predicateTCat = predicateTCat.And(c => c.PNC == pNC);
            }

            cat = tCatRep.FindBy(predicateTCat, true).Item2.FirstOrDefault();

            return cat;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchOrderNo"></param>
        /// <param name="TechnicalCode"></param>
        /// <returns></returns>
        public static IList<Order> FetchTOrder(string searchOrderNo, string technicalCode)
        {
            IList<Order> orders = null;
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            var predicateTOrder = PredicateBuilder.True<TOrder>();
            if (searchOrderNo != "")
            {
                predicateTOrder = predicateTOrder.And(c => c.OrderNo == searchOrderNo);
            }
            if (technicalCode != "")
            {
                predicateTOrder = predicateTOrder.And(c => c.TechnicalCode == technicalCode);
            }
            orders = (from o in tOrderRep.FindBy(predicateTOrder, false).Item1.ToList()
                      from c in tCatRep.GetAll().Where(obj => obj.TechnicalCode == o.TechnicalCode)
                      select new Order()
                      {
                          OrderId = o.OrderId,
                          TechnicalCode = o.TechnicalCode,

                          ItemName = c.ItemName,
                          PersianName = c.PersianName,

                          Quantity = o.Quantity,
                          Price = o.Price,
                          PriceAED = o.PriceAED,
                          Tariff = o.Tariff,
                          NetWeight = o.NetWeight,
                          GrossWeight = o.GrossWeight,
                          OrderNo = o.OrderNo,
                          EntryDataDate = o.EntryDataDate,
                          EntryDataTime = o.EntryDataTime

                      }).ToList();

            return orders;
        }

        public static IList<PackingListAndOrder> FetchPackingListAndOrder(string searchPackingListNo, string technicalCode, string invoiceNo, string caseNo, string technicalCodeOder, string orderNo, short isActive = -1)
        {
            IList<PackingListAndOrder> packingListAndOrders = null;
            IList<Order> orders = null;
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            IGenericRepository<TPackingList, PackingList> tPackingListRep = new TPackingListRepository();

            var predicateTPackingList = PredicateBuilder.True<TPackingList>();

            if (searchPackingListNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.PackingListNo == searchPackingListNo);
            }
            if (orderNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.OrderNo == orderNo);
            }

            if (technicalCode != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.TechnicalCode == technicalCode);
            }

            if (invoiceNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.InvoiceNo == invoiceNo);
            }
            if (caseNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.CaseNo == caseNo);
            }
            if (technicalCodeOder != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.TechnicalCodeOder == technicalCodeOder);
            }

            if (isActive != -1)
            {
                predicateTPackingList = predicateTPackingList.And(c => c.IsActive == isActive);
            }

            packingListAndOrders = (from pl in tPackingListRep.FindBy(predicateTPackingList, false).Item1.ToList()
                                    from or in tOrderRep.GetAll(false).Item1.Where(o => o.OrderId == pl.OrderId)
                                    from c1 in tCatRep.GetAll(false).Item1.Where(o => o.TechnicalCode == pl.TechnicalCode)
                                    from c2 in tCatRep.GetAll(false).Item1.Where(o => o.TechnicalCode == or.TechnicalCode)
                                    select new PackingListAndOrder()
                                    {
                                        OrderId = or.OrderId,
                                        OrderTechnicalCode = or.TechnicalCode,
                                        OrderItemName = c2.ItemName,
                                        OrderQuantity = or.Quantity,
                                        OrderPrice = or.Price,
                                        OrderPriceAED = or.PriceAED,
                                        OrderTariff = or.Tariff,
                                        OrderNetWeight = or.NetWeight,
                                        OrderGrossWeight = or.GrossWeight,
                                        OrderNo = or.OrderNo,
                                        OrderEntryDataDate = or.EntryDataDate,
                                        RecieveOrderQuantity = or.RecieveOrderQuantity,

                                        PackingListId = pl.PackingListId,
                                        PackingListNo = pl.PackingListNo,
                                        InvoiceNo = pl.InvoiceNo,
                                        CaseNo = pl.CaseNo,
                                        CountryOfOrigin = pl.CountryOfOrigin,
                                        PackingListTechnicalCode = pl.TechnicalCode,
                                        PackingListItemName = c1.ItemName,
                                        ShipQty = pl.ShipQty,
                                        ConfirmShipQty = pl.ConfirmShipQty,
                                        HSCode = pl.HSCode,
                                        PackingListUnitPrice = pl.UnitPrice,
                                        PackingListNetWeight = pl.NetWeight,
                                        PackingListGrossWeight = pl.GrossWeight,
                                        PackingListEntryDataDate = pl.EntryDataDate,
                                        PartName = c1.ItemName,
                                        NetWeight = pl.NetWeight,
                                        GrossWeight = pl.GrossWeight,
                                        IsSelected = true

                                    }).ToList();

            return packingListAndOrders;
        }

        public static IList<PackingListAndOrder> FetchPackingListAndOrderRep(string searchPackingListNo, string technicalCode, string invoiceNo, string caseNo, string technicalCodeOder, string orderNo, short isActive = -1)
        {
            IList<PackingListAndOrder> packingListAndOrders = null;
            IList<Order> orders = null;
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            IGenericRepository<TOrder, Order> tOrderRep = new TOrderRepository();
            IGenericRepository<TPackingList, PackingList> tPackingListRep = new TPackingListRepository();
            IGenericRepository<TInventoryDocDetail, InventoryDocDetail> tInventoryDocDetailRep = new TInventoryDocDetailRepository();
            IGenericRepository<TInventoryDocHeader, InventoryDocHeader> tInventoryDocHeaderRep = new TInventoryDocHeaderRepository();

            var predicateTPackingList = PredicateBuilder.True<TPackingList>();

            if (searchPackingListNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.PackingListNo == searchPackingListNo);
            }
            if (orderNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.OrderNo == orderNo);
            }

            if (technicalCode != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.TechnicalCode == technicalCode);
            }

            if (invoiceNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.InvoiceNo == invoiceNo);
            }
            if (caseNo != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.CaseNo == caseNo);
            }
            if (technicalCodeOder != "")
            {
                predicateTPackingList = predicateTPackingList.And(c => c.TechnicalCodeOder == technicalCodeOder);
            }

            if (isActive != -1)
            {
                predicateTPackingList = predicateTPackingList.And(c => c.IsActive == isActive);
            }

            packingListAndOrders = (from pl in tPackingListRep.FindBy(predicateTPackingList, false).Item1.ToList()
                                    from or in tOrderRep.GetAll(false).Item1.Where(o => o.OrderId == pl.OrderId)
                                    from c1 in tCatRep.GetAll(false).Item1.Where(o => o.TechnicalCode == pl.TechnicalCode)
                                    from c2 in tCatRep.GetAll(false).Item1.Where(o => o.TechnicalCode == or.TechnicalCode)
                                    from id in tInventoryDocDetailRep.GetAll(false).Item1.Where(o => o.PackingListId == pl.PackingListId)
                                    from ih in tInventoryDocHeaderRep.GetAll(false).Item1.Where(o => o.InventoryDocHeaderId == id.InventoryDocHeaderId)

                                    select new PackingListAndOrder()
                                    {
                                        OrderId = or.OrderId,
                                        OrderTechnicalCode = or.TechnicalCode,
                                        OrderItemName = c2.ItemName,
                                        OrderQuantity = or.Quantity,
                                        OrderPrice = or.Price,
                                        OrderPriceAED = or.PriceAED,
                                        OrderTariff = or.Tariff,
                                        OrderNetWeight = or.NetWeight,
                                        OrderGrossWeight = or.GrossWeight,
                                        OrderNo = or.OrderNo,
                                        OrderEntryDataDate = or.EntryDataDate,
                                        RecieveOrderQuantity = or.RecieveOrderQuantity,

                                        PackingListId = pl.PackingListId,
                                        PackingListNo = pl.PackingListNo,
                                        InvoiceNo = pl.InvoiceNo,
                                        CaseNo = pl.CaseNo,
                                        CountryOfOrigin = pl.CountryOfOrigin,
                                        PackingListTechnicalCode = pl.TechnicalCode,
                                        PackingListItemName = c1.ItemName,
                                        ShipQty = pl.ShipQty,
                                        ConfirmShipQty = pl.ConfirmShipQty,
                                        HSCode = pl.HSCode,
                                        PackingListUnitPrice = pl.UnitPrice,
                                        PackingListNetWeight = pl.NetWeight,
                                        PackingListGrossWeight = pl.GrossWeight,
                                        PackingListEntryDataDate = pl.EntryDataDate,
                                        PartName = c1.ItemName,
                                        NetWeight = pl.NetWeight,
                                        GrossWeight = pl.GrossWeight,
                                        IsSelected = true,

                                        InventoryQty = id.Qty,
                                        InventoryDate = ih.EntryDataDate,
                                        InventoryTime = ih.EntryDataTime,
                                        InventoryPersonalId = ih.EntryDataPersonalId,
                                        InventoryDocNo = ih.DocNo
                                    }).ToList();

            return packingListAndOrders;
        }
        /// <summary>
        /// FetchTItemGroup
        /// </summary>
        /// <returns></returns>
        public static IList<ItemGroup> FetchTItemGroup()
        {
            IList<ItemGroup> itemGroups = null;
            IGenericRepository<TItemGroup, ItemGroup> itemGroupRep = new TItemGroupRepository();

            itemGroups = itemGroupRep.GetAll(true).Item2.ToList();
            return itemGroups;
        }
        /// <summary>
        /// FetchTItemType
        /// </summary>
        /// <returns></returns>
        public static IList<ItemType> FetchTItemType()
        {
            IList<ItemType> itemTypes = null;
            IGenericRepository<TItemType, ItemType> itemTypeRep = new TItemTypeRepository();

            itemTypes = itemTypeRep.GetAll(true).Item2.ToList();
            return itemTypes;
        }
        /// <summary>
        /// FetchTBrand
        /// </summary>
        /// <returns></returns>
        public static IList<Brand> FetchTBrand()
        {
            IList<Brand> brands = null;
            IGenericRepository<TBrand, Brand> brandRep = new TBrandRepository();

            brands = brandRep.GetAll(true).Item2.ToList();
            return brands;
        }
        /// <summary>
        /// FetchTCarType
        /// </summary>
        /// <returns></returns>
        public static IList<CarType> FetchTCarType(int brandId = 0)
        {
            IList<CarType> carTypes = null;
            IGenericRepository<TCarType, CarType> carTypeRep = new TCarTypeRepository();
            var predicateTCarType = PredicateBuilder.True<TCarType>();
            if (brandId != 0)
            {
                predicateTCarType = predicateTCarType.And(c => c.BrandId == brandId);
            }
            carTypes = carTypeRep.FindBy(predicateTCarType, true).Item2.ToList();

            return carTypes;
        }
        /// <summary>
        /// FetchTCatCarType
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <param name="carTypeId"></param>
        /// <returns></returns>
        public static IList<CatCarType> FetchTCatCarType(string technicalCode, int carTypeId = 0)
        {
            IList<CatCarType> catCarTypes = null;
            IGenericRepository<VCatCarType, VCatCarTypeClass> vCatCarTypeRep = new VCatCarTypeRepository();
            var predicateTCatCarType = PredicateBuilder.True<VCatCarType>();
            var predicateTCat = PredicateBuilder.True<TCat>();
            if (carTypeId != 0)
            {
                predicateTCatCarType = predicateTCatCarType.And(c => c.CarTypeId == carTypeId);
            }

            if (technicalCode != "")
            {
                predicateTCatCarType = predicateTCatCarType.And(c => c.TechnicalCode == technicalCode);
            }
            catCarTypes = (from vcct in vCatCarTypeRep.FindBy(predicateTCatCarType, true).Item2.Where(o => o.TechnicalCode == technicalCode).ToList()

                           select new CatCarType()
                           {
                               ItemName = vcct.ItemName,
                               PersianName = vcct.PersianName,
                               TechnicalCode = vcct.TechnicalCode,
                               CarTypeDesc = vcct.CarTypeDesc,
                               CatCarTypeId = vcct.CatCarTypeId ?? 0,
                               Coefficient = vcct.Coefficient ?? 0,
                               IsActive = vcct.CatCarTypeIsActive ?? 0,

                               CarTypeId = vcct.CarTypeId
                           }).ToList();

            return catCarTypes;
        }
        /// <summary>
        /// FetchTItemGroup
        /// </summary>
        /// <param name="itemGroupId"></param>
        /// <returns></returns>
        public static IList<ItemGroup> FetchTItemGroup(int itemGroupId = 0)
        {
            IList<ItemGroup> itemGroups = null;
            IGenericRepository<TItemGroup, ItemGroup> itemGroupRep = new TItemGroupRepository();
            var predicateTItemGroup = PredicateBuilder.True<TItemGroup>();
            if (itemGroupId != 0)
            {
                predicateTItemGroup = predicateTItemGroup.And(c => c.ItemGroupId == itemGroupId);
            }
            predicateTItemGroup = predicateTItemGroup.And(c => c.IsActive == 1);

            itemGroups = itemGroupRep.FindBy(predicateTItemGroup, true).Item2.ToList();


            return itemGroups;
        }
        /// <summary>
        /// FetchTItemType
        /// </summary>
        /// <param name="itemTypeId"></param>
        /// <returns></returns>
        public static IList<ItemType> FetchTItemType(int itemTypeId = 0)
        {
            IList<ItemType> itemTypes = null;
            IGenericRepository<TItemType, ItemType> itemTypeRep = new TItemTypeRepository();
            var predicateTItemType = PredicateBuilder.True<TItemType>();
            if (itemTypeId != 0)
            {
                predicateTItemType = predicateTItemType.And(c => c.ItemTypeId == itemTypeId);
            }
            predicateTItemType = predicateTItemType.And(c => c.IsActive == 1);

            itemTypes = itemTypeRep.FindBy(predicateTItemType, true).Item2.ToList();


            return itemTypes;
        }
        /// <summary>
        /// FetchTGoodsGrade
        /// </summary>
        /// <param name="goodsGradeId"></param>
        /// <returns></returns>
        public static IList<GoodsGrade> FetchTGoodsGrade(int goodsGradeId = 0)
        {
            IList<GoodsGrade> goodsGrades = null;
            IGenericRepository<TGoodsGrade, GoodsGrade> goodsGradeRep = new TGoodsGradeRepository();
            var predicateTGoodsGrade = PredicateBuilder.True<TGoodsGrade>();
            if (goodsGradeId != 0)
            {
                predicateTGoodsGrade = predicateTGoodsGrade.And(c => c.GoodsGradeId == goodsGradeId);
            }
            predicateTGoodsGrade = predicateTGoodsGrade.And(c => c.IsActive == 1);

            goodsGrades = goodsGradeRep.FindBy(predicateTGoodsGrade, true).Item2.ToList();


            return goodsGrades;
        }

        /// <summary>
        /// FetchTBaseInfint
        /// </summary>
        /// <param name="goodsGradeId"></param>
        /// <returns></returns>
        public static IList<BaseInf> FetchTBaseInfint(int baseInfTypeId, int baseInfId)
        {
            IList<BaseInf> baseInf = null;
            IGenericRepository<TBaseInf, BaseInf> tBaseInfRep = new TBaseInfRepository();
            var predicateTBaseInf = PredicateBuilder.True<TBaseInf>();
            if (baseInfTypeId != 0)
            {
                predicateTBaseInf = predicateTBaseInf.And(c => c.BaseInfTypeId == baseInfTypeId);
            }

            predicateTBaseInf = predicateTBaseInf.And(c => c.IsActive == 1);

            baseInf = tBaseInfRep.FindBy(predicateTBaseInf, true).Item2.ToList();


            return baseInf;
        }

        /// <summary>
        /// FetchTBaseInfint
        /// </summary>
        /// <param name="goodsGradeId"></param>
        /// <returns></returns>
        public static IList<UsageType> FetchTUsageType(int usageTypeId)
        {
            IList<UsageType> usageType = null;
            IGenericRepository<TUsageType, UsageType> tUsageTypeRep = new TUsageTypeRepository();
            var predicateTUsageType = PredicateBuilder.True<TUsageType>();
            if (usageTypeId != 0)
            {
                predicateTUsageType = predicateTUsageType.And(c => c.UsageTypeId == usageTypeId);
            }

            predicateTUsageType = predicateTUsageType.And(c => c.IsActive == 1);

            usageType = tUsageTypeRep.FindBy(predicateTUsageType, true).Item2.ToList();


            return usageType;
        }
        /// <summary>
        /// SaveTCat
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool SaveTCat(Cat cat, string curUser)
        {

            TCat tCat;
            string technicalCode;
            IGenericRepository<TCat, Cat> tCatRep = new TCatRepository();
            var predicateTCat = PredicateBuilder.True<TCat>();
            technicalCode = cat.TechnicalCode;

            predicateTCat = predicateTCat.And(c => c.TechnicalCode == technicalCode);
            tCat = tCatRep.FindBy(predicateTCat, false).Item1.FirstOrDefault();

            if (tCat != null)
            {
                tCat.PNC = cat.PNC;
                tCat.ItemName = cat.ItemName;
                tCat.PersianName = cat.PersianName;
                tCat.Length = cat.Length;
                tCat.Width = cat.Width;
                tCat.Height = cat.Height;
                tCat.NetWeight = cat.NetWeight;
                tCat.ItemGroupId = cat.ItemGroupId;
                tCat.ItemTypeId = cat.ItemTypeId;
                tCat.GoodsGradeId = cat.GoodsGradeId;
                tCat.TrackingTypeId = cat.TrackingTypeId;
                tCat.LabelingTypeId = cat.LabelingTypeId;
                tCat.UsageTypeId = cat.UsageTypeId;

                tCatRep.Update(tCat);

            }
            else
            {
                tCat = tCatRep.Cast<TCat>(cat, 1);
                tCatRep.Add(tCat);
            }

            tCatRep.Save();
            return true;
        }
        /// <summary>
        /// FetchVCustomer
        /// </summary>
        /// <param name="CustomerTypeId"></param>
        /// <param name="CustomerContractTypeId"></param>
        /// <returns></returns>
        public static IList<VCustomerClass> FetchVCustomer(int CustomerTypeId, int CustomerContractTypeId)
        {
            IList<VCustomerClass> vCustomerClasses = null;

            IGenericRepository<VCustomer, VCustomerClass> vCustomerRep = new VCustomerRepository();
            var predicateVCustomer = PredicateBuilder.True<VCustomer>();

            if (CustomerTypeId != 0)
            {
                predicateVCustomer = predicateVCustomer.And(c => c.CustomerTypeId == CustomerTypeId);
            }

            if (CustomerContractTypeId != 0)
            {
                predicateVCustomer = predicateVCustomer.And(c => c.CustomerContractTypeId == CustomerContractTypeId);
            }

            predicateVCustomer = predicateVCustomer.And(c => c.IsActive == 1);
            vCustomerClasses = vCustomerRep.FindBy(predicateVCustomer, true).Item2.ToList();

            return vCustomerClasses;
        }
        /// <summary>
        /// FetchTCustomer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="CustomerTypeId"></param>
        /// <param name="CustomerContractTypeId"></param>
        /// <returns></returns>
        public static IList<Customer> FetchTCustomer(int customerId, int CustomerTypeId, int CustomerContractTypeId)
        {

            IList<Customer> customers = null;
            IGenericRepository<TCustomer, Customer> tCustomerRep = new TCustomerRepository();
            var predicateTCustomer = PredicateBuilder.True<TCustomer>();

            if (customerId != 0)
            {
                predicateTCustomer = predicateTCustomer.And(c => c.CustomerId == customerId);
            }

            if (CustomerTypeId != 0)
            {
                predicateTCustomer = predicateTCustomer.And(c => c.CustomerTypeId == CustomerTypeId);
            }

            if (CustomerContractTypeId != 0)
            {
                predicateTCustomer = predicateTCustomer.And(c => c.CustomerContractTypeId == CustomerContractTypeId);
            }

            customers = tCustomerRep.FindBy(predicateTCustomer, true).Item2.ToList();
            return customers;
        }

        /// <summary>
        /// SaveTCustomer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public static bool SaveTCustomer(Customer customer, string curUser)
        {

            TCustomer tCustomer;
            int customerId;
            IGenericRepository<TCustomer, Customer> tCustomerRep = new TCustomerRepository();

            var predicateTCustomer = PredicateBuilder.True<TCustomer>();
            customerId = customer.CustomerId;
            if (customerId != 0)
            {
                predicateTCustomer = predicateTCustomer.And(c => c.CustomerId == customerId);
                tCustomer = tCustomerRep.FindBy(predicateTCustomer, false).Item1.FirstOrDefault();

                if (tCustomer != null)
                {
                    tCustomer.CustomerTypeId = customer.CustomerTypeId;
                    tCustomer.CustomerContractTypeId = customer.CustomerContractTypeId;
                    tCustomer.CustomerName = customer.CustomerName;
                    tCustomer.Tel = customer.Tel;
                    tCustomer.MobileNo = customer.MobileNo;
                    tCustomer.Email = customer.Email;
                    tCustomer.Address = customer.Address;
                    tCustomer.ManagerName = customer.ManagerName;
                    tCustomer.IsActive = 1;

                    tCustomerRep.Update(tCustomer);

                }
            }
            else
            {
                tCustomer = tCustomerRep.Cast<TCustomer>(customer, 1);
                tCustomer.IsActive = 1;
                tCustomerRep.Add(tCustomer);

            }

            tCustomerRep.Save();
            return true;
        }
        /// <summary>
        /// FetchVGoodsPriceArc
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <returns></returns>
        public static IList<VGoodsPriceArcClass> FetchVGoodsPriceArc(string technicalCode)
        {
            IList<VGoodsPriceArcClass> vGoodsPriceArcs = null;
            IGenericRepository<VGoodsPriceArc, VGoodsPriceArcClass> vGoodsPriceArcRep = new VGoodsPriceArcRepository();

            var predicateVGoodsPriceArc = PredicateBuilder.True<VGoodsPriceArc>();

            if(technicalCode!= "")
            {
                predicateVGoodsPriceArc = predicateVGoodsPriceArc.And(c => c.TechnicalCode == technicalCode);
            }

            vGoodsPriceArcs = vGoodsPriceArcRep.FindBy(predicateVGoodsPriceArc, true).Item2.ToList();

            return vGoodsPriceArcs;
        }

        /// <summary>
        /// FetchGoodsPrice
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <returns></returns>
        public static IList<GoodsPrice> FetchGoodsPrice(string technicalCode)
        {
            IList<GoodsPrice> goodsPrices = null;
            IGenericRepository<TGoodsPrice, GoodsPrice>  tGoodsPriceRep = new TGoodsPriceRepository();

            var predicateTGoodsPrice = PredicateBuilder.True<TGoodsPrice>();

            if (technicalCode != "")
            {
                predicateTGoodsPrice = predicateTGoodsPrice.And(c => c.TechnicalCode == technicalCode);
            }

            goodsPrices = tGoodsPriceRep.FindBy(predicateTGoodsPrice, true).Item2.ToList();

            return goodsPrices;
        }

        public static bool SaveTGoodsPrice(GoodsPrice goodsPrice, string curUser, string curDate, string curTime)
        {
            TGoodsPrice tGoodsPrice;
            TGoodsPriceArc tGoodsPriceArc;

            int goodsPriceId;
            IGenericRepository<TGoodsPrice, GoodsPrice> tGoodsPriceRep = new TGoodsPriceRepository();
            IGenericRepository<TGoodsPriceArc, GoodsPriceArc> tGoodsPriceArcRep = new TGoodsPriceArcRepository();

            var predicateTGoodsPrice = PredicateBuilder.True<TGoodsPrice>();
            goodsPriceId = goodsPrice.GoodsPriceId;
            if (goodsPriceId != 0)
            {
                predicateTGoodsPrice = predicateTGoodsPrice.And(c => c.GoodsPriceId == goodsPriceId);
                tGoodsPrice = tGoodsPriceRep.FindBy(predicateTGoodsPrice, false).Item1.FirstOrDefault();

                if (tGoodsPrice != null)
                {
                    tGoodsPriceArc = new TGoodsPriceArc();
                    tGoodsPriceArc.GoodsPriceId = tGoodsPrice.GoodsPriceId;
                    tGoodsPriceArc.TechnicalCode = tGoodsPrice.TechnicalCode;
                    tGoodsPriceArc.CurrencyTypeId = tGoodsPrice.CurrencyTypeId;
                    tGoodsPriceArc.DollarPrice = tGoodsPrice.DollarPrice;
                    tGoodsPriceArc.BasePrice = tGoodsPrice.BasePrice;
                    tGoodsPriceArc.PriceInRials = tGoodsPrice.PriceInRials;
                    tGoodsPriceArc.EntryDataDate = tGoodsPrice.EntryDataDate;
                    tGoodsPriceArc.EntryDataTime = tGoodsPrice.EntryDataTime;
                    tGoodsPriceArc.EntryPersonalId = tGoodsPrice.EntryPersonalId;
                    tGoodsPriceArc.UpdateDate = curDate;
                    tGoodsPriceArc.UpdateTime = curTime;
                    tGoodsPriceArc.UpdatePersonalId = curUser;

                    tGoodsPriceArcRep.Add(tGoodsPriceArc);
                    //  Update TGoodsPrice
                    tGoodsPrice.CurrencyTypeId = goodsPrice.CurrencyTypeId;
                    tGoodsPrice.DollarPrice = goodsPrice.DollarPrice;
                    tGoodsPrice.BasePrice = goodsPrice.BasePrice;
                    tGoodsPrice.PriceInRials = goodsPrice.PriceInRials;
                    tGoodsPrice.EntryDataDate = curDate;

                    tGoodsPrice.EntryDataTime = curTime;
                    tGoodsPrice.EntryPersonalId = curUser;

                    tGoodsPriceRep.Update(tGoodsPrice);

                }
            }
            else
            {
                tGoodsPrice = tGoodsPriceRep.Cast<TGoodsPrice>(goodsPrice, 1);
                tGoodsPrice.EntryDataDate = curDate;
                tGoodsPrice.EntryDataTime = curTime;
                tGoodsPrice.EntryPersonalId = curUser;

                tGoodsPriceRep.Add(tGoodsPrice);
            }

            tGoodsPriceArcRep.Save();
            tGoodsPriceRep.Save();            

            return true;
        }
        /// <summary>
        ///     FetchTechnicalCode
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="technicalCode"></param>
        /// <param name="fromRequestDate"></param>
        /// <param name="toRequestDate"></param>
        /// <param name="RequestType"></param>
        /// <returns></returns>
        public static IList<VRequestClass> FetchVRequest(int customerId, string technicalCode, string requestNo,string fromRequestDate, string toRequestDate, int requestTypeId,  short RequestHeaderIsActive, short RequestDetailIsActive)
        {
            IList<VRequestClass> vRequestClasses = null;
            IGenericRepository<VRequest, VRequestClass> vRequestRep = new VRequestRepository();

            var predicateVRequest = PredicateBuilder.True<VRequest>();
            if(customerId !=0)
            {
                predicateVRequest = predicateVRequest.And(c => c.CustomerId == customerId);
            }
            if (technicalCode != "")
            {
                predicateVRequest = predicateVRequest.And(c => c.TechnicalCode == technicalCode);
            }
            if (requestNo != "")
            {
                predicateVRequest = predicateVRequest.And(c => c.RequestNo == requestNo);
            }

            if (fromRequestDate != "")
            {
                predicateVRequest = predicateVRequest.And(c => c.RequestDate.CompareTo(fromRequestDate) >= 0 );
            }

            if (toRequestDate != "")
            {
                predicateVRequest = predicateVRequest.And(c => c.RequestDate.CompareTo(toRequestDate) <= 0);
            }
            if (requestTypeId != 0)
            {
                predicateVRequest = predicateVRequest.And(c => c.RequestTypeId == requestTypeId);
            }

            //if (RequestDetailIsActive != 0)
            //{
            //    predicateVRequest = predicateVRequest.And(c => c.RequestDetailIsActive == RequestDetailIsActive);
            //}

            if (RequestDetailIsActive != 0)
            {
                predicateVRequest = predicateVRequest.And(c => c.RequestDetailIsActive == RequestDetailIsActive);
            }

            
            vRequestClasses = vRequestRep.FindBy(predicateVRequest, true).Item2.ToList();

            return vRequestClasses;
        }
        /// <summary>
        /// SaveRequest
        /// </summary>
        /// <param name="vRequestClass"></param>
        /// <param name="curUser"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public static string SaveRequest(VRequestClass vRequestClass, string curUser, string curDate, string curTime)
        {
            string requestNo = "";
            IGenericRepository<TRequestHeader, RequestHeader> tRequestHeaderRep = new TRequestHeaderRepository();
            IGenericRepository<TRequestDetial, RequestDetial> tRequestDetailRep = new TRequestDetialRepository();

            if (vRequestClass.RequestNo == "" || vRequestClass.RequestNo == null)
            {
                TRequestHeader requestHeader = new TRequestHeader();
                requestNo = MakeDocNo(System.Convert.ToInt32(curDate.Substring(0, 4)), 100);

                requestHeader.RequestNo = requestNo;
                requestHeader.CustomerId = vRequestClass.KeepCustomerId;
                requestHeader.RequestDate = curDate;
                requestHeader.RequestTime = curTime;
                requestHeader.RequestTitle = vRequestClass.RequestTitle;
                requestHeader.EntryPersonalId = curUser;
                requestHeader.IsActive = 24;

                tRequestHeaderRep.Add(requestHeader);
            }
            else
            {
                requestNo = vRequestClass.RequestNo;

            }

            if (vRequestClass.FlagUpdate == 1)
            {
                var qDetail = tRequestDetailRep.FindBy(o => o.RequestNo == requestNo && o.TechnicalCode == vRequestClass.TechnicalCode, false).Item1.FirstOrDefault();

                qDetail.Qty = vRequestClass.Qty;
                qDetail.RequestTypeId = vRequestClass.RequestTypeId;

                tRequestDetailRep.Update(qDetail, "Qty,RequestTypeId");
            }
            else
            {
                TRequestDetial requestDetial = new TRequestDetial();

                requestDetial.TechnicalCode = vRequestClass.TechnicalCode;
                requestDetial.Qty = vRequestClass.Qty;
                requestDetial.IsActive = 24;
                requestDetial.RequestTypeId = vRequestClass.RequestTypeId;
                requestDetial.RequestNo = requestNo;

                tRequestDetailRep.Add(requestDetial);
            }

            if (vRequestClass.RequestNo == "" || vRequestClass.RequestNo == null )
            {
                tRequestHeaderRep.Save();
            }
            tRequestDetailRep.Save();
            return requestNo;
        }
        /// <summary>
        /// FetchTRequestHeader
        /// </summary>
        /// <param name="RequestNo"></param>
        /// <param name="FromRequestDate"></param>
        /// <param name="ToRequestDate"></param>
        /// <param name="CustomerId"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public static IList<RequestHeader> FetchTRequestHeader(string RequestNo, string FromRequestDate, string ToRequestDate, int CustomerId, short IsActive)
        {
            IGenericRepository<TRequestHeader, RequestHeader> tRequestHeaderRep = new TRequestHeaderRepository();
            IList<RequestHeader> requestHeaders = null;
            var predicateTRequestHeader = PredicateBuilder.True<TRequestHeader>();
            if (RequestNo.Trim() != "") {
                predicateTRequestHeader = predicateTRequestHeader.And(c => c.RequestNo == RequestNo);
            }

            if (FromRequestDate.Trim() != "")
            {
                predicateTRequestHeader = predicateTRequestHeader.And(c => c.RequestDate.CompareTo(FromRequestDate) >= 0);
            }
            if (ToRequestDate.Trim() != "")
            {
                predicateTRequestHeader = predicateTRequestHeader.And(c => c.RequestDate.CompareTo(ToRequestDate) <= 0);
            }

            if (CustomerId != 0 && CustomerId != null)
            {
                predicateTRequestHeader = predicateTRequestHeader.And(c => c.CustomerId == CustomerId);
            }

            if (IsActive != 0)
            {
                predicateTRequestHeader = predicateTRequestHeader.And(c => c.IsActive == IsActive);
            }


            requestHeaders = tRequestHeaderRep.FindBy(predicateTRequestHeader, true).Item2.ToList();

            return requestHeaders;
        }
        /// <summary>
        /// SaveInvoice
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <param name="invoiceQty"></param>
        /// <param name="requestId"></param>
        /// <param name="requestDetialId"></param>
        /// <param name="requestNo"></param>
        /// <param name="customerId"></param>
        /// <param name="invoiceTitle"></param>
        /// <param name="curUser"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public static string SaveInvoice(string technicalCode, string invoiceQty, string requestId, string requestDetialId, string requestNo,int customerId, string invoiceTitle, string curUser, string curDate, string curTime)
        {
            string invoiceNo, curRequestNo;
            int ind;
            string[] arrTechnicalCode, arrInvoiceQty, arrRequestId, arrRequestDetialId, arrRequestNo;
            int iRequestDetialId, iRequestId;
            double dInvoiceQty;
            TInvoiceHeader tInvoiceHeader;
            TInvoiceDetail tInvoiceDetail;
            TRequestHeader tRequestHeader;
            TRequestDetial tRequestDetial;
            IGenericRepository<TInvoiceHeader, InvoiceHeader> tInvoiceHeaderRep = new TInvoiceHeaderRepository();
            IGenericRepository<TInvoiceDetail, InvoiceDetail> tInvoiceDetailRep = new TInvoiceDetailRepository();

            IGenericRepository<TRequestHeader, RequestHeader> tRequestHeaderRep = new TRequestHeaderRepository();
            IGenericRepository<TRequestDetial, RequestDetial> tRequestDetialRep = new TRequestDetialRepository();

            invoiceNo = MakeDocNo(System.Convert.ToInt32(curDate.Substring(0, 4)), 101);
            arrTechnicalCode = technicalCode.Split(',');
            arrInvoiceQty = invoiceQty.Split(',');
            arrRequestId = requestId.Split(',');
            arrRequestDetialId = requestDetialId.Split(',');
            arrRequestNo = requestNo.Split(',');

            for(ind = 0; ind < arrTechnicalCode.Length; ind++)
            {
                if (ind == 0)
                {
                    tInvoiceHeader = new TInvoiceHeader();

                    tInvoiceHeader.CustomerId = customerId;
                    tInvoiceHeader.InvoiceNo = invoiceNo;
                    tInvoiceHeader.InvoiceTitle = invoiceTitle;
                    tInvoiceHeader.InvoiceDate = curDate;
                    tInvoiceHeader.InvoiceTime = curTime;
                    tInvoiceHeader.EntryPersonalId = curUser;
                    tInvoiceHeader.IsActive = 24;
                    tInvoiceHeader.RequestId = System.Convert.ToInt32(arrRequestId[ind]);
                    tInvoiceHeader.RequestNo = requestNo;

                    tInvoiceHeaderRep.Add(tInvoiceHeader);
                }
                //  TInvoiceDetail
                iRequestDetialId = System.Convert.ToInt32(arrRequestDetialId[ind]);
                dInvoiceQty = System.Convert.ToDouble(arrInvoiceQty[ind]);
                tInvoiceDetail = new TInvoiceDetail();
                tInvoiceDetail.TechnicalCode = arrTechnicalCode[ind] ;
                tInvoiceDetail.Qty = dInvoiceQty;
                tInvoiceDetail.InvoiceNo = invoiceNo;
                tInvoiceDetail.IsActive = 24;
                tInvoiceDetail.RequestDetialId = iRequestDetialId;

                tInvoiceDetailRep.Add(tInvoiceDetail);

                //  TRequestDetial
                tRequestDetial = tRequestDetialRep.FindBy(c => c.RequestDetialId == iRequestDetialId, false).Item1.FirstOrDefault();
                if(tRequestDetial != null)
                {
                    tRequestDetial.InvoiceQty += dInvoiceQty;
                    tRequestDetial.IsActive = (short)(tRequestDetial.InvoiceQty == tRequestDetial.Qty ? 25 : 24);
                    tRequestDetialRep.Update(tRequestDetial);
                }
            }

            tInvoiceHeaderRep.Save();
            tInvoiceDetailRep.Save();
            tRequestDetialRep.Save();

            //  TRequestHeader
            iRequestId = System.Convert.ToInt32(arrRequestId[0]);
            curRequestNo = arrRequestNo[0];
            tRequestDetial = tRequestDetialRep.FindBy(c => c.RequestNo == curRequestNo && c.IsActive != 25, false).Item1.FirstOrDefault();
            if (tRequestDetial == null) {
                tRequestHeader = tRequestHeaderRep.FindBy(c => c.RequestNo == curRequestNo, false).Item1.FirstOrDefault();
                if(tRequestHeader != null)
                {
                    tRequestHeader.IsActive = 25;
                    tRequestHeaderRep.Save();
                }
            }

            return invoiceNo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestNo"></param>
        /// <param name="CustomerId"></param>
        /// <param name="InvoiceDetailIsActive"></param>
        /// <param name="InvoiceHeaderIsActive"></param>
        /// <returns></returns>
        public static IList<VInvoiceClass> FetchV_Invoice(string RequestNo, string InvoiceNo, int CustomerId, short InvoiceDetailIsActive, short InvoiceHeaderIsActive)
        {
            IGenericRepository<V_Invoice, VInvoiceClass> vInvoiceRep = new V_InvoiceRepository();
            IList<VInvoiceClass> vInvoiceClasses = null;
            var predicateVInvoice = PredicateBuilder.True<V_Invoice>();
            if (RequestNo.Trim() != "")
            {
                predicateVInvoice = predicateVInvoice.And(c => c.RequestNo == RequestNo);
            }

            if (InvoiceNo.Trim() != "")
            {
                predicateVInvoice = predicateVInvoice.And(c => c.InvoiceNo == InvoiceNo);
            }

            
            //if (FromRequestDate.Trim() != "")
            //{
            //    predicateTRequestHeader = predicateTRequestHeader.And(c => c.RequestDate.CompareTo(FromRequestDate) >= 0);
            //}
            //if (ToRequestDate.Trim() != "")
            //{
            //    predicateTRequestHeader = predicateTRequestHeader.And(c => c.RequestDate.CompareTo(ToRequestDate) <= 0);
            //}

            if (CustomerId != 0 && CustomerId != null)
            {
                predicateVInvoice = predicateVInvoice.And(c => c.CustomerId == CustomerId);
            }

            if (InvoiceDetailIsActive != 0)
            {
                predicateVInvoice = predicateVInvoice.And(c => c.InvoiceDetailIsActive == InvoiceDetailIsActive);
            }

            if (InvoiceHeaderIsActive != 0)
            {
                predicateVInvoice = predicateVInvoice.And(c => c.InvoiceHeaderIsActive == InvoiceHeaderIsActive);
            }

            vInvoiceClasses = vInvoiceRep.FindBy(predicateVInvoice, true).Item2.ToList();

            return vInvoiceClasses;
        }

        public static bool ConfirmInvoice(string invoiceDetailId, string curDate, string curTime, string curUser)
        {
            string[] arrInvoiceDetailId;
            int[] iArrInvoiceDetailId;
            int ind;
            IGenericRepository<TWareHouseStock, WareHouseStock> tWareHouseStockRep = new TWareHouseStockRepository();
            TWareHouseStock tWareHouseStock;
            string curTechnicalCode = "";
            double curQty;

            IGenericRepository<TInvoiceDetail, InvoiceDetail> tInvoiceDetailRep = new TInvoiceDetailRepository();
            IGenericRepository<TGoodsPrice, GoodsPrice> tGoodsPriceRep = new TGoodsPriceRepository();
            TGoodsPrice goodsPrice;
            IList<TInvoiceDetail> invoiceDetails = null;
            arrInvoiceDetailId = invoiceDetailId.Split(',');
            iArrInvoiceDetailId = new int[arrInvoiceDetailId.Length];
            double priceInRials;
            for (ind = 0;ind < arrInvoiceDetailId.Length; ind++)
            {
                iArrInvoiceDetailId[ind] = System.Convert.ToInt32(arrInvoiceDetailId[ind]);
            }
            invoiceDetails = tInvoiceDetailRep.FindBy(c => iArrInvoiceDetailId.Contains(c.InvoiceDetailId), false).Item1.ToList();
            foreach(var id in invoiceDetails)
            {
                curTechnicalCode = id.TechnicalCode;

                goodsPrice = tGoodsPriceRep.FindBy(c => c.TechnicalCode == curTechnicalCode, false).Item1.FirstOrDefault();
                if(goodsPrice != null)
                {
                    priceInRials = goodsPrice.PriceInRials;
                }
                else
                {
                    priceInRials = 0;
                }
                id.IsActive = 31;
                id.ConfirmDate = curDate;
                id.ConfirmTime = curTime;
                id.ConfirmPersonalId = curUser;
                id.PriceInRials = priceInRials;
                tInvoiceDetailRep.Update(id);

                
                curQty = id.Qty;
                tWareHouseStock = tWareHouseStockRep.FindBy(c => c.TechnicalCode == curTechnicalCode, false).Item1.FirstOrDefault();
                if (tWareHouseStock != null)
                {
                    tWareHouseStock.ReserveQty += curQty;
                }
                else
                {
                    tWareHouseStock = new TWareHouseStock();
                    tWareHouseStock.TechnicalCode = curTechnicalCode;
                    tWareHouseStock.ReserveQty = curQty;

                    tWareHouseStockRep.Add(tWareHouseStock);
                }


            }

            tInvoiceDetailRep.Save();
            tWareHouseStockRep.Save();
            return true;
        }

        public static bool OLD_SaveFactor(string invoiceDetailId, string technicalCode, string qty, string price, int customerId, string curDate, string curTime, string curUser)
        {

            //string[] arrInvoiceDetailId, arrTechnicalCode, arrQty, arrPrice;
            //int[] iArrInvoiceDetailId;
            //int ind;
            //string factorNo = "";
            //IGenericRepository<TFactorHeader, FactorHeader> tFactorHeaderRep = new TFactorHeaderRepository();
            //TFactorHeader tFactorHeader;
            //IGenericRepository<TFactorDetail, FactorDetail> tFactorDetailRep = new TFactorDetailRepository();
            //TFactorDetail tFactorDetail;
            //IGenericRepository<TInvoiceDetail, InvoiceDetail> tInvoiceDetailRep = new TInvoiceDetailRepository();
            //TInvoiceDetail tInvoiceDetail;
            //IGenericRepository<TWareHouseStock, WareHouseStock> tWareHouseStockRep = new TWareHouseStockRepository();
            //TWareHouseStock tWareHouseStock;
            //string curTechnicalCode = "";
            //double curQty;

            //IList<TInvoiceDetail> invoiceDetails = null;

            //arrInvoiceDetailId = invoiceDetailId.Split(',');
            //arrTechnicalCode = technicalCode.Split(',');
            //arrQty = qty.Split(',');
            //arrPrice = price.Split(',');

            //iArrInvoiceDetailId = new int[arrInvoiceDetailId.Length];
            //for (ind = 0; ind < arrInvoiceDetailId.Length; ind++)
            //{
            //    iArrInvoiceDetailId[ind] = System.Convert.ToInt32(arrInvoiceDetailId[ind]);
            //}
            //for (ind = 0; ind < iArrInvoiceDetailId.Length; ind++)
            //{
            //    if (ind == 0)
            //    {
            //        tFactorHeader = new TFactorHeader();
            //        factorNo = MakeDocNo(System.Convert.ToInt32(curDate.Substring(0, 4)), 102);

            //        tFactorHeader.CustomerId = customerId;
            //        tFactorHeader.FactorNo = factorNo;
            //        tFactorHeader.FactorDate = curDate;
            //        tFactorHeader.FactorTime = curTime;
            //        tFactorHeader.EntryPersonalId = curUser;
            //        tFactorHeader.IsActive = 24;
            //        tFactorHeaderRep.Add(tFactorHeader);
            //    }

            //    tFactorDetail = new TFactorDetail();
            //    tFactorDetail.FactorNo = factorNo;
            //    tFactorDetail.TechnicalCode = arrTechnicalCode[ind];
            //    tFactorDetail.Qty = System.Convert.ToDouble( arrQty[ind]);
            //    tFactorDetail.IsActive = 24;
            //    tFactorDetail.InvoiceDetailId = System.Convert.ToInt32(arrInvoiceDetailId[ind]);
            //    tFactorDetail.Price = System.Convert.ToDouble(arrPrice[ind]);

            //    curTechnicalCode = arrTechnicalCode[ind];
            //    curQty = System.Convert.ToDouble(arrQty[ind]);
            //    tWareHouseStock = tWareHouseStockRep.FindBy(c => c.TechnicalCode == curTechnicalCode, false).Item1.FirstOrDefault();
            //    if (tWareHouseStock != null)
            //    {
            //        tWareHouseStock.ReserveQty -= curQty;
            //    }
            //    //else
            //    //{
            //    //    tWareHouseStock = new TWareHouseStock();
            //    //    tWareHouseStock.TechnicalCode = curTechnicalCode;
            //    //    tWareHouseStock.ReserveQty = curQty;

            //    //    tWareHouseStockRep.Add(tWareHouseStock);
            //    //}
            //}

            //invoiceDetails = tInvoiceDetailRep.FindBy(c => iArrInvoiceDetailId.Contains(c.InvoiceDetailId), false).Item1.ToList();
            //foreach (var id in invoiceDetails)
            //{
            //    id.IsActive = 25;
            //}

            //tInvoiceDetailRep.Save();
            //tFactorHeaderRep.Save();
            //tFactorDetailRep.Save();
            //tWareHouseStockRep.Save();

            return true;
        }
        /// <summary>
        /// ChangeStatus2Print
        /// </summary>
        /// <param name="vInvoiceClasses"></param>
        /// <returns></returns>
        public static bool ChangeStatus2Print(IList<VInvoiceClass> vInvoiceClasses, string curDate, string curTime, string curUser)
        {
            int[] invoiceDetailIds = new int[vInvoiceClasses.Count];
            string[] invoiceNos = new string[vInvoiceClasses.Count];
            IGenericRepository<TInvoiceHeader, InvoiceHeader> tInvoiceHeaderRep = new TInvoiceHeaderRepository();
            TInvoiceHeader tInvoiceHeader;
            IGenericRepository<TInvoiceDetail, InvoiceDetail> tInvoiceDetailRep = new TInvoiceDetailRepository();
            IList<TInvoiceDetail> tInvoiceDetails;


            int ind = 0;
            foreach (var ic in vInvoiceClasses)
            {
                invoiceDetailIds[ind] = ic.InvoiceDetailId;
                invoiceNos[ind] = ic.InvoiceNo;
            }

            tInvoiceDetails = tInvoiceDetailRep.FindBy(c => invoiceDetailIds.Contains(c.InvoiceDetailId), false).Item1.ToList();
            foreach(var id in tInvoiceDetails)
            {
                id.IsActive = 32;
                id.PrintDate = curDate;
                id.PrintTime = curTime;
                id.PrintPersonalId = curUser;

            }
            tInvoiceDetailRep.Save();


            return true;
        }
        /// <summary>
        /// SaveExitFromWarhouse
        /// </summary>
        /// <param name="technicalCode"></param>
        /// <param name="exitQty"></param>
        /// <param name="invoiceId"></param>
        /// <param name="invoiceDetailId"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="customerId"></param>
        /// <param name="exitTitle"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string SaveExitFromWarhouse(string technicalCode, string exitQty, string invoiceId, string invoiceDetailId, string invoiceNo, int customerId, string exitTitle, string curDate, string curTime, string curUser)
        {
            string exitNo = "", curInvoiceNo;
            int ind;
            string[] arrTechnicalCode, arrExitQty, arrInvoiceId, arrInvoiceDetailId, arrInvoiceNo;
            int iInvoiceDetailId, iInvoiceId;
            double dExitQty;
            TExitFromWarehouseHeader tExitFromWarehouseHeader;
            TExitFromWarehouseDetail tExitFromWarehouseDetail;
            TInvoiceHeader tInvoiceHeader;
            TInvoiceDetail tInvoiceDetail;
            IGenericRepository<TInvoiceHeader, InvoiceHeader> tInvoiceHeaderRep = new TInvoiceHeaderRepository();
            IGenericRepository<TInvoiceDetail, InvoiceDetail> tInvoiceDetailRep = new TInvoiceDetailRepository();

            IGenericRepository<TExitFromWarehouseHeader, ExitFromWarehouseHeader> tExitFromWarehouseHeaderRep = new TExitFromWarehouseHeaderRepository();
            IGenericRepository<TExitFromWarehouseDetail, ExitFromWarehouseDetail> tExitFromWarehouseDetailRep = new TExitFromWarehouseDetailRepository();

            exitNo = MakeDocNo(System.Convert.ToInt32(curDate.Substring(0, 4)), 102);
            arrTechnicalCode = technicalCode.Split(',');
            arrExitQty = exitQty.Split(',');
            arrInvoiceId = invoiceId.Split(',');
            arrInvoiceDetailId = invoiceDetailId.Split(',');
            arrInvoiceNo = invoiceNo.Split(',');

            for (ind = 0; ind < arrTechnicalCode.Length; ind++)
            {
                if (ind == 0)
                {
                    tExitFromWarehouseHeader = new TExitFromWarehouseHeader();

                    tExitFromWarehouseHeader.CustomerId = customerId;
                    tExitFromWarehouseHeader.ExitNo = exitNo;
                    tExitFromWarehouseHeader.ExitTitle = exitTitle;
                    tExitFromWarehouseHeader.ExitDate = curDate;
                    tExitFromWarehouseHeader.ExitTime = curTime;
                    tExitFromWarehouseHeader.EntryPersonalId = curUser;
                    tExitFromWarehouseHeader.IsActive = 24;
                    tExitFromWarehouseHeader.InvoiceId = System.Convert.ToInt32(arrInvoiceDetailId[ind]);
                    tExitFromWarehouseHeader.InvoiceNo = invoiceNo;

                    tExitFromWarehouseHeaderRep.Add(tExitFromWarehouseHeader);
                }
                //  TInvoiceDetail
                iInvoiceDetailId = System.Convert.ToInt32(arrInvoiceDetailId[ind]);
                dExitQty = System.Convert.ToDouble(arrExitQty[ind]);

                tExitFromWarehouseDetail = new TExitFromWarehouseDetail();
                tExitFromWarehouseDetail.TechnicalCode = arrTechnicalCode[ind];
                tExitFromWarehouseDetail.Qty = dExitQty;
                tExitFromWarehouseDetail.ExitNo= exitNo;
                tExitFromWarehouseDetail.IsActive = 24;
                tExitFromWarehouseDetail.InvoiceDetailId= iInvoiceDetailId;

                tExitFromWarehouseDetailRep.Add(tExitFromWarehouseDetail);

                //  TRequestDetial
                tInvoiceDetail = tInvoiceDetailRep.FindBy(c => c.InvoiceDetailId == iInvoiceDetailId, false).Item1.FirstOrDefault();
                if (tInvoiceDetail != null)
                {
                    tInvoiceDetail.ExitQty += dExitQty;
                    tInvoiceDetail.IsActive = (short)(tInvoiceDetail.ExitQty == tInvoiceDetail.Qty ? 25 : tInvoiceDetail.IsActive);
                    tInvoiceDetailRep.Update(tInvoiceDetail);
                }
            }

            tExitFromWarehouseDetailRep.Save();
            tExitFromWarehouseHeaderRep.Save();
            tInvoiceDetailRep.Save();

            //  TRequestHeader
            iInvoiceId = System.Convert.ToInt32(arrInvoiceId[0]);
            curInvoiceNo = arrInvoiceNo[0];
            tInvoiceDetail = tInvoiceDetailRep.FindBy(c => c.InvoiceNo == curInvoiceNo && c.IsActive != 25, false).Item1.FirstOrDefault();
            if (tInvoiceDetail == null)
            {
                tInvoiceHeader = tInvoiceHeaderRep.FindBy(c => c.InvoiceNo == curInvoiceNo, false).Item1.FirstOrDefault();
                if (tInvoiceHeader != null)
                {
                    tInvoiceHeader.IsActive = 25;
                    tInvoiceHeaderRep.Save();
                }
            }

            return exitNo;
        }

        /// <summary>
        /// FetchVExitWarehouseClass
        /// </summary>
        /// <param name="exitNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="customerId"></param>
        /// <param name="exitHeaderIsActive"></param>
        /// <param name="exitDetailIsActive"></param>
        /// <returns></returns>
        public static IList<VExitWarehouseClass> FetchVExitWarehouseClass(string exitNo, string invoiceNo, int customerId, short exitHeaderIsActive, short exitDetailIsActive, string factorNo)
        {
            IGenericRepository<V_ExitWarehouse, VExitWarehouseClass> vExitWarehouseClassRep = new V_ExitWarehouseRepository();
            IList<VExitWarehouseClass> vExitWarehouseClasses = null;
            var predicateVExitWarehouse = PredicateBuilder.True<V_ExitWarehouse>();

            if (exitNo.Trim() != "")
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.ExitNo == exitNo);
            }
            if (invoiceNo.Trim() != "")
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.InvoiceNo == invoiceNo);
            }

            if (customerId != 0 && customerId != null)
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.CustomerId == customerId);
            }

            if (exitHeaderIsActive != 0)
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.ExitHeaderIsActive == exitHeaderIsActive);
            }

            if (exitDetailIsActive != 0)
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.ExitDetailIsActive == exitDetailIsActive);
            }

            if (factorNo != "")
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.FactorNo == factorNo);
            }
            vExitWarehouseClasses = vExitWarehouseClassRep.FindBy(predicateVExitWarehouse, true).Item2.ToList();

            return vExitWarehouseClasses;

        }
        /// <summary>
        /// FetchV_ExitWarehouseHeader
        /// </summary>
        /// <param name="exitNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="customerId"></param>
        /// <param name="exitHeaderIsActive"></param>
        /// <param name="exitDetailIsActive"></param>
        /// <returns></returns>
        public static IList<VExitWarehouseHeaderClass> FetchV_ExitWarehouseHeader(string exitNo, string invoiceNo, int customerId, short exitHeaderIsActive, short exitDetailIsActive)
        {
            IGenericRepository<V_ExitWarehouseHeader, VExitWarehouseHeaderClass> vExitWarehouseHeaderClassRep = new V_ExitWarehouseHeaderRepository();
            IList<VExitWarehouseHeaderClass> vExitWarehouseHeaders = null;
            var predicateVExitWarehouse = PredicateBuilder.True<V_ExitWarehouseHeader>();

            if (exitNo.Trim() != "")
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.ExitNo == exitNo);
            }
            if (invoiceNo.Trim() != "")
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.InvoiceNo == invoiceNo);
            }

            if (customerId != 0 && customerId != null)
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.CustomerId == customerId);
            }

            if (exitHeaderIsActive != 0)
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.ExitHeaderIsActive == exitHeaderIsActive);
            }

            if (exitDetailIsActive != 0)
            {
                predicateVExitWarehouse = predicateVExitWarehouse.And(c => c.ExitDetailIsActive == exitDetailIsActive);
            }

            vExitWarehouseHeaders = vExitWarehouseHeaderClassRep.FindBy(predicateVExitWarehouse, true).Item2.ToList();

            return vExitWarehouseHeaders;

        }
        /// <summary>
        /// SaveFactor
        /// </summary>
        /// <param name="invoiceDetailId"></param>
        /// <param name="exitFromWarehouseHeaderId"></param>
        /// <param name="exitFromWarehouseDetailId"></param>
        /// <param name="curDate"></param>
        /// <param name="curTime"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string SaveFactor(string invoiceDetailId, string exitFromWarehouseHeaderId, string exitFromWarehouseDetailId, string curDate, string curTime, string curUser)
        {

            string[] arrInvoiceDetailId, arrExitFromWarehouseHeaderId, arrExitFromWarehouseDetailId;
            int[] iarrInvoiceDetailId, iarrExitFromWarehouseHeaderId, iarrExitFromWarehouseDetailId;
            IGenericRepository<TInvoiceDetail, InvoiceDetail> tInvoiceDetailRep = new TInvoiceDetailRepository();
            var predicateTInvoiceDetail = PredicateBuilder.True<TInvoiceDetail>();
            string curTechnicalNo = "";
            IGenericRepository<TExitFromWarehouseHeader, ExitFromWarehouseHeader> tExitFromWarehouseHeaderRep = new TExitFromWarehouseHeaderRepository();
            var predicateTExitFromWarehouseHeader = PredicateBuilder.True<TExitFromWarehouseHeader>();

            IGenericRepository<TExitFromWarehouseDetail, ExitFromWarehouseDetail> tExitFromWarehouseDetailRep = new TExitFromWarehouseDetailRepository();
            var predicateTExitFromWarehouseDetail = PredicateBuilder.True<TExitFromWarehouseDetail>();

            //IGenericRepository<TFactorHeader, FactorHeader> tFactorHeaderRep = new TFactorHeaderRepository();

            //IGenericRepository<TFactorDetail, FactorDetail> tFactorDetailRep = new TFactorDetailRepository();

            IGenericRepository<TWareHouseStock, WareHouseStock> tWareHouseStockRep = new TWareHouseStockRepository();
            TWareHouseStock wareHouseStock;
            var predicateTWareHouseStock = PredicateBuilder.True<TWareHouseStock>();
            int ind = 0;
            string factorNo;
 
            factorNo = MakeDocNo(System.Convert.ToInt32(curDate.Substring(0, 4)), 103);
            arrExitFromWarehouseDetailId = exitFromWarehouseDetailId.Split(',');
            iarrExitFromWarehouseDetailId = new int[arrExitFromWarehouseDetailId.Length];
            ind = 0;
            for (ind = 0; ind<arrExitFromWarehouseDetailId.Length; ind++)
            {
                
                iarrExitFromWarehouseDetailId[ind] = System.Convert.ToInt32(arrExitFromWarehouseDetailId[ind]);
            }


            //  TExitFromWarehouseHeader
            arrExitFromWarehouseHeaderId = exitFromWarehouseHeaderId.Split(',');
            iarrExitFromWarehouseHeaderId = new int[arrExitFromWarehouseHeaderId.Length];
            ind = 0;
            for (ind = 0; ind<arrExitFromWarehouseHeaderId.Length; ind++)
            {
                iarrExitFromWarehouseHeaderId[ind] = System.Convert.ToInt32(arrExitFromWarehouseHeaderId[ind]);
            }

            IList<TExitFromWarehouseHeader> exitFromWarehouseHeaders = tExitFromWarehouseHeaderRep.FindBy(c => iarrExitFromWarehouseHeaderId.Contains(c.ExitFromWarehouseHeaderId), false).Item1.ToList();
            foreach (var efw in exitFromWarehouseHeaders)
            {
                efw.IsActive = 25;
                tExitFromWarehouseHeaderRep.Update(efw);
            }

            //  TExitFromWarehouseDetail
            arrExitFromWarehouseDetailId = exitFromWarehouseDetailId.Split(',');
            iarrExitFromWarehouseDetailId = new int[arrExitFromWarehouseDetailId.Length];
            ind = 0;
            foreach (var efw in arrExitFromWarehouseDetailId)
            {
                iarrExitFromWarehouseDetailId[ind] = System.Convert.ToInt32(efw);

            }
            IList<TExitFromWarehouseDetail> exitFromWarehouseDetails = tExitFromWarehouseDetailRep.FindBy(c => iarrExitFromWarehouseDetailId.Contains(c.ExitFromWarehouseDetailId), false).Item1.ToList();

            foreach (var efw in exitFromWarehouseDetails)
            {
                efw.IsActive = 25;
                efw.FactorNo = factorNo;
                efw.FactorDate = curDate;
                efw.FactorTime = curTime;
                efw.FactorEntryDataPersonalId = curUser;

                tExitFromWarehouseDetailRep.Update(efw);

                curTechnicalNo = efw.TechnicalCode;

                //TwareHouseStock
                wareHouseStock = tWareHouseStockRep.FindBy(c=>c.TechnicalCode == curTechnicalNo, false).Item1.FirstOrDefault();
                if(wareHouseStock!= null)
                {
                    wareHouseStock.ReserveQty = (wareHouseStock.ReserveQty - efw.Qty > 0 ? wareHouseStock.ReserveQty - efw.Qty : 0);
                }
            }
                        //

            tExitFromWarehouseHeaderRep.Save();
            tExitFromWarehouseDetailRep.Save();
            tWareHouseStockRep.Save();
            return factorNo;
        }


    }
}
