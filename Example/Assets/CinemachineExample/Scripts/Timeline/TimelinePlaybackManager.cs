using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.ThirdPerson;

public class TimelinePlaybackManager : MonoBehaviour {

	[Header("Timeline References")]
	public PlayableDirector playableDirector;

	[Header("Timeline Settings")]
	public bool playTimelineOnlyOnce;

	[Header("Player Input Settings")]
	public KeyCode interactKey;
	public bool disablePlayerInput = false;
	private ThirdPersonUserControl m_inputController;

	[Header("Player Timeline Position")]
	public bool setPlayerTimelinePosition = false;
	public Transform playerTimelinePosition;

	[Header("Trigger Zone Settings")]
	public GameObject triggerZoneObject;

	[Header("UI Interact Settings")]
	public bool displayUI;
	public GameObject interactDisplay;

	[Header("Player Settings")]
	public string playerTag = "Player";
	private GameObject m_playerObject;
	private PlayerCutsceneSpeedController m_playerCutsceneSpeedController;

	private bool playerInZone = false;
	private bool timelinePlaying = false;
	private float timelineDuration;

	// Use this for initialization
	void Start () {
		m_playerObject = GameObject.FindWithTag (playerTag);
		m_inputController = m_playerObject.GetComponent<ThirdPersonUserControl> ();
		m_playerCutsceneSpeedController = m_playerObject.GetComponent<PlayerCutsceneSpeedController> ();
		ToggleInteractUI (false);
	}

	public void PlayerEnteredZone(){
		playerInZone = true;
		ToggleInteractUI (playerInZone);
	}

	public void PlayerExitedZone(){
		playerInZone = false;
		ToggleInteractUI (playerInZone);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInZone && !timelinePlaying) {

			var activateTimelineInput = Input.GetKey (interactKey);

			if (interactKey == KeyCode.None) {
				PlayTimeline ();
			} else {
				if (activateTimelineInput) {
					PlayTimeline ();
					ToggleInteractUI (false);
				}
			}

		}
	}

	public void PlayTimeline(){
		if (setPlayerTimelinePosition) {
			SetPlayerToTimelinePosition ();
		}

		if (playableDirector) {
			playableDirector.Play ();
		}

		triggerZoneObject.SetActive (false);
		timelinePlaying = true;
		StartCoroutine (WaitForTimelineToFinish());
	}

	IEnumerator WaitForTimelineToFinish(){

		timelineDuration = (float)playableDirector.duration;

		ToggleInput (false);
		yield return new WaitForSeconds(timelineDuration);
		ToggleInput (true);


		if (!playTimelineOnlyOnce) {
			triggerZoneObject.SetActive (true);
		} else if (playTimelineOnlyOnce) {
			playerInZone = false;
		}

		timelinePlaying = false;

	}

	void ToggleInput(bool newState){
		if (disablePlayerInput){
			m_playerCutsceneSpeedController.SetPlayerSpeed ();
			m_inputController.inputAllowed = newState;
		}
	}


	void ToggleInteractUI(bool newState){
		if (displayUI) {
			interactDisplay.SetActive (newState);
		}
	}

	void SetPlayerToTimelinePosition(){
		m_playerObject.transform.position = playerTimelinePosition.position;
		m_playerObject.transform.localRotation = playerTimelinePosition.rotation;
	}
}
