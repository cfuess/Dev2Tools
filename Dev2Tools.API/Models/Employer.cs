using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dev2Tools.API.Models
{
    public class Employer
    {
        [Required(ErrorMessage = "Name is required.")]
        public string LegalName { get; set; }
        public string BusinessName { get; set; }
        public string EsdNum { get; set; }
        public long EmpId { get; set; } 
        public List<LiabilityPeriod> LiabilityPeriods { get; set; } = new List<LiabilityPeriod>();
        public List<WagePeriod> WagePeriods { get; set; } = new List<WagePeriod>();
    }
}
//TBL_ADDTL_BUSNS_NAME_ABN