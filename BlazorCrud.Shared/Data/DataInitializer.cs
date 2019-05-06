using BlazorCrud.Shared.Models;
using Bogus;
using System;
using System.Linq;

namespace BlazorCrud.Shared.Data
{
    public class DataInitializer
    {
        public static void Initialize(PatientContext patientContext, OrganizationContext organizationContext, ClaimContext claimContext, UserContext userContext, UploadContext uploadContext)
        {
            Randomizer.Seed = new Random(8675309);

            if (patientContext.Patients.Count() == 0)
            {
                // Create test contacts
                var system = new[] { "Phone", "Fax", "Pager", "SMS" };
                var use = new[] { "Home", "Work", "Mobile" };
                var testContacts = new Faker<ContactPoint>()
                    .RuleFor(c => c.System, f => f.PickRandom(system))
                    .RuleFor(c => c.Value, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Use, f => f.PickRandom(use));
                
                // Create new patients only if the collection is empty.
                var gender = new[] { "Male", "Female" };
                var state = new[] { "MI", "OH", "IL", "IN" };
                var testPatients = new Faker<Patient>()
                    .RuleFor(p => p.Name, f => f.Name.FullName())
                    .RuleFor(p => p.Gender, f => f.PickRandom(gender))
                    .RuleFor(p => p.PrimaryCareProvider, f => f.Company.CompanyName())
                    .RuleFor(p => p.State, f => f.PickRandom(state))
                    .RuleFor(p => p.Contacts, f => testContacts.Generate(2).ToList());
                var patients = testPatients.Generate(200);
                
                foreach (Patient p in patients)
                {
                    patientContext.Patients.Add(p);
                }
                patientContext.SaveChanges();
            }

            if (organizationContext.Organizations.Count() == 0)
            {
                // Create new organizations only if the collection is empty
                var orgType = new[] { "Healthcare Provider", "Hospital Department", "Organizational Team", "Government", "Insurance Company"};
                var testOrganizations = new Faker<Organization>()
                    .RuleFor(o => o.Name, f => f.Company.CompanyName())
                    .RuleFor(o => o.Type, f => f.PickRandom(orgType))
                    .RuleFor(o => o.IsActive, f => f.Random.Bool());
                var organizations = testOrganizations.Generate(50);
       
                foreach (Organization o in organizations)
                {
                    organizationContext.Organizations.Add(o);
                }
                organizationContext.SaveChanges();
            }

            if (claimContext.Claims.Count() == 0)
            {
                // Create new claims only if the collection is empty
                var status = new[] { "Active", "Cancelled", "Draft" };
                var type = new[] { "Institutional", "Oral", "Pharmacy", "Professional", "Vision"};
                var testClaims = new Faker<Claim>()
                    .RuleFor(c => c.Patient, f => f.Name.FullName())
                    .RuleFor(c => c.Organization, f => f.Company.CompanyName())
                    .RuleFor(c => c.Status, f => f.PickRandom(status))
                    .RuleFor(c => c.Type, f => f.PickRandom(type));
                var claims = testClaims.Generate(500);

                foreach (Claim c in claims)
                {
                    claimContext.Claims.Add(c);
                }
                claimContext.SaveChanges();
            }

            if (userContext.Users.Count() == 0)
            {
                // Create new users only if the collection is empty.
                var testUsers = new Faker<User>()
                    .RuleFor(u => u.Username, u => u.Internet.UserName())
                    .RuleFor(u => u.Password, u => u.Internet.Password())
                    .RuleFor(u => u.FirstName, u => u.Name.FirstName())
                    .RuleFor(u => u.LastName, u => u.Name.LastName())
                    .RuleFor(u => u.Email, u => u.Internet.Email());
                var users = testUsers.Generate(20);

                users.AddRange(
                    new User[] {
                        new User { Username = "admin@beckshome.com", Password = "Password123", FirstName = "Thomas", LastName = "Beck", Email = "admin@beckshome.com" },
                        new User { Username = "user@beckshome.com", Password = "Password123", FirstName = "Anna", LastName = "Beck", Email = "user@beckshome.com" }
                });

                foreach (User u in users)
                {
                    userContext.Users.Add(u);
                }
                userContext.SaveChanges();
            }

            if (uploadContext.Uploads.Count() == 0)
            {
                // Create new uploads only if the current collection is empty.
                var testUploads = new Faker<Upload>()
                    .RuleFor(u => u.FileName, u => u.System.FileName())
                    .RuleFor(u => u.UploadTimestamp, u => u.Date.Past(1, DateTime.Now))
                    .RuleFor(u => u.ProcessedTimestamp, u => u.Date.Future(1, DateTime.Now))
                    .RuleFor(u => u.FileContent, u => u.Image.PicsumUrl(640, 480, false, false));
                var uploads = testUploads.Generate(30);

                foreach (Upload u in uploads)
                {
                    uploadContext.Uploads.Add(u);
                }
                uploadContext.SaveChanges();
            }
        }
    }
}
