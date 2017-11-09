using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ConversationAsset : PlayableAsset {

	[Header("Dialogue Info")]
	public ExposedReference<GameObject> canvasObject;
	public ExposedReference<Image> dialogueBoxDisplay;
	public ExposedReference<Text> dialogueTextDisplay;


	public Color dialogueBoxColor;
	[Multiline(3)]
	public string dialogueString;
	public Sprite npcHead;

	public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
	{
		var playable = ScriptPlayable<ConversationPlayable>.Create(graph);
		var conversationPlayable = playable.GetBehaviour();

		var _canvasObject = canvasObject.Resolve(playable.GetGraph().GetResolver());
		var _dialogueBoxDisplay = dialogueBoxDisplay.Resolve (playable.GetGraph ().GetResolver ());
		var _dialogueTextDisplay = dialogueTextDisplay.Resolve (playable.GetGraph ().GetResolver ());


		conversationPlayable.Initialize (_canvasObject, _dialogueBoxDisplay, _dialogueTextDisplay, npcHead, dialogueBoxColor, dialogueString);
		return playable;
	}
}
