using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ShaderFloatControlAsset : PlayableAsset
{
	public string shaderPropertyFloatName;
	public float startValue;
	public float endValue;

	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		var playable = ScriptPlayable<ShaderFloatControlPlayable>.Create(graph);
		var shaderFloatControlPlayable = playable.GetBehaviour();
		shaderFloatControlPlayable.Initialize (shaderPropertyFloatName, startValue, endValue);
		return playable;
	}
}
