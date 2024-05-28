using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSData;
using FrameWorkSRV;

namespace LSService
{
  
    /// <summary>
    /// TUploadInf
    /// </summary>
    public interface ITUploadInfRepository : IGenericRepository<TUploadInf, UploadInf>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TUploadInfRepository : GenericRepository<LSDBEntities, TUploadInf, UploadInf>, ITUploadInfRepository
    {
    }
    /// <summary>
    /// TCat
    /// </summary>
    public interface ITCatRepository : IGenericRepository<TCat, Cat>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TCatRepository : GenericRepository<LSDBEntities, TCat, Cat>, ITCatRepository
    {
    }
    /// <summary>
    /// TOrder
    /// </summary>
    public interface ITOrderRepository : IGenericRepository<TOrder, Order>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TOrderRepository : GenericRepository<LSDBEntities, TOrder, Order>, ITOrderRepository
    {
    }
    /// <summary>
    /// TOrderHaveError
    /// </summary>
    public interface ITOrderHaveErrorRepository : IGenericRepository<TOrderHaveError, OrderHaveError>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TOrderHaveErrorRepository : GenericRepository<LSDBEntities, TOrderHaveError, OrderHaveError>, ITOrderHaveErrorRepository
    {
    }
    /// <summary>
    /// TOrderSerial
    /// </summary>
    public interface ITSerialRepository : IGenericRepository<TSerial, OrderSerial>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TSerialRepository : GenericRepository<LSDBEntities, TSerial, OrderSerial>, ITSerialRepository
    {
    }

    /// <summary>
    /// TPackingList
    /// </summary>
    public interface ITPackingListRepository : IGenericRepository<TPackingList, PackingList>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TPackingListRepository : GenericRepository<LSDBEntities, TPackingList, PackingList>, ITPackingListRepository
    {
    }
    /// <summary>
    /// TPackingListError
    /// </summary>
    public interface ITPackingListErrorRepository : IGenericRepository<TPackingListError, PackingListError>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TPackingListErrorRepository : GenericRepository<LSDBEntities, TPackingListError, PackingListError>, ITPackingListErrorRepository
    {
    }

    /// <summary>
    /// TInventoryDocHeader
    /// </summary>
    public interface ITInventoryDocHeaderRepository : IGenericRepository<TInventoryDocHeader, InventoryDocHeader>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TInventoryDocHeaderRepository : GenericRepository<LSDBEntities, TInventoryDocHeader, InventoryDocHeader>, ITInventoryDocHeaderRepository
    {
    }

    /// <summary>
    /// TInventoryDocDetail
    /// </summary>
    public interface ITInventoryDocDetailRepository : IGenericRepository<TInventoryDocDetail, InventoryDocDetail>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TInventoryDocDetailRepository : GenericRepository<LSDBEntities, TInventoryDocDetail, InventoryDocDetail>, ITInventoryDocDetailRepository
    {
    }
    /// <summary>
    /// TDocSerial
    /// </summary>
    public interface ITDocSerialRepository : IGenericRepository<TDocSerial, DocSerial>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TDocSerialRepository : GenericRepository<LSDBEntities, TDocSerial, DocSerial>, ITDocSerialRepository
    {
    }
    /// <summary>
    /// TBrand
    /// </summary>
    public interface ITBrandRepository : IGenericRepository<TBrand, Brand>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TBrandRepository : GenericRepository<LSDBEntities, TBrand, Brand>, ITBrandRepository
    {
    }
    /// <summary>
    /// TCarType
    /// </summary>
    public interface ITCarTypeRepository : IGenericRepository<TCarType, CarType>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TCarTypeRepository : GenericRepository<LSDBEntities, TCarType, CarType>, ITCarTypeRepository
    {
    }
    /// <summary>
    /// TCatCarType
    /// </summary>
    public interface ITCatCarTypeRepository : IGenericRepository<TCatCarType, CatCarType>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TCatCarTypeRepository : GenericRepository<LSDBEntities, TCatCarType, CatCarType>, ITCatCarTypeRepository
    {
    }
    /// <summary>
    /// TItemGroup
    /// </summary>
    public interface ITItemGroupRepository : IGenericRepository<TItemGroup, ItemGroup>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TItemGroupRepository : GenericRepository<LSDBEntities, TItemGroup, ItemGroup>, ITItemGroupRepository
    {
    }
    /// <summary>
    /// TItemType
    /// </summary>
    public interface ITItemTypeRepository : IGenericRepository<TItemType, ItemType>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TItemTypeRepository : GenericRepository<LSDBEntities, TItemType, ItemType>, ITItemTypeRepository
    {
    }

