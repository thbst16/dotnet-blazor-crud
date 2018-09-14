using BlazorCrud.Shared;
using System.Linq;

namespace BlazorCrud.Shared.Data
{
    public class PatientInitializer
    {
        public static void Initialize(PatientContext context)
        {
            if (context.Patients.Count() == 0)
            {
                // Create a new Patient object if collection is empty, which means you can't delete all Patients.
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
                        new Patient { Name = "Dale Beck", Gender = "Female", PrimaryCareProvider = "Hanseatic Services", State = "NY" }
                };
                foreach (Patient p in patients)
                {
                    context.Patients.Add(p);
                }
                context.SaveChanges();
            }
        }
    }
}
