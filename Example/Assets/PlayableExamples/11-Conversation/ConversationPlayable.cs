using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// A behaviour that is attached to a playable
public class ConversationPlayable : PlayableBehaviour
{
	private GameObject _canvasObject;
	private Image _dialogueBoxDisplay;
	private Text _dialogTextDisplay;
	private Color _color;
	private string _textString;

	public void Initialize(GameObject canvasObject, Image dialogueBoxDisplay, Text dialogTextDisplay, Color color, string textString)
	{
		_canvasObject = canvasObject;
		_dialogueBoxDisplay = dialogueBoxDisplay;
		_dialogTextDisplay = dialogTextDisplay;
		_color = color;
		_textString = textString;
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info) 
	{
		Debug.Log ("OnBehaviourPlay");
		_canvasObject.SetActive (true);
		_dialogueBoxDisplay.color = _color;
		_dialogTextDisplay.text = _textString;
	}

	public override void OnBehaviourPause(Playable playable, FrameData info) 
	{
		Debug.Log ("OnBehaviourPause");
//		_canvasObject.SetActive (false);
	}

	public override void OnPlayableDestroy (Playable playable)
	{
		base.OnPlayableDestroy (playable);
		Debug.Log ("OnPlayableDestroy");
	}

	public override void OnGraphStop (Playable playable)
	{
		base.OnGraphStop (playable);
		Debug.Log ("OnGraphStop");
	}
}
