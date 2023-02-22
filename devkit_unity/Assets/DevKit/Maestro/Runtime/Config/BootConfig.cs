using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace DevKit.IOC.Config
{
    [Serializable]
    [DataContract]
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
