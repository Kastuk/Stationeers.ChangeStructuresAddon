﻿using Assets.Scripts.Objects;
using Assets.Scripts.Serialization;
using Assets.Scripts.Util;
using HarmonyLib;
using System;
using System.Xml.Serialization;
using static Assets.Scripts.Objects.Structure;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.IO;
using Assets.Scripts.Inventory;
using System.Collections.Generic;
using System.Reflection;

namespace ChangeStructuresAddon.Scripts
{
	[HarmonyPatch(typeof(XmlSaveLoad))]
	public class XmlSaveLoadPatch
	{
		[HarmonyPatch("LoadThing")]
		[HarmonyPostfix]
		public static void LoadThingPostfix(ref Thing __result)
		{
			StructureEdits edits = ChangeStructuresAddon.CachedStructureEdits;

			foreach (StructureEdit edit in edits.StructureEditList)
			{
				if (__result.PrefabName != edit.StructurePrefabName)
				{
					continue;
				}

				Debug.Log("ChangeStructuresAddon: Reading StructureEdit for structure prefab:");
				Debug.Log(edit.StructurePrefabName);

				if ((__result as Structure))
				{
					Debug.Log("ChangeStructuresAddon: Found structure to edit:");
					Debug.Log(edit.StructurePrefabName);

					foreach (BuildStateData buildStateData in edit.BuildStateDataList)
					{
						int stateIdx = buildStateData.State;
						BuildState editedBuildState = (__result as Structure).BuildStates[stateIdx];
						if (editedBuildState != null)
						{
							float newEntryTime = buildStateData.ToolUseData.EntryTime;
							string newToolEntryName = buildStateData.ToolUseData.ToolEntryPrefabName;
							string newToolEntry2Name = buildStateData.ToolUseData.ToolEntry2PrefabName;
							int newEntryQuantity = buildStateData.ToolUseData.EntryQuantity;
							int newEntryQuantity2 = buildStateData.ToolUseData.EntryQuantity2;

							Debug.Log("ChangeStructuresAddon: Found buildstate:");
							Debug.Log(editedBuildState);

							Debug.Log("ChangeStructuresAddon: XML buildstate:");
							Debug.Log(newEntryTime);
							Debug.Log(newToolEntryName);
							Debug.Log(newToolEntry2Name);
							Debug.Log(newEntryQuantity);
							Debug.Log(newEntryQuantity2);

							if (newEntryTime != float.NaN)
							{
								Debug.Log("ChangeStructuresAddon: Changed EntryTime from " + editedBuildState.Tool.EntryTime + " to " + newEntryTime);
								editedBuildState.Tool.EntryTime = newEntryTime;
							}
							if (newToolEntryName != "")
							{
								Item newItem = Item.FindPrefab(newToolEntryName) as Item;
								Debug.Log("ChangeStructuresAddon: Changed ToolEntry " + editedBuildState.Tool.ToolEntry + " to " + newItem);
								editedBuildState.Tool.ToolEntry = newItem;
							}
							if (newToolEntry2Name != "")
							{
								Item newItem = Item.FindPrefab(newToolEntry2Name) as Item;
								Debug.Log("ChangeStructuresAddon: Changed ToolEntry2 " + editedBuildState.Tool.ToolEntry2 + " to " + newItem);
								editedBuildState.Tool.ToolEntry2 = newItem;
							}
							if (newEntryQuantity != -1)
							{
								Debug.Log("ChangeStructuresAddon: Changed EntryQuantity " + editedBuildState.Tool.EntryQuantity + " to " + newEntryQuantity);
								editedBuildState.Tool.EntryQuantity = newEntryQuantity;
							}
							if (newEntryQuantity2 != -1)
							{
								Debug.Log("ChangeStructuresAddon: Changed EntryQuantity2 " + editedBuildState.Tool.EntryQuantity2 + " to " + newEntryQuantity2);
								editedBuildState.Tool.EntryQuantity2 = newEntryQuantity2;
							}
						}

						(__result as Structure).BuildStates[stateIdx] = editedBuildState;
						Debug.Log("ChangeStructuresAddon: Edited structure " + edit.StructurePrefabName);
					}
				}
				else
				{
					Debug.LogError("ChangeStructuresAddon: Unable to find structure of name " + edit.StructurePrefabName);
				}
			}
		}
	}
}