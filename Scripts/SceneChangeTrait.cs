using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DSA.Extensions.Base;

[RequireComponent(typeof(TraitedMonoBehaviour))]
[System.Serializable]
public class SceneChangeTrait : TraitBase, ISendable<string>
{
	public override ExtensionEnum Extension { get { return ExtensionEnum.Scene; } }

	public Action<string> SendAction { get; set; }

	[SerializeField] private string data;
	public string Data { get { return data; } protected set { data = value; } }

	protected void Use()
	{
		if (!GetIsExtensionLoaded() || SendAction == null || Data == null) { return; }
		SendAction(Data);
	}
}
