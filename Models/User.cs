using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Models
{
    public class User
    {
        public virtual int  Id {get; set;}
        public virtual string FirstName {get; set;}
        public virtual string LastName {get; set;}
        public virtual string City {get; set;}
        public virtual string Department {get; set;}
        public virtual string JobTitle{get; set;}
        public virtual string Email{get; set;}
        public virtual string PhoneNumber {get; set;}
        public virtual string CompanyName{get; set;}
        public virtual string Country{get; set;}
        public virtual string State {get; set;}
        public virtual string PostalCode {get; set;}
        public virtual string IsAdmin {get; set;}
        public virtual string IsGuestUser {get; set;}
        public virtual string FaxNumber{get; set;}
        public virtual string SigninStatus {get; set;}
        

}
}
