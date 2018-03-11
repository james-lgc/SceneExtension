using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SceneInstruction
{
	[SerializeField] private string sceneName;
	public string SceneName { get { return sceneName; } }

	public SceneInstruction(SceneInstruction sentInstruction)
	{
		sceneName = sentInstruction.SceneName;
	}

	public SceneInstruction(string sentName)
	{
		sceneName = sentName;
	}
}