    /// <summary>
    /// TItemType
    /// </summary>
    public interface ITGoodsGradeRepository : IGenericRepository<TGoodsGrade, GoodsGrade>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TGoodsGradeRepository : GenericRepository<LSDBEntities, TGoodsGrade, GoodsGrade>, ITGoodsGradeRepository
    {
    }

    /// <summary>
    /// TBaseInf
    /// </summary>
    public interface ITBaseInfRepository : IGenericRepository<TBaseInf, BaseInf>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TBaseInfRepository : GenericRepository<LSDBEntities, TBaseInf, BaseInf>, ITBaseInfRepository
    {
    }

    /// <summary>
    /// TBaseInfType
    /// </summary>
    public interface ITBaseInfTypeRepository : IGenericRepository<TBaseInfType, BaseInfType>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TBaseInfTypeRepository : GenericRepository<LSDBEntities, TBaseInfType, BaseInfType>, ITBaseInfTypeRepository
    {
    }
    /// <summary>
    /// TUsageType
    /// </summary>
    public interface ITUsageTypeRepository : IGenericRepository<TUsageType, UsageType>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TUsageTypeRepository : GenericRepository<LSDBEntities, TUsageType, UsageType>, ITUsageTypeRepository
    {
    }
    /// <summary>
    /// VCat
    /// </summary>
    public interface IVCatRepository : IGenericRepository<VCat, VCatClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VCatRepository : GenericRepository<LSDBEntities, VCat, VCatClass>, IVCatRepository
    {
    }
    /// <summary>
    /// VCatCarType
    /// </summary>
    public interface IVCatCarTypeRepository : IGenericRepository<VCatCarType, VCatCarTypeClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VCatCarTypeRepository : GenericRepository<LSDBEntities, VCatCarType, VCatCarTypeClass>, IVCatCarTypeRepository
    {
    }
    /// <summary>
    /// TCustomer
    /// </summary>
    public interface ITCustomerRepository : IGenericRepository<TCustomer, Customer>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TCustomerRepository : GenericRepository<LSDBEntities, TCustomer, Customer>, ITCustomerRepository
    {
    }
    /// <summary>
    /// VCustomer
    /// </summary>
    public interface IVCustomerRepository : IGenericRepository<VCustomer, VCustomerClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VCustomerRepository : GenericRepository<LSDBEntities, VCustomer, VCustomerClass>, IVCustomerRepository
    {
    }

    /// <summary>
    /// GoodsPrice
    /// </summary>
    public interface ITGoodsPriceRepository : IGenericRepository<TGoodsPrice, GoodsPrice>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TGoodsPriceRepository : GenericRepository<LSDBEntities, TGoodsPrice, GoodsPrice>, ITGoodsPriceRepository
    {
    }

    /// <summary>
    /// GoodsPriceArc
    /// </summary>
    public interface ITGoodsPriceArcRepository : IGenericRepository<TGoodsPriceArc, GoodsPriceArc>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TGoodsPriceArcRepository : GenericRepository<LSDBEntities, TGoodsPriceArc, GoodsPriceArc>, ITGoodsPriceArcRepository
    {
    }

    /// <summary>
    /// VGoodsPrice
    /// </summary>
    public interface IVGoodsPriceRepository : IGenericRepository<VGoodsPrice, VGoodsPriceClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VGoodsPriceRepository : GenericRepository<LSDBEntities, VGoodsPrice, VGoodsPriceClass>, IVGoodsPriceRepository
    {
    }

