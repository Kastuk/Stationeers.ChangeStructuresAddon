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
	public class BuildStateData
	{
		[XmlElement("State")]
		public int State = -1;

		[XmlElement("ToolUseData")]
		public ToolUseData ToolUseData = new ToolUseData();
	}
}
