using BlazorCrud.Shared;
using System.Linq;

namespace BlazorCrud.Server.DataAccess
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
                        new Patient { Name = "Anna Beck", Gender = "Female", PrimaryCareProvider = "Barbarossa Services", State = "MI" }
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
