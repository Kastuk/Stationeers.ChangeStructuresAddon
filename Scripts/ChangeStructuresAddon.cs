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

		//public static string WorkshopId = "2600483974"; // I don't know what is for and how to preserve copyright.

		private static StructureEdits _cachedStructureEdits;
		public static StructureEdits CachedStructureEdits
		{
			get
			{
				if (_cachedStructureEdits == null)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(StructureEdits));

					// Try normal path for XML
					string xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/My Games/Stationeers/mods/ChangeStructuresAddon/Content/StructureEdits.xml";

					if (!File.Exists(xmlPath))
					{
						Debug.Log("ChangeStructuresAddon: WARN: StructureEdits.xml not found; creating template");
						Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/My Games/Stationeers/mods/ChangeStructuresAddon/Content/");
						xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/My Games/Stationeers/mods/ChangeStructuresAddon/Content/StructureEdits.xml";

						StructureEdits newEdits = new StructureEdits();

						ToolUseData toolData = new ToolUseData();
						toolData.EntryTime = 1f;
						toolData.ToolEntryPrefabName = "ItemWeldingTorch";
						toolData.ToolEntry2PrefabName = "ItemPlasticSheets";
						toolData.EntryQuantity = 0;
						toolData.EntryQuantity2 = 4;
                        //DECONSTRUCTION!
                        toolData.ExitTime = 1f;
                        toolData.ToolExitPrefabName = "ItemAngleGrinder";
                        toolData.ExitQuantity = 0;

						BuildStateData buildData = new BuildStateData();
						buildData.State = 1;
						buildData.ToolUseData = toolData;

						StructureEdit edit = new StructureEdit();
						edit.StructurePrefabName = "StructureInteriorDoorTriangle";
						edit.BuildStateDataList.Add(buildData);

						newEdits.StructureEditList.Add(edit);

						XmlSerialization.Serialization(xmlSerializer, newEdits, xmlPath);
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
