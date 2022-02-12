namespace Blazorcrud.Shared.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; } = default!;
        public string City {get; set;} = default!;
        public string State {get; set;} = default!;
        public string ZipCode {get; set;} = default!;
    }
}