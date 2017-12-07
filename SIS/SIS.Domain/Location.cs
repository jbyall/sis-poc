using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SIS.Domain
{
    #region location constants
    // Used to control location types and for coding convenience
    // THIS CLASS DOES NOT REPRESENT A DATABASE TABLE
    public static class LocationTypes
    {
        // These string properties will allow referencing
        // the string values from any code without having to type them
        // out every time.
        [Display(Name = "Distribution")]
        public const string Distribution = "Distribution";
        [Display(Name = "Storage")]
        public const string Storage = "Storage";
        [Display(Name = "Sub-Basement")]
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

        public static List<string> GetLocationTypesList()
        {
            var fields = typeof(LocationTypes).GetFields().ToList();
            var result = new List<string>();
            foreach (var item in fields)
            {
                result.Add(item.CustomAttributes.First().NamedArguments.First().TypedValue.Value.ToString());
            }
            return result;
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

        // Defines 1-many relationship between a location and its item locations
        public ICollection<ItemLocation> ItemLocations { get; set; }

    }
}
