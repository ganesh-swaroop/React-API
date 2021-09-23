using System;

namespace CustomerAPI.Models
{
    public class CustomerDefSO
    {
    public string CustomerID {get; set; }
    public string[] CustomerIdArr {get; set; }
    public string CustomerCD {get; set; }
    public string[] CustomerCDArr {get; set; }
    public string SPID {get; set; }
    public string[] FirstNameArr {get; set; }
    public string FirstName {get; set; }
    public string[] LastNameArr {get; set; }
    public string LastName {get; set; }
    public string Salutation {get; set; }
    public string[] SalutationArr {get; set; }

    public string CountryID {get; set; }
    public string[] CountryIdArr {get; set; }
    public string StateID {get; set; }
    public string[] StateIdArr {get; set; }
    public string CityID {get; set; }
    public string[] CityIdArr {get; set; }

    public string ModID {get; set; }
    public string[] ModIdArr {get; set; }
    public string CreID {get; set; }
    public string[] CreIdArr {get; set; }
    public string IsActive {get; set; }
    public string[] IsActiveArr {get; set; }
    public DateTime CreTime {get;set;}
    public DateTime ModTime {get; set;}

    public string DateOfBirthStart {get; set; }
    public string DateOfBirthEnd {get; set; }
    public string  SearchString {get;set;}

    public string Orderby {get; set;}

    public int StartIndex {get; set;}

    public int RecordsPerPage {get; set;}
    }
}