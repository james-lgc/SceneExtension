using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using DSA.Extensions.Base;

[System.Serializable]
public class SceneParameter : TraitedMonoBehaviour, ISendable<SceneParameter>
{
	public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.Scene; } }

	[SerializeField] private string parameterName;
	public string ParameterName { get { return parameterName; } }

	[SerializeField] private bool useLockedDoors;
	public bool SendLockedDoors { get { return useLockedDoors; } }
	[SerializeField] private bool useCoverCanvas;
	public bool UseCoverCanvas { get { return useCoverCanvas; } }

	public Action<SceneParameter> SendAction { get; set; }

	[UnityEditor.MenuItem("GameObject/SceneParameter/Parameter", false, 10)]
	public static void CreateTrait(UnityEditor.MenuCommand menuCommand)
	{
		string name = "SceneParameter (0)";
		GameObject instance = new GameObject(name);
		GameObjectUtility.SetParentAndAlign(instance, menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name);
		Selection.activeObject = instance;
		instance.AddComponent<SceneParameter>();
	}
}

