namespace AutomatedTellerMachine.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using AutomatedTellerMachine.Models;
    using AutomatedTellerMachine.Services;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedTellerMachine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AutomatedTellerMachine.Models.ApplicationDbContext";
        }

        protected override void Seed(AutomatedTellerMachine.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if(!context.Users.Any(t=>t.UserName == "admin@gmail.com"))
            {
                var user = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                userManager.Create(user, "Abcd1234!");
                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin", "user", user.Id, 1000);
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Admin");
            }

            context.Transactions.Add(new Transaction { Amount = 75, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 25, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 3213.89m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 32.54m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -45, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -377, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 24.78m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 145, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 278, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -255, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -1.57m, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = 2478, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -600, CheckingAccountId = 5 });
            context.Transactions.Add(new Transaction { Amount = -.07m, CheckingAccountId = 5 });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
