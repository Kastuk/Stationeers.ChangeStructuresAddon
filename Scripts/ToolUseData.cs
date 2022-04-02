using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Assets.Scripts.Objects.Structure;

namespace ChangeStructuresAddon.Scripts
{
	[Serializable]
	public class ToolUseData
	{
		[XmlElement("EntryTime")]
		public float EntryTime = float.NaN;

		[XmlElement("ToolEntryPrefabName")]
		public string ToolEntryPrefabName = "";

		[XmlElement("EntryQuantity")]
		public int EntryQuantity = -1;

		[XmlElement("ToolEntry2PrefabName")]
		public string ToolEntry2PrefabName = "";

		[XmlElement("EntryQuantity2")]
		public int EntryQuantity2 = -1;

        //DECONSTRUCTION!

        [XmlElement("ExitTime")]
		public float ExitTime = float.NaN;
		
        [XmlElement("ToolExitPrefabName")]
		public string ToolExitPrefabName = "";

		[XmlElement("ExitQuantity")]
		public int ExitQuantity = -1;

	}
}
