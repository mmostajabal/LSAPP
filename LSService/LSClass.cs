using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSService
{
    public class LSClass
    {
    }

    public class UploadDocType
    {
        //public UploadDocType()
        //{
        //    this.TUploadInfs = new List<UploadInf>();
        //}

        public int UploadDocTypeId { get; set; }
        public string DocTypeDesc { get; set; }

        //public List<UploadInf> TUploadInfs { get; set; }
    }

    public class UploadInf
    {
        public int UploadInfId { get; set; }
        public string DocPath { get; set; }
        public string EntryPersonalId { get; set; }
        public int UploadDocTypeId { get; set; }
        public string DocTitle { get; set; }
        public string DocDesc { get; set; }

        public string OrderNo { get; set; }
        //public UploadDocType TUploadDocType { get; set; }
    }

    public class newPartsInf
    {
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }

    }

    public class Cat
    {
        public int ID { get; set; }
        public string TechnicalCode { get; set; }
        public string PNC { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double NetWeight { get; set; }
        public string ForWardItemNo { get; set; }
        public string ForWardITCCode { get; set; }
        public string BackwardItemNo { get; set; }
        public string BackwardITCCode { get; set; }
        public double QuantityOfUsageInMachine { get; set; }
        public double PPLDollar { get; set; }
        public double BasePriceRls { get; set; }
        public string ItemGroup { get; set; }
        public string Brand { get; set; }
        public string Health { get; set; }
        public string Grade { get; set; }
        public string ItemUsage { get; set; }
        public string UsagePlace { get; set; }
        public string Specificcode { get; set; }
        public string VeryCritical { get; set; }

        public int ItemGroupId { get; set; }
        public int ItemTypeId { get; set; }
        public int GoodsGradeId { get; set; }

        public int TrackingTypeId { get; set; }
        public int LabelingTypeId { get; set; }
        public int UsageTypeId { get; set; }
        public int UnitId { get; set; }        
        public short FlagUpdate { get; set; }
    }
    public class SearchCat
    {
        public int SearchID { get; set; }
        public string SearchTechnicalCode { get; set; }
        public string SearchPNC { get; set; }
        public string SearchItemName { get; set; }
        public string SearchPersianName { get; set; }
        public int SearchItemGroupId { get; set; }
        public int SearchItemTypeId { get; set; }
        public int SearchTrackingTypeId { get; set; }
        public int SearchGoodsGradeId { get; set; }
        public int SearchUsageTypeId { get; set; }

        public int SearchLabelingTypeId { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double PriceAED { get; set; }
        public string Tariff { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string OrderNo { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryDataPersonalId { get; set; }

        public int UploadInfId { get; set; }

        public double PackListQTY { get; set; }

        public string PackingListTechnicalCode { get; set; }

        public double RecieveOrderQuantity { get; set; }
        public short IsActive { get; set; }
    }

    public class OrderHaveError
    {
        public int OrderHaveErrorId { get; set; }
        public string TechnicalCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double PriceAED { get; set; }
        public string Tariff { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string OrderNo { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryDataPersonalId { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public bool IsSelected { get; set; }

        public short IsActive { get; set; }
        public int UploadInfId { get; set; }

    }

    public class OrderSerial
    {
        public int OrderSerialNo { get; set; }
        public int CurYear { get; set; }
    }

    public class PackingList
    {
        public int PackingListId { get; set; }
        public string PackingListNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CaseNo { get; set; }
        public string CountryOfOrigin { get; set; }
        public string TechnicalCode { get; set; }
        public double ShipQty { get; set; }
        public double ConfirmShipQty { get; set; }
        public string HSCode { get; set; }
        public double UnitPrice { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryDataPersonalId { get; set; }
        public int UploadInfId { get; set; }
        public string OrderNo { get; set; }
        public string TechnicalCodeOder { get; set; }

        public int OrderId { get; set; }

        public short IsActive { get; set;}
    }

    public class PackingListError
    {
        public int PackingListErrorId { get; set; }
        public string PackingListNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CaseNo { get; set; }
        public string CountryOfOrigin { get; set; }
        public string TechnicalCode { get; set; }
        public double ShipQty { get; set; }
        public string HSCode { get; set; }
        public double UnitPrice { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryDataPersonalId { get; set; }
        public int UploadInfId { get; set; }
        public string OrderNo { get; set; }
        public string TechnicalCodeOder { get; set; }
        public short IsActive { get; set; }

        public bool IsSelected { get; set; }

        public short ErrorType { get; set; }

        public string PartName { get; set; }
    }

    public class PackingListAndOrder
    {
        public int OrderId { get; set; }
        public string OrderTechnicalCode { get; set; }
        public string OrderItemName { get; set; }
        public string OrderPersianName { get; set; }
        public double OrderQuantity { get; set; }
        public double RecieveOrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public double OrderPriceAED { get; set; }
        public string OrderTariff { get; set; }
        public double OrderNetWeight { get; set; }
        public double OrderGrossWeight { get; set; }
        public string OrderNo { get; set; }
        public string OrderEntryDataDate { get; set; }


        public int PackingListId { get; set; }
        public string PackingListNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CaseNo { get; set; }
        public string CountryOfOrigin { get; set; }
        public string PackingListTechnicalCode { get; set; }

        public string PackingListItemName { get; set; }
        public double ShipQty { get; set; }
        public double ConfirmShipQty { get; set; }
        public string HSCode { get; set; }
        public double PackingListUnitPrice { get; set; }
        public double PackingListNetWeight { get; set; }
        public double PackingListGrossWeight { get; set; }
        public string PackingListEntryDataDate { get; set; }

        public bool IsSelected { get; set; }

        public string PartName { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }

        public string InventoryDocNo{ get; set; }
        public double InventoryQty { get; set; }
        public string InventoryDate { get; set; }
        public string InventoryTime { get; set; }

        public string InventoryPersonalId { get; set; }

    }

    public class InventoryDocDetail
    {
        public int InventoryDocDetailId { get; set; }
        public int InventoryDocHeaderId { get; set; }
        public string TechnicalCode { get; set; }
        public int OrderId { get; set; }
        public int PackingListId { get; set; }
        public double Qty { get; set; }
        public string Explain { get; set; }

        public InventoryDocHeader TInventoryDocHeader { get; set; }
    }

    public class InventoryDocHeader
    {
        public InventoryDocHeader()
        {
            this.InventoryDocDetails = new List<InventoryDocDetail>();
        }

        public int InventoryDocHeaderId { get; set; }
        public string DocNo { get; set; }
        public int InventoryDocTypeId { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryDataPersonalId { get; set; }
        public short IsActive { get; set; }

        public List<InventoryDocDetail> InventoryDocDetails { get; set; }
    }

    public class DocSerial
    {
        public int Id { get; set; }
        public int DocSerialNo { get; set; }
        public int CurYear { get; set; }
        public int InventoryDocTypeId { get; set; }
    }
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandDesc { get; set; }
        public short IsActive { get; set; }
    }

    public class CarType
    {
        public int CarTypeId { get; set; }
        public string CarTypeDesc { get; set; }
        public int BrandId { get; set; }
        public short IsActive { get; set; }
    }

    public class CatCarType
    {
        public int CatCarTypeId { get; set; }
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public int CarTypeId { get; set; }
        public string CarTypeDesc { get; set; }
        public double Coefficient { get; set; }
        public short IsActive { get; set; }
    }

    public class ItemGroup
    {
        public int ItemGroupId { get; set; }
        public string ItemGroupDesc { get; set; }
        public short IsActive { get; set; }
    }

    public class ItemType
    {
        public int ItemTypeId { get; set; }
        public string ItemTypeDesc { get; set; }
        public string ItemTypeCode { get; set; }
        public short IsActive { get; set; }
    }

    public class GoodsGrade
    {
        public int GoodsGradeId { get; set; }
        public string GoodsGradeDesc { get; set; }
        public short IsActive { get; set; }
    }

    public class BaseInf
    {
        public int BaseInfId { get; set; }
        public string BaseInfDesc { get; set; }
        public short IsActive { get; set; }
        public int BaseInfTypeId { get; set; }
    }

    public class BaseInfType
    {
        public int BaseInfTypeId { get; set; }
        public string BaseInfDesc { get; set; }
        public short IsActive { get; set; }
    }

    public class UsageType
    {
        public int UsageTypeId { get; set; }
        public string UsageTypeDesc { get; set; }
        public short IsActive { get; set; }
    }


    public class VCatClass
    {
        public int ID { get; set; }
        public string TechnicalCode { get; set; }
        public string PNC { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double NetWeight { get; set; }
        public int ItemGroupId { get; set; }
        public string ItemGroupDesc { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeDesc { get; set; }
        public int GoodsGradeId { get; set; }
        public string GoodsGradeDesc { get; set; }
        public int TrackingTypeId { get; set; }
        public string TrackingDesc { get; set; }
        public int LabelingTypeId { get; set; }
        public string LabelingDesc { get; set; }
        public int UsageTypeId { get; set; }
        public string UsageTypeDesc { get; set; }
        public int UnitId { get; set; }
        public string UnitDesc { get; set; }
    }

    public class VCatCarTypeClass
    {
        public string TechnicalCode { get; set; }
        public string PersianName { get; set; }
        public int? CatCarTypeId { get; set; }
        public double? Coefficient { get; set; }
        public short? CatCarTypeIsActive { get; set; }
        public string CarTypeDesc { get; set; }
        public string BrandDesc { get; set; }
        public int CarTypeId { get; set; }
        public string ItemName { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
        public int CustomerContractTypeId { get; set; }
        public string CustomerName { get; set; }
        public string Tel { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public short IsActive { get; set; }
        public short FlagUpdate { get; set; }
        public string NationalId { get; set; }
        public string ZIPCode{ get; set; }
        public string EcoCode { get; set; }
        public string RegistrationNumber { get; set; }
        public string RepresentativeCode { get; set; }
        public string RepresentativeName { get; set; }

    }


    public class VCustomerClass
    {
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
        public string CustomerTypeDesc { get; set; }
        public int CustomerContractTypeId { get; set; }
        public string CustomerContractTypeDesc { get; set; }
        public string CustomerName { get; set; }
        public string Tel { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public short IsActive { get; set; }
        public string NationalId { get; set; }
        public string ZIPCode { get; set; }
        public string EcoCode { get; set; }
        public string RegistrationNumber { get; set; }
        public string RepresentativeCode { get; set; }
        public string RepresentativeName { get; set; }
    }

    public class GoodsPrice
    {
        public int GoodsPriceId { get; set; }
        public string TechnicalCode { get; set; }
        public int CurrencyTypeId { get; set; }
        public double DollarPrice { get; set; }
        public double? BasePrice { get; set; }
        public double PriceInRials { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryPersonalId { get; set; }
    }

    public class GoodsPriceArc
    {
        public int GoodsPriceArcId { get; set; }
        public int GoodsPriceId { get; set; }
        public string TechnicalCode { get; set; }
        public int CurrencyTypeId { get; set; }
        public double DollarPrice { get; set; }
        public double? BasePrice { get; set; }
        public double PriceInRials { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryPersonalId { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateTime { get; set; }
        public string UpdatePersonalId { get; set; }
    }

    public class VGoodsPriceClass
    {
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public int CurrencyTypeId { get; set; }
        public string CurrencyTypeDesc { get; set; }
        public double DollarPrice { get; set; }
        public double? BasePrice { get; set; }
        public string PriceInRials { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryPersonalId { get; set; }
    }

    public class VGoodsPriceArcClass
    {
        public int GoodsPriceArcId { get; set; }
        public int GoodsPriceId { get; set; }
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public int CurrencyTypeId { get; set; }
        public string CurrencyTypeDesc { get; set; }
        public double DollarPrice { get; set; }
        public double? BasePrice { get; set; }
        public string PriceInRials { get; set; }
        public string EntryDataDate { get; set; }
        public string EntryDataTime { get; set; }
        public string EntryPersonalId { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateTime { get; set; }
        public string UpdatePersonalId { get; set; }
    }
    public class WareHouseStock
    {
        public int WareHouseStockId { get; set; }
        public string TechnicalCode { get; set; }
        public double StockQty { get; set; }
        public double ReserveQty { get; set; }
        public double BlockedQty { get; set; }
        public double ScrapeQty { get; set; }
    }

    public class VWareHouseStockClass
    {
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public int UnitId { get; set; }
        public string UnitDesc { get; set; }
        public double? ReserveQty { get; set; }
        public double? BlockedQty { get; set; }
        public double? ScrapeQty { get; set; }
        public double? StockQty { get; set; }
    }

    public class RequestDetial
    {
        public int RequestDetialId { get; set; }
        public string TechnicalCode { get; set; }
        public double Qty { get; set; }
        public double ConfirmQty { get; set; }
        public int RequestId { get; set; }
        public short IsActive { get; set; }
        public string RequestNo { get; set; }

        public double InvoiceQty { get; set; }
    }

    public class RequestHeader
    {
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string RequestTime { get; set; }
        public int CustomerId { get; set; }
        public string EntryPersonalId { get; set; }
        public short IsActive { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestTitle { get; set; }
        public short IconType { get; set; }
    }

    public class VRequestClass
    {
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public int UnitId { get; set; }
        public string UnitDesc { get; set; }
        public double Qty { get; set; }
        public double ConfirmQty { get; set; }
        public short RequestDetailIsActive { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string RequestTime { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EntryPersonalId { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestTypeDesc { get; set; }
        public string RequestTitle { get; set; }
        public short RequestHeaderIsActive { get; set; }
        public int RequestDetialId { get; set; }
        public int RequestId { get; set; }

        public double StockQty { get; set; }
        public double ReserveQty { get; set; }
        public double BlockedQty { get; set; }
        public double ScrapeQty { get; set; }
        public double InvoiceQty { get; set; }
        public int KeepCustomerId { get; set; }
        public short FlagUpdate { get; set; }
    }

    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public string TechnicalCode { get; set; }
        public double Qty { get; set; }
        public short IsActive { get; set; }
        public int RequestDetialId { get; set; }
        public string InvoiceNo { get; set; }

        public string ConfirmDate { get; set; }
        public string ConfirmTime { get; set; }
        public string ConfirmPersonalId { get; set; }

        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string PrintPersonalId { get; set; }
        public double ExitQty { get; set; }

        public double PriceInRials { get; set; }
    }

    public class InvoiceHeader
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceTitle { get; set; }
        public int CustomerId { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceTime { get; set; }
        public string EntryPersonalId { get; set; }
        public short IsActive { get; set; }
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
    }
    public class VInvoiceClass
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceTitle { get; set; }
        public int CustomerId { get; set; }
        public int CustomerTypeId { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceTime { get; set; }
        public string EntryPersonalId { get; set; }
        public short InvoiceHeaderIsActive { get; set; }
        public int RequestId { get; set; }
        public string RequestNo { get; set; }
        public string TechnicalCode { get; set; }
        public double Qty { get; set; }
        public int RequestDetialId { get; set; }
        public string ItemName { get; set; }
        public string PersianName { get; set; }
        public string CustomerName { get; set; }
        public short InvoiceDetailIsActive { get; set; }
        public double PriceInRials { get; set; }
        public string ConfirmDate { get; set; }
        public string ConfirmTime { get; set; }
        public string ConfirmPersonalId { get; set; }
        public int InvoiceDetailId { get; set; }
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string PrintPersonalId { get; set; }
        public double ExitQty { get; set; }
    }
    public class FactorDetail
    {
        public int FactorDetailId { get; set; }
        public string FactorNo { get; set; }
        public string TechnicalCode { get; set; }
        public double Qty { get; set; }
        public short IsActive { get; set; }
        public int InvoiceDetailId { get; set; }
        public double Price { get; set; }
    }

    public class FactorHeader
    {
        public int FactorHeaderId { get; set; }
        public int InvoiceId { get; set; }
        public string FactorNo { get; set; }
        public string FactorDate { get; set; }
        public string FactorTime { get; set; }
        public string EntryPersonalId { get; set; }
        public short IsActive { get; set; }
        public int CustomerId { get; set; }
    }

    public class VFactorClass
    {
        public int FactorHeaderId { get; set; }
        public int InvoiceId { get; set; }
        public string FactorNo { get; set; }
        public string FactorDate { get; set; }
        public string FactorTime { get; set; }
        public string EntryPersonalId { get; set; }
        public short IsActive { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int FactorDetailId { get; set; }
        public string TechnicalCode { get; set; }
        public double Qty { get; set; }
        public short EXPR1 { get; set; }
        public int InvoiceDetailId { get; set; }
        public double Price { get; set; }
        public string ItemName { get; set; }
    }

    public class ExitFromWarehouseDetail
    {
        public int ExitFromWarehouseDetailId { get; set; }
        public string ExitNo { get; set; }
        public string TechnicalCode { get; set; }
        public double Qty { get; set; }
        public short IsActive { get; set; }
        public int InvoiceDetailId { get; set; }

        public string FactorNo { get; set; }
        public string  FactorDate { get; set; }
        public string FactorTime { get; set; }

        public string FactorEntryDataPersonalId { get; set; }

    }

    public class ExitFromWarehouseHeader
    {
        public int ExitFromWarehouseHeaderId { get; set; }
        public string InvoiceNo { get; set; }
        public string ExitNo { get; set; }
        public int CustomerId { get; set; }
        public string ExitDate { get; set; }
        public string ExitTime { get; set; }
        public string EntryPersonalId { get; set; }
        public short IsActive { get; set; }

        public int InvoiceId { get; set; }
        public string ExitTitle { get; set; }
    }

    public class VExitWarehouseClass
    {
        public int ExitFromWarehouseDetailId { get; set; }
        public string TechnicalCode { get; set; }
        public string ItemName { get; set; }
        public short ExitDetailIsActive { get; set; }
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ExitDate { get; set; }
        public string ExitTime { get; set; }
        public string EntryPersonalId { get; set; }
        public short ExitHeaderIsActive { get; set; }
        public string ExitNo { get; set; }
        public int InvoiceId { get; set; }
        public string ExitTitle { get; set; }
        public string ExitHeaderIsActiveDesc { get; set; }
        public string ExitDetailIsActiveDesc { get; set; }
        public double Qty { get; set; }
        public int InvoiceDetailId { get; set; }
        public string InvoiceTitle { get; set; }
        public int ExitFromWarehouseHeaderId { get; set; }

        public double PriceInRials { get; set; }

        public string FactorNo { get; set; }
        public string FactorDate { get; set; }
        public string FactorTime { get; set; }
        public string FactorEntryDataPersonalId { get; set; }

    }
    public class VExitWarehouseHeaderClass
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ExitDate { get; set; }
        public string ExitTime { get; set; }
        public short ExitHeaderIsActive { get; set; }
        public short ExitDetailIsActive { get; set; }
        public string InvoiceNo { get; set; }
        public string ExitNo { get; set; }
        public string InvoiceTitle { get; set; }
        public string ExitTitle { get; set; }
        public int ExitFromWarehouseHeaderId { get; set; }
    }

}
