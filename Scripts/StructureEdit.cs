using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChangeStructuresAddon.Scripts
{
    [Serializable]
    public class StructureEdit
    {
        [XmlElement("StructurePrefabName")]
        public string StructurePrefabName = "";

        [XmlArray("BuildStateDataList"), XmlArrayItem(typeof(BuildStateData), ElementName = "BuildStateData")]
        public List<BuildStateData> BuildStateDataList = new List<BuildStateData>();
    }
}
