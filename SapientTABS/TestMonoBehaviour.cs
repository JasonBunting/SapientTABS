using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SapientTABS
{
    public class TestMonoBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            System.IO.File.WriteAllText(Application.dataPath + "/testmonobehavior.txt", "This is a test, yo.");
        }
    }
}
