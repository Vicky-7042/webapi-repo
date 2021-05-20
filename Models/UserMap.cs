using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;

namespace UserApi.Models
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Id(x => x.Id, m =>
            {

                m.Column("id");
                m.Generator(Generators.Sequence);
            }
                 );
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.FaxNumber);
            Property(x => x.City);
            Property(x => x.CompanyName);
            Property(x => x.Country);
            Property(x => x.Department);
            Property(x => x.IsAdmin);
            Property(x => x.IsGuestUser);
            Property(x => x.JobTitle);
            Property(x => x.PhoneNumber);
            Property(x => x.PostalCode);
            Property(x => x.SigninStatus);
            Property(x => x.SigninStatus);
            Property(x => x.Email);
            Property(x => x.State);
        }
    }
}