    /// <summary>
    /// VGoodsPriceArc
    /// </summary>
    public interface IVGoodsPriceArcRepository : IGenericRepository<VGoodsPriceArc, VGoodsPriceArcClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VGoodsPriceArcRepository : GenericRepository<LSDBEntities, VGoodsPriceArc, VGoodsPriceArcClass>, IVGoodsPriceArcRepository
    {
    }
    /// <summary>
    /// TWareHouseStock
    /// </summary>
    public interface ITWareHouseStockRepository : IGenericRepository<TWareHouseStock, WareHouseStock>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TWareHouseStockRepository : GenericRepository<LSDBEntities, TWareHouseStock, WareHouseStock>, ITWareHouseStockRepository
    {
    }
    /// <summary>
    /// TWareHouseStock
    /// </summary>
    public interface IVWareHouseStockRepository : IGenericRepository<VWareHouseStock, VWareHouseStockClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VWareHouseStockRepository : GenericRepository<LSDBEntities, VWareHouseStock, VWareHouseStockClass>, IVWareHouseStockRepository
    {
    }
    /// <summary>
    /// TRequestHeader
    /// </summary>
    public interface ITRequestHeaderRepository : IGenericRepository<TRequestHeader, RequestHeader>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TRequestHeaderRepository : GenericRepository<LSDBEntities, TRequestHeader, RequestHeader>, ITRequestHeaderRepository
    {
    }
    /// <summary>
    /// TRequestDetial
    /// </summary>
    public interface ITRequestDetialRepository : IGenericRepository<TRequestDetial, RequestDetial>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TRequestDetialRepository : GenericRepository<LSDBEntities, TRequestDetial, RequestDetial>, ITRequestDetialRepository
    {
    }
    /// <summary>
    /// VRequest
    /// </summary>
    public interface IVRequestRepository : IGenericRepository<VRequest, VRequestClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VRequestRepository : GenericRepository<LSDBEntities, VRequest, VRequestClass>, IVRequestRepository
    {
    }
    /// <summary>
    /// TInvoiceHeader
    /// </summary>
    public interface ITInvoiceHeaderRepository : IGenericRepository<TInvoiceHeader, InvoiceHeader>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TInvoiceHeaderRepository : GenericRepository<LSDBEntities, TInvoiceHeader, InvoiceHeader>, ITInvoiceHeaderRepository
    {
    }
    /// <summary>
    /// TInvoiceDetail
    /// </summary>
    public interface ITInvoiceDetailRepository : IGenericRepository<TInvoiceDetail, InvoiceDetail>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TInvoiceDetailRepository : GenericRepository<LSDBEntities, TInvoiceDetail, InvoiceDetail>, ITInvoiceDetailRepository
    {
    }
    /// <summary>
    /// V_Invoice
    /// </summary>
    public interface IV_InvoiceRepository : IGenericRepository<V_Invoice, VInvoiceClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class V_InvoiceRepository : GenericRepository<LSDBEntities, V_Invoice, VInvoiceClass>, IV_InvoiceRepository
    {
    }
    /// <summary>
    /// TFactorHeader
    /// </summary>
    //public interface ITFactorHeaderRepository : IGenericRepository<TFactorHeader, FactorHeader>
    //{
    //    //IQueryable GetJobAndInjuryTbl();
    //}

    //public class TFactorHeaderRepository : GenericRepository<LSDBEntities, TFactorHeader, FactorHeader>, ITFactorHeaderRepository
    //{
    //}
    /// <summary>
    /// FactorDetail
    /// </summary>
    //public interface ITFactorDetailRepository : IGenericRepository<TFactorDetail, FactorDetail>
    //{
    //    //IQueryable GetJobAndInjuryTbl();
    //}

    //public class TFactorDetailRepository : GenericRepository<LSDBEntities, TFactorDetail, FactorDetail>, ITFactorDetailRepository
    //{
    //}

    /// <summary>
    /// VFactor00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
    /// </summary>
    public interface IVFactorRepository : IGenericRepository<VFactor, VFactorClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class VFactorRepository : GenericRepository<LSDBEntities, VFactor, VFactorClass>, IVFactorRepository
    {
    }
    /// <summary>
    /// TExitFromWarehouseHeader
    /// </summary>
    public interface ITExitFromWarehouseHeaderRepository : IGenericRepository<TExitFromWarehouseHeader, ExitFromWarehouseHeader>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TExitFromWarehouseHeaderRepository : GenericRepository<LSDBEntities, TExitFromWarehouseHeader, ExitFromWarehouseHeader>, ITExitFromWarehouseHeaderRepository
    {
    }
    /// <summary>
    /// TExitFromWarehouseDetail
    /// </summary>
    public interface ITExitFromWarehouseDetailRepository : IGenericRepository<TExitFromWarehouseDetail, ExitFromWarehouseDetail>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class TExitFromWarehouseDetailRepository : GenericRepository<LSDBEntities, TExitFromWarehouseDetail, ExitFromWarehouseDetail>, ITExitFromWarehouseDetailRepository
    {
    }
    /// <summary>
    /// V_ExitWarehouse
    /// </summary>
    public interface IV_ExitWarehouseRepository : IGenericRepository<V_ExitWarehouse, VExitWarehouseClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class V_ExitWarehouseRepository : GenericRepository<LSDBEntities, V_ExitWarehouse, VExitWarehouseClass>, IV_ExitWarehouseRepository
    {
    }

    public interface IV_ExitWarehouseHeaderRepository : IGenericRepository<V_ExitWarehouseHeader, VExitWarehouseHeaderClass>
    {
        //IQueryable GetJobAndInjuryTbl();
    }

    public class V_ExitWarehouseHeaderRepository : GenericRepository<LSDBEntities, V_ExitWarehouseHeader, VExitWarehouseHeaderClass>, IV_ExitWarehouseHeaderRepository
    {
    }
}
