using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OdaMeClone.Models
    {
    public class ApartmentAddOn
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The DB will auto-generate the Guid
        public Guid   ApartmentAddOnId { get; set; }
        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public Guid AddOnId { get; set; }
        public AddOn AddOn { get; set; }

        public int InstalledUnits { get; set; } // Additional property for installed units
        }
    }
