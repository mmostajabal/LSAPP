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
    
    public partial class TInvoiceDetail
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
}
