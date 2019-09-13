using System;

namespace ScenarioBuilder.Models
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
