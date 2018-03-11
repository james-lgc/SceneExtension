using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;

[AddComponentMenu("Trait/Scene/(Interactable) Scene Change Trait")]
[System.Serializable]
public class InteractableSceneChangeTrait : SceneChangeTrait, IInteractCallable
{
	public void CallInteract()
	{
		Use();
	}
}