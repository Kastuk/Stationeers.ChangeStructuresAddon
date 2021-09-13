using Assets.Scripts;
using Assets.Scripts.Serialization;
using Stationeers.Addons;
using Stationeers.Addons.API;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace ChangeStructuresAddon.Scripts
{
    public class ChangeStructuresAddon : IPlugin
    {
        public void OnLoad()
        {
            Debug.Log("ChangeStructuresAddon: Loaded");
        }

        public void OnUnload()
        {
            Debug.Log("ChangeStructuresAddon: Unloaded");
        }

		public static string WorkshopId = "2600483974";

		private static StructureEdits _cachedStructureEdits;
		public static StructureEdits CachedStructureEdits
		{
			get
			{
				if (_cachedStructureEdits == null)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(StructureEdits));

					// Try workshop path for xml first
					string xmlPath = GameManager.SteamAppPath + "/../../workshop/content/544550/" + WorkshopId + "/GameData/StructureEdits.xml";
					// Try local mod path for xml
					if (!File.Exists(xmlPath))
						xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/My Games/Stationeers/mods/ChangeStructuresAddon/GameData/StructureEdits.xml";
					// Try local mod path with workshopId for xml
					if (!File.Exists(xmlPath))
						xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/My Games/Stationeers/mods/" + WorkshopId + "/GameData/StructureEdits.xml";
					// Try server mod path for xml
					if (!File.Exists(xmlPath))
						xmlPath = GameManager.SteamAppPath + "/mods/" + WorkshopId + "/GameData/StructureEdits.xml";
					// Try local server mod path for xml
					if (!File.Exists(xmlPath))
						xmlPath = GameManager.SteamAppPath + "/mods/ChangeStructuresAddon/GameData/StructureEdits.xml";

					if (!File.Exists(xmlPath))
					{
						Debug.Log("ChangeStructuresAddon: ERROR: StructureEdits.xml not found");
						_cachedStructureEdits = new StructureEdits();
						return _cachedStructureEdits;
					}

					//Debug.Log("ChangeStructuresAddon: Initialize Patch");
					Debug.Log("ChangeStructuresAddon: Loading " + xmlPath);
					_cachedStructureEdits = XmlSerialization.Deserialize(xmlSerializer, xmlPath) as StructureEdits;

					if (_cachedStructureEdits != null)
						Debug.Log("ChangeStructuresAddon: Loaded " + xmlPath);

					//Debug.Log(_cachedStructureEdits);
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
