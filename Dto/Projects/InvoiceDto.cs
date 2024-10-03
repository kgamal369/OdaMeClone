using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class InvoiceDTO
        {
        public Guid InvoiceId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; }
        }
    }
