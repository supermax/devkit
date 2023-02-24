using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace DevKit.DIoC.Config
{
    public class BootConfig : ScriptableObject
    {
        public bool AutoConfig
        {
            get;
            set;
        }

        public AssemblyConfig[] Assemblies
        {
            get;
            set;
        }
    }
}
