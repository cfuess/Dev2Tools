using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Dev2Tools.API.Data;
using Dev2Tools.API.Models;

namespace Dev2Tools.API.Controllers
{
    public class NgtsController : ApiController
    {
        // GET api/Ngts/000283192003
        public Employer Get(string id)
        {
            Employer employer = new Employer()
            {
                EmpId = 0,
                EsdNum = "None",
                LegalName = "No Employer Found",
                BusinessName = ""
            };

            if (string.IsNullOrWhiteSpace(id))
            {
                return employer;
            }

            using (var context = new Data.NGTSLOB_dev2Entities())
            {
                TBL_EMPR_EMP emp;
                if (id.Length == 12)
                {
                    emp = context.TBL_EMPR_EMP
                        .Include("TBL_ADDTL_BUSNS_NAME_ABN")
                        .Include("TBL_EMPR_LIAB_PERIOD_ELP")
                        .Include("TBL_WAGE_RPT_WRE")
                        .FirstOrDefault(e => e.EMP_ESD_NBR == id);
                }
                else
                {
                    var empId = long.Parse(id);
                    emp = context.TBL_EMPR_EMP
                    .Include("TBL_ADDTL_BUSNS_NAME_ABN")
                    .Include("TBL_EMPR_LIAB_PERIOD_ELP")
                    .Include("TBL_WAGE_RPT_WRE")
                    .FirstOrDefault(e => e.EMP_ID == empId);
                }

                if (emp == null) return employer;

                employer.EmpId = emp.EMP_ID;
                employer.EsdNum = emp.EMP_ESD_NBR;
                employer.LegalName = emp.EMP_LEGAL_ENTY_NAME;
                employer.BusinessName = emp.TBL_ADDTL_BUSNS_NAME_ABN.FirstOrDefault()?.ABN_ADDTL_BUSNS_NAME;

                foreach (var liab in emp.TBL_EMPR_LIAB_PERIOD_ELP)
                {
                    employer.LiabilityPeriods.Add(new LiabilityPeriod()
                    {
                        StartDate = liab.ELP_LIAB_START_DATE,
                        EndDate = liab.ELP_LIAB_END_DATE ?? DateTime.Now
                    });
                }

                foreach (var wage in emp.TBL_WAGE_RPT_WRE)
                {
                    employer.WagePeriods.Add(new WagePeriod()
                    {
                        Qtr = wage.WRE_QTR,
                        Year = wage.WRE_YR,
                        Gross = wage.WRE_GROSS_WAGES,
                        IsNoPayroll = wage.TBL_TAX_RPT_TRE.Any(t => t.TRE_NO_PAYRL_THIS_QTR == "Y")
                    });
                }
            }
            return employer;
        }

    }
}
