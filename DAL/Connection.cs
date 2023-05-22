using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DAL
{
    public static class Connection
    {
        public static string GetConnectionString()
        {
            return "Data Source=SAQIB\\SAQIB;Initial Catalog=Evolve;Integrated Security=true;";
        }
    }
}
