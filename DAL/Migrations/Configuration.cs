using MTFS.Business.Domain.Model;
using System.Data.Entity.Migrations;
using System.Linq;
using MTFS.DAL.Context;
using System.Data.Entity;

namespace MTFS.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MFTSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            //update-database -force
            //add-migration
        }

        protected override void Seed(MFTSContext context)
        {

            #region Currency

            context.Currencies.AddOrUpdate(
                r => r.code, new Currency
                {
                    code = "00001",
                    name = "Doller",
                    symbol = "$"
                }
                );

            context.Currencies.AddOrUpdate(
            r => r.code, new Currency
            {
                code = "00002",
                name = "Euro",
                symbol = "€"
            }
            );

            context.Currencies.AddOrUpdate(
            r => r.code, new Currency
            {
                code = "00003",
                name = "Derham",
                symbol = "د.إ."
            }
            );

            context.Currencies.AddOrUpdate(
            r => r.code, new Currency
            {
                code = "00004",
                name = "Rial",
                symbol = "ريال"
            }
            );

            #endregion

            #region Country

            context.Countries.AddOrUpdate(
            p => p.countryName,
             new Country
             {
                 countryName = "USA"
             }
            );

            context.Countries.AddOrUpdate(
            p => p.countryName,
                new Country
                {
                    countryName = "Iran"
                }
            );

            context.Countries.AddOrUpdate(
            p => p.countryName,
                new Country
                {
                    countryName = "UK"
                }
             );

            context.Countries.AddOrUpdate(
            p => p.countryName,
                new Country
                {
                    countryName = "Turkey"
                }
            );

            #endregion

            #region Transport Type

            context.Transporttypes.AddOrUpdate(
            p => p.typeName,
            new Transporttype
            {
                typeName = "Sea",
            }
           ); 

            context.Transporttypes.AddOrUpdate(
            p => p.typeName,
            new Transporttype
            {
                typeName = "Road",
            }
           );

            context.Transporttypes.AddOrUpdate(
           p => p.typeName,
           new Transporttype
           {
               typeName = "Air",
           }
          );

            context.Transporttypes.AddOrUpdate(
            p => p.typeName,
            new Transporttype
            {
                typeName = "Rail",
            }
           ); 

            context.Transporttypes.AddOrUpdate(
             p => p.typeName,
             new Transporttype
             {
                 typeName = "River",
             }
            );

            #endregion

            #region Subsystem

            context.Subsystems.AddOrUpdate(
              r => r.subsyatemName, new Subsystem
              {
                  subsyatemName = "Forwarding System",
                  isActive = true,
                  subsyatemDescription = ""
              }
              );

            context.Subsystems.AddOrUpdate(
            r => r.subsyatemName, new Subsystem
            {
                subsyatemName = "Shipping System",
                isActive = false,
                subsyatemDescription = ""
            }
            );

            #endregion
             
            context.Companies.AddOrUpdate(
             p => p.companyName,
             new Company
             {
                 companyName = "Satras",
                 isActive = true,

             }
            );
             
            context.SaveChanges();


            if (context.CompanySubsystems.Count() == 0)
            {
                context.CompanySubsystems.Add(
               new CompanySubsystem
               {
                   companyId = context.Companies.FirstOrDefault(r => r.companyName == "Satras").id,
                   subsystemId = context.Subsystems.FirstOrDefault(r => r.subsyatemName == "Forwarding System").id,
               }
              );
            }
            

            context.Users.AddOrUpdate(
              p => p.username,
              new User
              {
                  username = "admin"
              ,
                  password = "UizUE8ZcQH+nwJd+vYnzvQ==" // => 2
              ,
                  fullName = "Keiwan Azaryoun"
              ,
                  address = ""
              ,
                  email = "k.azaryoun@gmail.com"
              ,
                  mobile = "09122764983"
              ,
                  isItemAdmin = true,
                  isDataAdmin = true,
                  isActive = true,
                  nationalCode = "0062087465",
                  telephone = "88871876"
                  ,
                  companyId = context.Companies.FirstOrDefault().id

              }
            );



            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Home Page",
                subsystemId = 1,
                displayOrder = 1,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "/",
                titleStyle = "fa fa-lg fa-fw fa-home  txt-color-blue",
                titleText = "Home",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Sales & Marketing",
                subsystemId = 1,
                displayOrder = 2,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-handshake-o  txt-color-blue",
                titleText = "Sales & Marketing",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Contract & Document",
                subsystemId = 1,
                displayOrder = 3,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-credit-card",
                titleText = "Contract & Document",
            });
            //"fa fa-lg fa-fw circle-o"
            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Operation Management",
                subsystemId = 1,
                displayOrder = 4,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-money",
                titleText = "Operation Management",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Financial Management",
                subsystemId = 1,
                displayOrder = 5,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-money",
                titleText = "Financial Management",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "BI",
                subsystemId = 1,
                displayOrder = 6,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-cubes",
                titleText = "BI",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "EDI",
                subsystemId = 1,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-arrows-alt",
                titleText = "EDI",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Email Management",
                subsystemId = 1,
                displayOrder = 8,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-envelope",
                titleText = "Email Management",
            });

            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "SMS Management",
                subsystemId = 1,
                displayOrder = 9,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-comment",
                titleText = "SMS Management",
            });

            context.SaveChanges();


            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Basic Info",
                subsystemId = 1,
                displayOrder = context.Menutitles.Max(r => r.displayOrder) + 1,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-info-circle",
                titleText = "Basic Info",
            });


            context.SaveChanges();


            context.Menutitles.AddOrUpdate(r => r.titleText, new Menutitle
            {
                pageTitle = "Administration",
                subsystemId = 1,
                displayOrder = context.Menutitles.Max(r => r.displayOrder) + 1,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = null,
                titleStyle = "fa fa-lg fa-fw fa-cogs",
                titleText = "Administration",
            });


            context.SaveChanges();

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 1,
                pageTitle = "Outbound Negotiations",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Sales & Marketing").id,
                displayOrder = 1,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "SalesMarketing/Negotiation/outboundNegotiation",
                itemStyle = "fa fa-lg fa-fw fa-volume-control-phone",
                itemText = "Outbound Negotiations",
            });


            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 2,
                pageTitle = "Access Groups",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Administration").id,
                displayOrder = 1,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "Administration/Accessgroup/accessgroup",
                itemStyle = "fa fa-lg fa-fw fa-users",
                itemText = "Access Groups",
            });


            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 3,
                pageTitle = "Users",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Administration").id,
                displayOrder = 2,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "Administration/User/user",
                itemStyle = "fa fa-lg fa-fw fa-user",
                itemText = "Users",
            });

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 4,
                pageTitle = "Upload Company Logo",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Administration").id,
                displayOrder = 3,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "Administration/logoUpload",
                itemStyle = "fa fa-lg fa-fw fa-upload",
                itemText = "Upload Company Logo",
            });


            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 5,
                pageTitle = "Locations",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 1,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Location/location",
                itemStyle = "fa fa-lg fa-fw fa-globe",
                itemText = "Locations",
            });


            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 6,
                pageTitle = "Package Types",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 2,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Packagetype/packagetype",
                itemStyle = "fa fa-lg fa-fw fa-cube",
                itemText = "Package Types",
            });

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 7,
                pageTitle = "Marketers",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 3,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Marketer/marketer",
                itemStyle = "fa fa-lg fa-fw fa-male txt-color-blue",
                itemText = "Marketers",
            });

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 8,
                pageTitle = "Forwarders",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 4,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Forwarder/forwarder",
                itemStyle = "fa fa-lg fa-fw fa-male txt-color-red",
                itemText = "Forwarders",
            });

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 9,
                pageTitle = "Customers",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 5,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Customer/customer",
                itemStyle = "fa fa-lg fa-fw fa-male txt-color-green",
                itemText = "Customers",
            });

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 10,
                pageTitle = "Carriers",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 6,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Carrier/carrier",
                itemStyle = "fa fa-lg fa-fw fa-male",
                itemText = "Carriers",
            });

            context.Menuitems.AddOrUpdate(r => r.itemText, new Menuitem
            {
                id = 11,
                pageTitle = "Agents",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Basic Info").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "BasicInfo/Agent/agent",
                itemStyle = "fa fa-lg fa-fw fa-male txt-color-yellow",
                itemText = "Agents",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 12,
                pageTitle = "Inbox",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "EDI").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EDI/Inbox",
                itemStyle = "fa fa-lg fa-fw fa-inbox txt-color-yellow",
                itemText = "Inbox",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 13,
                pageTitle = "Draft",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "EDI").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EDI/Draft",
                itemStyle = "fa fa-lg fa-fw fa-pencil-square-o txt-color-yellow",
                itemText = "Draft",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 14,
                pageTitle = "Deleted Items",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "EDI").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EDI/DeletedItems",
                itemStyle = "fa fa-lg fa-fw fa-trash-o txt-color-yellow",
                itemText = "Deleted Items",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 15,
                pageTitle = "Sent Items",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "EDI").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EDI/SentItems",
                itemStyle = "fa fa-lg fa-fw fa-paper-plane-o txt-color-yellow",
                itemText = "Sent Items",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 16,
                pageTitle = "Outbox",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "EDI").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EDI/Outbox",
                itemStyle = "fa fa-lg fa-fw fa-share-square-o txt-color-yellow",
                itemText = "Outbox",
            });


            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 17,
                pageTitle = "Inbox",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Email Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EmailManagement/Inbox",
                itemStyle = "fa fa-lg fa-fw fa-inbox txt-color-yellow",
                itemText = "Inbox",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 18,
                pageTitle = "Draft",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Email Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EmailManagement/Draft",
                itemStyle = "fa fa-lg fa-fw fa-pencil-square-o txt-color-yellow",
                itemText = "Draft",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 19,
                pageTitle = "Deleted Items",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Email Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EmailManagement/DeletedItems",
                itemStyle = "fa fa-lg fa-fw fa-trash-o txt-color-yellow",
                itemText = "Deleted Items",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 20,
                pageTitle = "Sent Items",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Email Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EmailManagement/SentItems",
                itemStyle = "fa fa-lg fa-fw fa-paper-plane-o txt-color-yellow",
                itemText = "Sent Items",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 21,
                pageTitle = "Outbox",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "Email Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "EmailManagement/Outbox",
                itemStyle = "fa fa-lg fa-fw fa-share-square-o txt-color-yellow",
                itemText = "Outbox",
            });


            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 22,
                pageTitle = "Inbox",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "SMS Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "SMSManagement/Inbox",
                itemStyle = "fa fa-lg fa-fw fa-inbox txt-color-yellow",
                itemText = "Inbox",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 23,
                pageTitle = "Draft",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "SMS Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "SMSManagement/Draft",
                itemStyle = "fa fa-lg fa-fw fa-pencil-square-o txt-color-yellow",
                itemText = "Draft",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 24,
                pageTitle = "Deleted Items",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "SMS Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "SMSManagement/DeletedItems",
                itemStyle = "fa fa-lg fa-fw fa-trash-o txt-color-yellow",
                itemText = "Deleted Items",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 25,
                pageTitle = "Sent Items",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "SMS Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "SMSManagement/SentItems",
                itemStyle = "fa fa-lg fa-fw fa-paper-plane-o txt-color-yellow",
                itemText = "Sent Items",
            });

            context.Menuitems.AddOrUpdate(t => new { t.itemText, t.menutitleId }, new Menuitem
            {
                id = 26,
                pageTitle = "Outbox",
                menutitleId = context.Menutitles.FirstOrDefault(r => r.titleText == "SMS Management").id,
                displayOrder = 7,
                extraInfoStyle = null,
                hasExtraInfo = false,
                href = "SMSManagement/Outbox",
                itemStyle = "fa fa-lg fa-fw fa-share-square-o txt-color-yellow",
                itemText = "Outbox",
            });
        }

    }
}
