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
	private Sprite _npcHead;

	public void Initialize(GameObject canvasObject, Image dialogueBoxDisplay, Text dialogTextDisplay, Sprite npcHead, Color color, string textString)
	{
		_canvasObject = canvasObject;
		_dialogueBoxDisplay = dialogueBoxDisplay;
		_dialogTextDisplay = dialogTextDisplay;
		_color = color;
		_textString = textString;
		_npcHead = npcHead;
	}

	public override void OnBehaviourPlay(Playable playable, FrameData info) 
	{
		_canvasObject.SetActive (true);
		_dialogTextDisplay.color = _color;
		_dialogTextDisplay.text = _textString;
		_dialogueBoxDisplay.sprite = _npcHead;
	}

	public override void OnBehaviourPause (Playable playable, FrameData info)
	{
		_canvasObject.SetActive (false);
	}
}
