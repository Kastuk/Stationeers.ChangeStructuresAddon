using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChangeStructuresAddon.Scripts
{
    [XmlRoot]
    public class StructureEdits
    {
        [XmlArray("StructureEditList"), XmlArrayItem(typeof(StructureEdit), ElementName = "StructureEdit")]
        public List<StructureEdit> StructureEditList = new List<StructureEdit>();
    }
}
