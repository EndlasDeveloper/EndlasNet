﻿function getWorkFromInput() {
    return {
        "workID": getValue("workID"),  
    };
}
/*
[Key]
        public Guid WorkId { get; set; }

[Required]
[Display(Name = "Endlas number")]
        public string EndlasNumber { get; set; }

[Required]
[Display(Name = "Work description")]
        public string WorkDescription { get; set; }
[Required]
[Display(Name = "Status")]
        public Status Status { get; set; }

[Display(Name = "Purchase order number")]
        public string PurchaseOrderNum { get; set; }

[Required]
[Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

[ForeignKey("UserId")]
        public Guid ? UserId { get; set; }
        public virtual User User { get; set; }

[ForeignKey("CustomerId")]
        public Guid ? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public IEnumerable < PartForWork > PartsForWork { get; set; }
        public IEnumerable < MachiningToolForJob > ToolsForJob { get; set; }
        public IEnumerable < MachiningToolForWorkOrder > ToolsForWorkOrder { get; set; }*/