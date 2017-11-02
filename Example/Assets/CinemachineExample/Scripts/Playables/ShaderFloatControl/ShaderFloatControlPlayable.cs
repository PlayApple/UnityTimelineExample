using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class ShaderFloatControlPlayable : PlayableBehaviour
{
	private string m_shaderPropertyFloatName;
	private float m_startValue;
	private float m_endValue;

	public void Initialize(string shaderPropertyFloatName, float startValue, float endValue)
	{
		m_shaderPropertyFloatName = shaderPropertyFloatName;
		m_startValue = startValue;
		m_endValue = endValue;
	}

	public override void ProcessFrame (Playable playable, FrameData info, object playerData)
	{
		var bindedMaterial = playerData as Material;
		if (bindedMaterial != null) {
			float currentFloatValue = Mathf.Lerp (m_startValue, m_endValue, (float)(playable.GetTime () / playable.GetDuration ()));
			bindedMaterial.SetFloat (m_shaderPropertyFloatName, currentFloatValue);
		}
	}
}
