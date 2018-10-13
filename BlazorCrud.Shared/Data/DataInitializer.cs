using BlazorCrud.Shared.Models;
using System.Linq;

namespace BlazorCrud.Shared.Data
{
    public class DataInitializer
    {
        public static void Initialize(PatientContext patientContext, OrganizationContext organizationContext, ClaimContext claimContext, UserContext userContext)
        {
            if (patientContext.Patients.Count() == 0)
            {
                // Create new patients only if the collection is empty.
                var patients = new Patient[]{
                    new Patient { Name = "Thomas Beck", Gender = "Male", PrimaryCareProvider = "Baton Rouge General", State = "LA" },
                    new Patient { Name = "Anna Beck", Gender = "Female", PrimaryCareProvider = "Barbarossa Services", State = "MI" },
                    new Patient { Name = "Mia Beck", Gender = "Female", PrimaryCareProvider = "Hanseatic Services", State = "MI" },
                    new Patient { Name = "Suzanne Beck", Gender = "Female", PrimaryCareProvider = "Barbarossa Services", State = "MI" },
                    new Patient { Name = "Joseph Henry Tank", Gender = "Male", PrimaryCareProvider = "Capital Blue Cross", State = "GA" },
                    new Patient { Name = "Peter Machavelli", Gender = "Male", PrimaryCareProvider = "Hanseatic Services", State = "DE" },
                    new Patient { Name = "Michael Whyre", Gender = "Male", PrimaryCareProvider = "Humana Services", State = "TX" },
                    new Patient { Name = "Cynthia McDowell", Gender = "Female", PrimaryCareProvider = "Hanseatic Services", State = "NY" },
                    new Patient { Name = "Claudia Yi", Gender = "Female", PrimaryCareProvider = "Barbarossa Services", State = "WA" },
                    new Patient { Name = "Dominic Dalfresco", Gender = "Male", PrimaryCareProvider = "Blue Cross of Michigan", State = "MI" },
                    new Patient { Name = "Brian Hill", Gender = "Male", PrimaryCareProvider = "Hanseatic Services", State = "NY" },
                    new Patient { Name = "Damien Grock", Gender = "Male", PrimaryCareProvider = "Capital Blue Cross", State = "PA" },
                    new Patient { Name = "Mark Pang", Gender = "Male", PrimaryCareProvider = "Hanseatic Services", State = "OR" },
                    new Patient { Name = "Satu Heliomann", Gender = "Female", PrimaryCareProvider = "Hanseatic Services", State = "WI" },
                    new Patient { Name = "Dale Beck", Gender = "Female", PrimaryCareProvider = "Hanseatic Services", State = "NY" },
                    new Patient { Name = "Jeffrey Ricklemeister", Gender = "Male", PrimaryCareProvider = "Festivus Services", State = "NY" },
                };
                foreach (Patient p in patients)
                {
                    patientContext.Patients.Add(p);
                }
                patientContext.SaveChanges();
            }

            if (organizationContext.Organizations.Count() == 0)
            {
                // Create new organizations only if the collection is empty
                var organizations = new Organization[]
                {
                    new Organization {Name="Barabarossa Services", Type="Healthcare Provider", IsActive=true},
                    new Organization {Name="Hanseatic Services", Type="Clinical Research Sponsor", IsActive=true},
                    new Organization {Name="Aetna Health", Type="Insurance Company", IsActive=true},
                    new Organization {Name="Festivus Services", Type="Religous Institution", IsActive=true},
                    new Organization {Name="Central Michigan Services", Type="Organizational Team", IsActive=true},
                    new Organization {Name="General Hospital Inc.", Type="Hospital Department", IsActive=true},
                    new Organization {Name="NHH Services", Type="Government", IsActive=false},
                    new Organization {Name="Big Red Services", Type="Healthcare Provider", IsActive=true},
                    new Organization {Name="Pete's Apotheke", Type="Healthcare Provider", IsActive=true},
                };
                foreach (Organization o in organizations)
                {
                    organizationContext.Organizations.Add(o);
                }
                organizationContext.SaveChanges();
            }

            if (claimContext.Claims.Count() == 0)
            {
                // Create new claims only if the collection is empty
                var claims = new Claim[]
                {
                    new Claim{Patient="Brian Hill", Organization="General Hospital Inc.", Status="Active", Type="Institutional"},
                    new Claim{Patient="Cynthia McDowell", Organization="General Hospital Inc.", Status="Active", Type="Institutional"},
                    new Claim{Patient="Claudia Yi", Organization="Pete's Apotheke", Status="Active", Type="Pharmacy"},
                    new Claim{Patient="Dominic Dalfresco", Organization="Barabarossa Services", Status="Active", Type="Institutional"},
                    new Claim{Patient="Satu Heliomann", Organization="General Hospital Inc.", Status="Draft", Type="Professional"},
                    new Claim{Patient="Brian Hill", Organization="Big Red Services", Status="Active", Type="Institutional"},
                    new Claim{Patient="Anna Beck", Organization="General Hospital Inc.", Status="Active", Type="Institutional"},
                    new Claim{Patient="Joseph Henry Tank", Organization="Barabarossa Services", Status="Active", Type="Institutional"},
                    new Claim{Patient="Peter Machavelli", Organization="Barabarossa Services", Status="Entered-in-Error", Type="Professional"},
                    new Claim{Patient="Michael Whyre", Organization="General Hospital Inc.", Status="Active", Type="Institutional"},
                    new Claim{Patient="Brian Hill", Organization="General Hospital Inc.", Status="Cancelled", Type="Institutional"},
                    new Claim{Patient="Damien Grock", Organization="Big Red Services", Status="Active", Type="Institutional"},
                };
                foreach (Claim c in claims)
                {
                    claimContext.Claims.Add(c);
                }
                claimContext.SaveChanges();
            }

            if (userContext.Users.Count() == 0)
            {
                // Create new users only if the collection is empty.
                var users = new User[]
                {
                    new User {Username="admin@beckshome.com", Password="Password123", FirstName="Thomas", LastName="Beck", Email="admin@beckshome.com"},
                    new User {Username="user@beckshome.com", Password="Password123", FirstName="Anna", LastName="Beck", Email="user@beckshome.com"}
                };
                foreach (User u in users)
                {
                    userContext.Users.Add(u);
                }
                userContext.SaveChanges();
            }
        }
    }
}
