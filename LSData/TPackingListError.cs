//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LSData
{
    using System;
    using System.Collections.Generic;
    
    public partial class TPackingListError
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
        public short ErrorType { get; set; }
        public string PartName { get; set; }
    }
}
