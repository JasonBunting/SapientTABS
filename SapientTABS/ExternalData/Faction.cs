using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapientTABS.ExternalData
{
    [Serializable]
    public class Faction
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        //PinaCollada.GetSprite("monkyicon") - returns a sprite

    }
}
