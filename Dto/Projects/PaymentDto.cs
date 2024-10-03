using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class PaymentDTO
        {
        public Guid PaymentId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        }
    }
