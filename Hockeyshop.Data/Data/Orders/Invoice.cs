﻿using Hockeyshop.Data.Data.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Orders
{
    public class Invoice
    {
        [Key]
        public int IdInvoice { get; set; }

        [Required(ErrorMessage = "The Order field is required.")]
        [ForeignKey("Order")]
        [Display(Name = "Order ID")]
        public int IdOrder { get; set; }
        public Order Order { get; set; }

        [Required(ErrorMessage = "The Invoice Number field is required.")]
        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "The Issue Date field is required.")]
        [Display(Name = "Issue date")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "The Total Amount field is required.")]
        [Display(Name = "Total amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "The User field is required.")]
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public int IdUser { get; set; }
        public User User { get; set; }
    }
}
