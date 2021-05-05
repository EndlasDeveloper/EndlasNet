using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class PowderBottle
    {
        [Key]
        public Guid PowderBottleId { get; set; }

        [Required]
        [Display(Name = "Bottle number")]
        public string BottleNumber { get; set; }

        [Required]
        [Range(1.0f, 1000.0f)]
        [Display(Name = "Initial weight (lbs)")]
        public float InitWeight { get; set; }

        [Required]
        [Range(0f, 1000.0f)]
        [Display(Name = "Weight (lbs)")]
        public float Weight { get; set; }

        [Display(Name = "Lot number")]
        [Required]
        public string LotNumber { get; set; }

        [ForeignKey("LineItemId")]
        public Guid LineItemId { get; set; }
        public virtual LineItem LineItem { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User{ get; set; }

        [ForeignKey("StaticPowderInfoId")]
        public Guid? StaticPowderInfoId { get; set; }
        public virtual StaticPowderInfo StaticPowderInfo { get; set; }

        [NotMapped]
        public string PowderName { get; set; }

        public IEnumerable<PowderForPart> PowderForParts { get; set; }

        [NotMapped]
        public bool IsWeightValid
        {
            get => InitWeight >= Weight;
        }

        [NotMapped]
        [Display(Name ="Cost per lb")]
        public float CostPerPound { get; set; }

    }
}
