using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSqlDemoApp
{
    public interface IPeople
    {
         string FirstName { get; set; }
         string LastName { get; set; }

    }
}
