using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ClinicManagementSystem.Data
{
    static public class clsDataSettings
    {
        public static readonly string ConnectionString =
            "Server=.;Database=CMS;Integrated Security=True;TrustServerCertificate=True;";
    }
}
