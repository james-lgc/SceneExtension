using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Reflection;
using DSA.Extensions.Base;

namespace DSA.Extensions.Scenes
{
	[System.Serializable]
	public class GameSceneManager : CanvasedManagerBase<BlackOutCanvas>
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Scene; } }

		[SerializeField] private string[] sceneNames;
		public string[] SceneNames { get { return sceneNames; } }

		private SceneInformation sceneInfo;
		public SceneInformation SceneInfo { get { return sceneInfo; } }

		private string sceneToSearch;
		public string SceneToSearch { get { return sceneToSearch; } }

		private string sceneToLoad;
		public string SceneToLoad { get { return sceneToLoad; } }

		private SceneParameter currentParameter;
		public SceneParameter CurrentParameter { get { return currentParameter; } }

		private SceneInstruction currentSceneInstruction;

		private TransformValue spawnTransformValue;
		public TransformValue SpawnTransformValue { get { return spawnTransformValue; } }

		public override void Initialize()
		{
			base.Initialize();
		}

		private void RaiseOnLoadEvent(Scene scene, LoadSceneMode loadSceneMode)
		{
			Load();
			base.RaiseOnLoadEvent();
		}

		public override void PassDelegatesToTraits(TraitedMonoBehaviour sentObj)
		{
			SetTraitActions<SceneChangeTrait, string>(sentObj, ChangeScene);
		}

		private void SetActiveSceneInfo()
		{
			SceneInformation tempInfo = FindObjectOfType<SceneInformation>();
			sceneInfo = tempInfo;
			RaiseTraitsFound(sceneInfo);
			for (int i = 0; i < sceneInfo.SceneParameters.Length; i++)
			{
				RaiseTraitsFound(sceneInfo.SceneParameters[i]);
			}
			sceneInfo.Assign();
		}

		public override void LoadAtGameStart()
		{
			//SetActiveSceneInfo();
			QueueProecess();
		}

		public void ReceiveSceneInstruction(SceneInstruction sentInstruction)
		{
			sceneToLoad = sentInstruction.SceneName;
			currentSceneInstruction = sentInstruction;
			QueueProecess();
			return;
		}

		public SceneInstruction GetActiveSceneInstruction(bool sentBool)
		{
			return currentSceneInstruction;
		}

		public void ChangeScene(string sentName)
		{
			sceneToLoad = sentName;
			QueueProecess();
		}

		protected override void StartProcess()
		{
			base.StartProcess();
			StartCoroutine(WaitForProcessEnd());
		}

		private IEnumerator WaitForProcessEnd()
		{
			string oldSceneName = SceneManager.GetActiveScene().name;
			yield return new WaitForEndOfFrame();
			AsyncOperation aSyncUnload = SceneManager.UnloadSceneAsync(oldSceneName);
			while (aSyncUnload.isDone == false) { yield return null; }
			if (string.IsNullOrEmpty(sceneToLoad))
			{
				sceneToLoad = oldSceneName;
			}
			AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
			while (aSyncLoad.isDone == false) { yield return null; }
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
			RaiseOnLoadEvent();
			SetActiveSceneInfo();
			EndProcess();
		}

		public override void EndProcess()
		{
			base.EndProcess();
		}

		public void SetSpawnTransform(Transform sentTrans)
		{
			if (sentTrans == null)
			{
				spawnTransformValue = default(TransformValue);
				return;
			}
			spawnTransformValue = new TransformValue(sentTrans);
		}

		public override void AddDataToArrayList(ArrayList sentArrayList)
		{
			SceneInstruction instruction = new SceneInstruction(SceneManager.GetActiveScene().name);
			sentArrayList.Add(instruction);
		}

		public override void ProcessArrayList(ArrayList sentArrayList)
		{
			for (int i = 0; i < sentArrayList.Count; i++)
			{
				if (sentArrayList[i] is SceneInstruction)
				{
					SceneInstruction instruction = (SceneInstruction)sentArrayList[i];
					ChangeScene(instruction.SceneName);
				}
			}
		}

		public void StoreSceneName()
		{
			List<string> tempList = new List<string>();
			if (sceneNames == null)
			{
				sceneNames = new string[0];
			}
			if (sceneNames.Length > 0)
			{
				for (int i = 0; i < sceneNames.Length; i++)
				{
					if (sceneNames[i] == SceneManager.GetActiveScene().name)
					{
						return;
					}
					tempList.Add(sceneNames[i]);
				}
			}
			tempList.Add(SceneManager.GetActiveScene().name);
			tempList.Sort();
			sceneNames = new string[tempList.Count];
			for (int i = 0; i < tempList.Count; i++)
			{
				sceneNames[i] = tempList[i];
			}
		}
	}
}
