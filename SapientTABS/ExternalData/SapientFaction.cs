using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace SapientTABS.ExternalData
{
    [Serializable]
    public class SapientFaction
    {
        public string Name { get; set; }

        public string SpriteIconName { get; set; }

        public SapientFaction(string name, string spriteIconName)
        {
            Name = name;
            SpriteIconName = spriteIconName;
        }        

        public SapientFaction() { }
    }
}
