using System;
using System.ComponentModel.DataAnnotations;

namespace EndlasNet.Data
{
    /*
    * Class: Quote
    * Description: Model object/entity describing the Quote entity
    */
    public class Quote
    {
        [Key]
        public Guid QuoteId { get; set; }

        [Required]
        [Display(Name ="Endlas number")]
        public string EndlasNumber { get; set; }
    }
}