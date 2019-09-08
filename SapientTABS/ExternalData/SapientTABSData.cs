using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapientTABS.ExternalData
{
    public class SapientTABSData
    {
        public string Version { get { return "1.0"; } }

        public List<SapientFaction> Factions { get; private set; }

        public SapientTABSData()
        {
            Factions = new List<SapientFaction>();
        }
    }
}
