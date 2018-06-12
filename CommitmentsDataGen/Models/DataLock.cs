using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsDataGen.Models
{
    public class DataLock
    {
        public string PriceEpisodeIdentifier { get; set; }
        public string IlrTrainingCourseCode { get; set; }
        public int IlrTrainingType { get; set; }
        public DateTime IlrEffectiveFromDate { get; set; }
        public decimal IlrTotalCost { get; set; }
        public int ErrorCode { get; set; }
        public int Status { get; set; }
    }
}
