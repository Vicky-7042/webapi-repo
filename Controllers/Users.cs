using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Mapping;
using NHibernate.Transform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Helper;
using UserApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {


        // GET: api/<Users>
        [HttpGet]
        public IList Get()
        {
            using ISession session = NHibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            {
                try
                {

                    return session.Query<User>().ToList();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }

        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //GET all values by column name from table
        [HttpGet("column/{value?}")]
        public IList GetColumnName([FromQuery] string value = null)
        {
            using ISession session = NHibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            {
                try
                {


                    if (value == null)
                    {
                        NoContent();
                    }
                    var res = session.CreateCriteria<User>()
                    .SetProjection(Projections.Distinct(Projections.ProjectionList()
                    .Add(Projections.Property(value))))
                    .List();
                    res.Remove(null);
                    return res;



                }
                catch (Exception e)
                {

                    throw;
                }
                finally
                {
                    session.Close();
                }

            }
        }

        //function returns the null count of all the column 
        [HttpGet("nullcount/{col?}/{opt?}/{val?}")]
        public Dictionary<string,int> GetNullCount([FromQuery] string col = null, [FromQuery] string opt = null, [FromQuery] string val = null)
        {

            using ISession session = NHibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            {
                try
                {
                   
                    Dictionary<string, int> cntValue = new Dictionary<string, int>();
                    var metadata = session.SessionFactory.GetClassMetadata(typeof(User));
                    foreach (var name in metadata.PropertyNames)
                    {
                        var res = session.CreateCriteria<User>()
                                .SetProjection(Projections.RowCount())
                                .Add(Restrictions.IsNull(name));
                        if (col != null && opt != null )
                        {
                   
                            if (opt == "NotNull")
                            {
                                 res.Add(Restrictions.IsNotNull(col));
                            }
                            if (opt == "Equal")
                            {
                               
                                res.Add(Restrictions.Eq(col, val));
                            }
                            if (opt == "NotEqual")
                            {
                                res.Add(Restrictions.Not(Restrictions.Eq(col, val)));
                            }
                            if(opt=="Null")
                            {
                                res.Add(Restrictions.IsNull(col));
                            }
                        }
                             var count=res.UniqueResult<int>();
                   
                        cntValue.Add( name, count);

                    }

                    return cntValue;
                }
                catch (Exception e)
                {
                    
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        [HttpGet("filter/{column?}/{option?}/{value?}")]
        public IList GetFilteredValue([FromQuery] string column = null, [FromQuery] string option = null, [FromQuery] string value = null)
        {
            using ISession session = NHibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            {
                try
                {
                    if (column != null && option != null)
                    {
                        ICriteria query = session.CreateCriteria<User>();
                        if (option == "NotNull")
                        {
                            if(column == "AllColumn")
                            {

                                var properties = new List<string>()
                                    {
                                        "FirstName",
                                        "LastName",
                                        "City",
                                        "Department",
                                        "JobTitle",
                                        "Email",
                                        "PhoneNumber",
                                        "CompanyName",
                                        "Country",
                                        "State",
                                        "PostalCode",
                                        "IsAdmin",
                                        "IsGuestUser",
                                        "FaxNumber",
                                        "SigninStatus"

                                     };
                                string str = "";
                                for (int i = 0; i < properties.Count; i++)
                                {
                                    str = str + "user." + properties[i] + " IS NOT NULL ";
                                    if (i < properties.Count - 1)
                                    {
                                        str += "and ";
                                    }
                                }
                                var sql = session.CreateQuery("SELECT user FROM User as user where " + str).List();
                                return sql;
                            }
                            return query.Add(Restrictions.IsNotNull(column)).List();
                        }
                        else if (option == "Equal")
                        {
                            return query.Add(Restrictions.Eq(column, value)).List();
                        }
                        else if (option == "NotEqual")
                        {
                            return query.Add(Restrictions.Not(Restrictions.Eq(column, value))).List();
                        }
                        else
                        {
                            return query.Add(Restrictions.IsNull(column)).List();
                        }
                    }
                    else
                    {
                        return session.Query<User>().ToList();
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                finally
                {
                    session.Close();
                }

            }
        }

        // POST api/<Users>
        [HttpPost]
        public void Post(User user)
        {
            using ISession session = NHibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            {
                try
                {

                    session.Save(user);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
