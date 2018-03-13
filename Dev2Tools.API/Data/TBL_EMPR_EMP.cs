//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dev2Tools.API.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_EMPR_EMP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_EMPR_EMP()
        {
            this.TBL_EMPR_LIAB_PERIOD_ELP = new HashSet<TBL_EMPR_LIAB_PERIOD_ELP>();
            this.TBL_WAGE_RPT_WRE = new HashSet<TBL_WAGE_RPT_WRE>();
        }
    
        public long EMP_ID { get; set; }
        public string EMP_ESD_NBR { get; set; }
        public string EMP_EIN { get; set; }
        public string EMP_UBI { get; set; }
        public string EMP_DSHS_CASE_NBR { get; set; }
        public string EMP_LEGAL_ENTY_NAME { get; set; }
        public Nullable<long> NAI_ID { get; set; }
        public Nullable<long> SIC_ID { get; set; }
        public long REF_ID_EMPR_STATUS { get; set; }
        public Nullable<long> REF_ID_LIAB_TYPE { get; set; }
        public Nullable<long> REF_ID_CORPTN_TYPE { get; set; }
        public Nullable<long> REF_ID_PAYMT_METHD { get; set; }
        public Nullable<long> REF_ID_EMPR_CLASS { get; set; }
        public Nullable<long> REF_ID_LIAB_DATE_CHNG_REASON { get; set; }
        public string EMP_OUT_OF_BALNC_FLAG { get; set; }
        public string EMP_501C3_RECVD_VERIFIED_FLAG { get; set; }
        public string EMP_IVRS_PIN { get; set; }
        public Nullable<long> REF_ID_MULTI_CD { get; set; }
        public long REF_ID_EMPR_ACCT_TYPE { get; set; }
        public Nullable<long> DTO_ID { get; set; }
        public string EMP_INCLUDE_TRIBAL_PACKET { get; set; }
        public string EMP_SUPPRESS_EMPR_PACKET { get; set; }
        public long REF_ID_DELIVERY_METHD { get; set; }
        public string EMP_BUSNS_ACTVY { get; set; }
        public Nullable<System.DateTime> EMP_RGSTRED_DATE { get; set; }
        public Nullable<long> REF_ID_FORCE_OPEN_REASON { get; set; }
        public string EMP_BANKRUPTCY_FLAG { get; set; }
        public string EMP_ADT_FLAG { get; set; }
        public string EMP_APPEAL_FLAG { get; set; }
        public Nullable<long> NAI_ID_PREVIOUS { get; set; }
        public string EMP_HISTORICAL_DOC_CONVERTED { get; set; }
        public string EMP_COLCTN_FLAG { get; set; }
        public Nullable<System.DateTime> EMP_DETRMN_DATE { get; set; }
        public string EMP_INVSTGN_FLAG { get; set; }
        public Nullable<System.DateTime> EMP_EMPR_CLASS_EFCTV_DATE { get; set; }
        public long USR_LAST_MODFD_BY { get; set; }
        public System.DateTime EMP_LAST_MODFD_DATE { get; set; }
        public long BTA_ID { get; set; }
        public string EMP_COPES_SENT_FLAG { get; set; }
        public string EMP_STATE_AGENCY_CD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_EMPR_LIAB_PERIOD_ELP> TBL_EMPR_LIAB_PERIOD_ELP { get; set; }
        public virtual TBL_EMPR_EMP TBL_EMPR_EMP1 { get; set; }
        public virtual TBL_EMPR_EMP TBL_EMPR_EMP2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_WAGE_RPT_WRE> TBL_WAGE_RPT_WRE { get; set; }
    }
}