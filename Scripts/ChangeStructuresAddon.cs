using Assets.Scripts.Serialization;
using Stationeers.Addons;
using Stationeers.Addons.API;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace ChangeStructuresAddon.Scripts
{
    public class UnofficialStationeersPatch : IPlugin
    {
        public void OnLoad()
        {
            Debug.Log("ChangeStructuresAddon: Loaded");
        }

        public void OnUnload()
        {
            Debug.Log("ChangeStructuresAddon: Unloaded");
        }

		private static StructureEdits _cachedStructureEdits;
		public static StructureEdits CachedStructureEdits
		{
			get
			{
				if (_cachedStructureEdits == null)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(StructureEdits));

					string xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/My Games/Stationeers/mods/UnofficialStationeersPatch/GameData/StructureEdits.xml";

					if (!File.Exists(xmlPath))
					{
						Debug.Log("ChangeStructuresAddon: ERROR: StructureEdits.xml not found");
						_cachedStructureEdits = new StructureEdits();
						return _cachedStructureEdits;
					}

					Debug.Log("ChangeStructuresAddon: Initialize Patch");
					Debug.Log("ChangeStructuresAddon: Loading StructureEdits.xml");
					_cachedStructureEdits = XmlSerialization.Deserialize(xmlSerializer, xmlPath) as StructureEdits;
					Debug.Log("ChangeStructuresAddon: Loaded StructureEdits.xml");
					Debug.Log(_cachedStructureEdits);
					return _cachedStructureEdits;
				}
				else
				{
					return _cachedStructureEdits;
				}
			}
		}
	}
}
