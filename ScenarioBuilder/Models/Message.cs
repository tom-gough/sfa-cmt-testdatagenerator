using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Models
{
    public class Message
    {
        public long Id { get; set; }
        public long CommitmentId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public short CreatedBy { get; set; }
    }
}
