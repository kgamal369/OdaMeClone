using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using OdaMeClone.Models;

namespace OdaMeClone.Dtos.Projects
    {
    public class PackageDTO
        {
        public Guid PackageId { get; set; }

        public string? PackageName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PackageType PackageType { get; set; }

        public decimal Price { get; set; }
        }
    }
