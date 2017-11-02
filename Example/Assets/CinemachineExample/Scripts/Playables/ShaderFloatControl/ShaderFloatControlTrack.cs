using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackColor(1f, 0f, 0f)]
[TrackClipType(typeof(ShaderFloatControlAsset))]
[TrackBindingType(typeof(Material))]
public class ShaderFloatControlTrack : TrackAsset {

	public override Playable CreateTrackMixer (PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<ShaderFloatControlPlayable>.Create(graph, inputCount);
	}
}
