using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    #region enums
    // Used to control location types and for coding convenience
    public static class LocationTypes
    {
        // These string properties will allow referencing
        // the string values from any code without having to type them
        // out every time.
        public const string Distribution = "Distribution";
        public const string Storage = "Storage";
        public const string SubBasement = "Sub-Basement";

        public static string GetLocationTypeFromLocation(string location)
        {
            location = location.ToLower();
            if (location.Contains("dist"))
            {
                return LocationTypes.Distribution;
            }

            if (location.Contains("stor"))
            {
                return LocationTypes.Storage;
            }

            if (location.Contains("sub"))
            {
                return LocationTypes.SubBasement;
            }
            return "N/A";
        }
    }
    #endregion

    [Table("Location_LUT")]
    public class Location
    {
        [Key]
        [StringLength(8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [StringLength(12)]
        public string Type { get; set; }
        [StringLength(10)]
        public string OldLocation { get; set; }

        public ICollection<ItemLocation> ItemLocations { get; set; }

    }
}
