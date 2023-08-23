using System.Diagnostics.CodeAnalysis;

namespace ParkingProject.Models;

public class Address
{
    public required string StreetName { get; set; }
    public required string StreetNumber { get; set; }
    public string? Apartment { get; set; }
    public required int ZipCode { get; set; }
    public required string Country { get; set; }

    [SetsRequiredMembers]
    public Address(string streetName, string streetNumber, int zipCode, string country)
    {
        StreetName = streetName;
        StreetNumber = streetNumber;
        ZipCode = zipCode;
        Country = country;
    }

    [SetsRequiredMembers]
    public Address(string streetName, string streetNumber, string apartment, int zipCode, string country)
    {
        StreetName = streetName;
        StreetNumber = streetNumber;
        Apartment = apartment;
        ZipCode = zipCode;
        Country = country;
    }
}