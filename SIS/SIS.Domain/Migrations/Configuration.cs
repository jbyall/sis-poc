namespace SIS.Domain.Migrations
{
    using LegacyData;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SisDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // This method runs when executing migrations from the package manager console.
        // It migrates data from the legacy data model
        // to the updated data model. To run, you will need to modify
        // the connection strings in SIS.Domain.App.config to connect
        // to your instance of SQL Server

        // NOTE - TO PREVENT UNINTENDED DATA SEEDING, ALL CODE HAS BEEN COMMENTED
        // TO RUN, YOU WILL NEED TO UNCOMMENT EVERYTHING WITHIN THIS SEED METHOD
        protected override void Seed(SisDbContext context)
        {
            using (var legacyContext = new LegacyDbContext())
            {
                // SEED DEPARTMENTS IF NONE EXIST
                if (context.Departments.Count() < 1)
                {
                    // Get all departments from the legacy database
                    var legacyDepartments = legacyContext.Department_LUT.ToList();

                    // for each legacy dept, modify as needed and add to the collection (in-memory)
                    foreach (var dept in legacyDepartments)
                    {
                        var newDept = new Department { Id = dept.Department, Description = dept.C_Dept_desc };
                        context.Departments.Add(newDept);
                    }

                    // Add dummy department for receiving transactions
                    context.Departments.Add(new Department
                    {
                         Id = "3XX99",
                         Description = "Receiving"
                    });

                    // Commit the new records to the database
                    context.SaveChanges();
                }

                // SEED LOCATIONS IF NONE EXIST
                if (context.Locations.Count() < 1)
                {
                    // Get all locations from the legacy database
                    var legacyLocations = legacyContext.Location_LUT.ToList();

                    // for each legacy location, modify as needed and add to the collection (in-memory)
                    foreach (var loc in legacyLocations)
                    {
                        var newLocation = new Location { Id = loc.Location, Type = loc.Loc_Type, OldLocation = loc.Old_Loc };
                        context.Locations.Add(newLocation);
                    }

                    // Commit the new records to the database
                    context.SaveChanges();
                }

                // SEED SUPPLIERS IF NONE EXIST
                if (context.Suppliers.Count() < 1)
                {
                    // Get all suppliers from the legacy database
                    var legacySuppliers = legacyContext.Supplier_LUT.ToList();

                    // for each legacy supplier, modify as needed and add to the collection (in-memory)
                    foreach (var sub in legacySuppliers)
                    {
                        var newSupplier = new Supplier
                        {
                            Id = sub.SupplierID,
                            Name = sub.Supplier,
                            Address = sub.Address,
                            Address2 = sub.Address2,
                            City = sub.City,
                            State = sub.State,
                            Zip = sub.Zip,
                            Comment = sub.Comment
                        };
                        context.Suppliers.Add(newSupplier);
                    }

                    // Commit the new records to the database
                    context.SaveChanges();
                }

                // SEED ITEMS IF NONE EXIST
                if (context.Items.Count() < 1)
                {
                    // Get all items from the legacy database
                    var legacyItems = legacyContext.Item_Master.ToList();

                    // for each legacy item, modify as needed and add to the collection (in-memory)
                    foreach (var item in legacyItems)
                    {
                        var newItem = new Item
                        {
                            Id = item.Item_Number,
                            Name = item.Item_Name.Trim(new[] { '"' }), // remove leading and trailing "
                            Unit = item.Unit,
                            Price = item.Price,
                            ReorderPoint = item.Reorder_point,
                            SupplierId = item.Supplier_ID,
                            Comment = item.Comment
                        };
                        context.Items.Add(newItem);
                    }

                    // Commit the new records to the database
                    context.SaveChanges();
                }

                // SEED ITEM LOCATIONS IF NONE EXIST
                if (context.ItemLocations.Count() < 1)
                {
                    // Get all of the item locations from the legacy database
                    var legacyItemLocations = legacyContext.Item_Location.ToList();

                    // for each legacy location, modify as needed and add to the collection (in-memory)
                    foreach (var legacyLocation in legacyItemLocations)
                    {
                        // Since there is now a relationship between location and item location
                        // we need to add any location that exists in legacy Item_Location but
                        // was missing from the legacy Locations_LUT
                        if (!context.Locations.Any(l => l.Id == legacyLocation.Location))
                        {
                            var missingLocation = new Location { Id = legacyLocation.Location, Type = LocationTypes.SubBasement, OldLocation = "NA" };
                            context.Locations.Add(missingLocation);
                            context.SaveChanges();
                        }
                        var newItemLoc = new ItemLocation
                        {
                            ItemId = legacyLocation.Item_Number,
                            LocationId = legacyLocation.Location,
                            QuantityOnHand = legacyLocation.Qty_OnHand
                        };
                        context.ItemLocations.Add(newItemLoc);
                    }

                    // Commit the new records to the database
                    context.SaveChanges();
                }

                // SEED UNITS
                if (context.Units.Count() < 1)
                {
                    // Create list of units in memory
                    var seedUnits = new List<Unit>
                    {
                        new Unit { Code = "BT", Description = "Batch"},
                        new Unit { Code = "BX", Description = "Box"},
                        new Unit { Code = "DZ", Description = "Dozen"},
                        new Unit { Code = "EA", Description = "Each"},
                        new Unit { Code = "HD", Description = "HD"},
                        new Unit { Code = "MX", Description = "MX"},
                        new Unit { Code = "PK", Description = "Pack"},
                        new Unit { Code = "PR", Description = "PR"},
                        new Unit { Code = "RM", Description = "Ream"},
                        new Unit { Code = "RO", Description = "Roll"},
                        new Unit { Code = "SE", Description = "Set"},
                        new Unit { Code = "TU", Description = "TU"},
                    };
                    context.Units.AddRange(seedUnits);

                    // Commit the new records to the database
                    context.SaveChanges();
                }
            }
        }
    }
}
