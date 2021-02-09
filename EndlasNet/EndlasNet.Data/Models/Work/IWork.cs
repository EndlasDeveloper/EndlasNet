using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public enum Status
    {
        NotStarted,
        InProgress,
        Complete,
        PastDue
    }
    public interface IWork
    {
        public Guid WorkId { get; set; }
        public string EndlasNumber { get; set; }
        public string WorkDescription { get; set; }
        public Status Status { get; set; }
        public string PurchaseOrderNum { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Customer Customer { get; set; }
    }
}
