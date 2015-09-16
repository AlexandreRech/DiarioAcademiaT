using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Infra.Shared
{
    public class DaoException : DbException
    {

        public DaoException()
        {

        }
    }
}
