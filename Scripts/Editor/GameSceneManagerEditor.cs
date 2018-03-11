using UnityEngine;
using System.Collections;
using UnityEditor;
using DSA.Extensions.Scenes;
using DSA.Extensions.Base;

[CustomEditor(typeof(GameSceneManager))]
public class GameSceneManagerEditor : Editor
{
	//GUI Button to store current scene name
	public override void OnInspectorGUI()
	{
		GameSceneManager gameSceneManager = (GameSceneManager)target;
		if (GUILayout.Button("StoreScene"))
		{
			gameSceneManager.StoreSceneName();
		}
		DrawDefaultInspector();
	}
}
