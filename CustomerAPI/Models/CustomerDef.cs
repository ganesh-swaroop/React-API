using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Models
{

    public class CustomerDef
    {
    [Key]
    [MaxLength(32)]
    public string CustomerID {get; set;}
    [MaxLength(32)]
    public string CustomerCD {get; set;}
    [MaxLength(7)]
    public string Salutation {get; set;}
    [MaxLength(128)]
    public string FirstName {get; set;}
    [MaxLength(128)]
    public string MiddleName {get; set;}
    [MaxLength(128)]
    public string LastName {get; set;}
    [MaxLength(64)]
    public string ShortName {get; set;}
    public string DateOfBirth {get; set;}
    public string AddrLine1 {get; set;}
    public string AddrLine2 {get; set;}
    public string AddrLine3 {get; set;}
    public string CountryID {get; set;}
    public string CityID {get; set;}
    public string StateID {get;set;}
    public string PostalCode {get; set;}
    [MaxLength(10)]
    public string MobileNumber {get; set;}
    [MaxLength(10)]
    public string PhoneNumber {get; set;}
    public string FaxNumber {get; set;}
    public string Email {get; set;}
    public string GooglePlus {get; set;}
    public string Facebook {get; set;}
    public string Twitter {get; set;}
    public string SPID {get; set;}
    public string AlertChannel {get; set;}
    public string IsActive {get; set;}
    public string IsSysCust {get; set;}
    public string CegidDataPost {get; set;}
    public string CreID {get; set;}
    public DateTime CreTime {get; set;}
    public string ModID {get; set;}
    public DateTime ModTime {get;set;}
    }
}
