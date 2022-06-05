using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modals.Orther
{
    public class Payroll
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public int WokingDays { get; set; }
        public int PaidLeave { get; set; }
        public int UnpaidLeave { get; set; }
        public int Late { get; set; }
        public int Total { get; set; }

    }
}
