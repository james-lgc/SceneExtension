using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.Scenes
{
	[SerializeField]
	public class SceneInformation : TraitedMonoBehaviour
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.Scene; } }

		private SceneParameter[] sceneParameters;
		public SceneParameter[] SceneParameters
		{
			get
			{
				if (sceneParameters == null) { SetSceneParameters(); }
				return sceneParameters;
			}
		}
		public void SetSceneParameters()
		{
			sceneParameters = GetComponentsInChildren<SceneParameter>();
		}

		public void Assign()
		{
			SetSceneParameters();
			for (int i = 0; i < SceneParameters.Length; i++)
			{
				for (int j = 0; j < SceneParameters[i].GetArray().Length; j++)
				{
					if (SceneParameters[i].GetArray()[j] is IConditional)
					{
						IConditional conditional = (IConditional)SceneParameters[i].GetArray()[j];
						if (conditional.GetIsConditionMet())
						{
							SceneParameters[i].CallInSequence(0);
							continue;
						}
					}
				}
			}
		}
	}
}