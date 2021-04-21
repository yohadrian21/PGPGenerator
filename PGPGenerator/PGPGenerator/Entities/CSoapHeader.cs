using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGPGenerator.Entities
{
    class CSoapHeader
    {
        public static M10BusinessProcess.BusinessProcessClient ClientWs = new M10BusinessProcess.BusinessProcessClient();
        public static M10BusinessProcess.SoapDataContract Soap = new M10BusinessProcess.SoapDataContract();
        public static Dictionary<string, M10BusinessProcess.ModuleDataContract[]> UserMenu;
    }
}
