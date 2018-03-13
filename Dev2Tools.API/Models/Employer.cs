using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dev2Tools.API.Models
{
    public class Employer
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string EsdNum { get; set; }
        public long EmpId { get; set; } 
        public List<LiabilityPeriod> LiabilityPeriods { get; set; } = new List<LiabilityPeriod>();
        public List<WagePeriod> WagePeriods { get; set; } = new List<WagePeriod>();
    }

    public class LiabilityPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class WagePeriod
    {
        private int _qtr;
        private int _year;

        public int Qtr
        {
            get { return _qtr; }
            set
            {
                _qtr = value;
                ComputeStartEnd();
            }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                ComputeStartEnd();
            }
        }

        public decimal Gross { get; set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        private void ComputeStartEnd()
        {
            if (_qtr == 0 || _year == 0) return;

            switch (_qtr)
            {
                case 1:
                    StartDate = new DateTime(_year, 1, 1);
                    EndDate = new DateTime(_year, 3, 31);
                    break;
                case 2:
                    StartDate = new DateTime(_year, 4, 1);
                    EndDate = new DateTime(_year, 6, 30);
                    break;
                case 3:
                    StartDate = new DateTime(_year , 7, 1);
                    EndDate = new DateTime(_year, 9, 30);
                    break;
                case 4:
                    StartDate = new DateTime(_year, 10, 1);
                    EndDate = new DateTime(_year, 12, 31);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("quarter", _qtr, "There are only 4 quarters in 1 year. Valid values are 1-4");
            }

        }
    }

}