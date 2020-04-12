using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Client.Shared
{
    public class FormMapper
    {
        [Required(ErrorMessage = "Patient is a required field")]
        public _Patient SelectedPatient { get; set; }
        [Required(ErrorMessage = "Claim status is a required field")]
        public string Status { get; set; }
    }

    public class _Patient
    {
        public _Patient() { }
        public _Patient(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
