//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gamification.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GTTimeSheet
    {
        public decimal gtID { get; set; }
        public decimal gtTimePeriodID { get; set; }
        public decimal gtResourceID { get; set; }
        public Nullable<short> gtStatus { get; set; }
        public string gtMngrComments { get; set; }
        public string gtUserComments { get; set; }
        public Nullable<decimal> gtTotalHour { get; set; }
        public string gtInstanceID { get; set; }
        public Nullable<short> gtTRReview { get; set; }
        public Nullable<decimal> gtTotalPlan { get; set; }
        public Nullable<short> gtTaskDelay { get; set; }
        public Nullable<short> gtTaskProposed { get; set; }
    }
}