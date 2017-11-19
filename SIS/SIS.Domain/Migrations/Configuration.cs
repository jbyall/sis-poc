namespace SIS.Domain.Migrations
{
    using SIS.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SIS.Domain.SisDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SIS.Domain.SisDbContext context)
        {
            using (var sis = new SISDbContext())
            {
                // Seed look-up tables
                if (context.Departments.Count() < 1)
                {
                    var oldDepts = sis.Department_LUT.ToList();
                    foreach (var dept in oldDepts)
                    {
                        var newDept = new Department { Name = dept.Department, Description = dept.C_Dept_desc };
                        context.Departments.Add(newDept);
                    }
                    context.SaveChanges();
                }

                if (context.Locations.Count() < 1)
                {
                    var oldLocations = sis.Location_LUT.ToList();
                    foreach (var loc in oldLocations)
                    {
                        var newLocation = new Location { Id = loc.Location, Type = loc.Loc_Type, OldLocation = loc.Old_Loc };
                        context.Locations.Add(newLocation);
                    }
                    context.SaveChanges();
                }

                if (context.Suppliers.Count() < 1)
                {
                    var oldSups = sis.Supplier_LUT.ToList();
                    foreach (var sub in oldSups)
                    {
                        var newSup = new Supplier
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
                        context.Suppliers.Add(newSup);
                    }
                    context.SaveChanges();
                }

                if (context.Items.Count() < 1)
                {
                    var oldItems = sis.Item_Master.ToList();
                    foreach (var item in oldItems)
                    {
                        var newItem = new Item
                        {
                            Id = item.Item_Number,
                            Name = item.Item_Name.Trim(new[] {'"'}), // remove leading and trailing "
                            Unit = item.Unit,
                            Price = item.Price,
                            ReorderPoint = item.Reorder_point,
                            SupplierId = item.Supplier_ID,
                            Comment = item.Comment
                        };
                        context.Items.Add(newItem);
                    }
                    context.SaveChanges();

                }

                if (context.ItemLocations.Count() < 1)
                {
                    var old = sis.Item_Location.ToList();
                    foreach (var oldLocation in old)
                    {
                        if (!context.Locations.Any(l => l.Id == oldLocation.Location))
                        {
                            var missingLocation = new Location { Id = oldLocation.Location, Type = LocationTypes.SubBasement, OldLocation = "N/A" };
                            context.Locations.Add(missingLocation);
                            context.SaveChanges();
                        }
                        var newItemLoc = new ItemLocation
                        {
                            ItemId = oldLocation.Item_Number,
                            LocationId = oldLocation.Location,
                            QuantityOnHand = oldLocation.Qty_OnHand
                        };
                        context.ItemLocations.Add(newItemLoc);
                        
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
