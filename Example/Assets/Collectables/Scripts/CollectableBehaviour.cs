using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.ThirdPerson;

public class CollectableBehaviour : MonoBehaviour {
	[Header("Playable Director References")]
	public PlayableDirector idleTimeline;
	public PlayableDirector collectedTimeline;

	[Header("Collider")]
	public Collider collectableCollider;

	[Header("Player Settings")]
	public string playerTag;
	private GameObject player;
	private ThirdPersonUserControl thirdPersonUserControl;
	private PlayerCutsceneSpeedController playerCutsceneSpeedController;
	public string playerAnimationTrackName;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag (playerTag);
		thirdPersonUserControl = player.GetComponent<ThirdPersonUserControl> ();
		playerCutsceneSpeedController = player.GetComponent<PlayerCutsceneSpeedController> ();
		BindPlayerToAnimationTrack ();
	}
	
	void BindPlayerToAnimationTrack(){
		//Loop through timeline tracks and find the desired track to bind the player to
		foreach (var playableAssetOutput in collectedTimeline.playableAsset.outputs) {
			if (playableAssetOutput.streamName == playerAnimationTrackName) {
				collectedTimeline.SetGenericBinding (playableAssetOutput.sourceObject, player);
			}
		}
	}

	void OnTriggerEnter(Collider theCollider){
		if (theCollider.gameObject == player) {
			collectableCollider.enabled = false;
			SetPlayerRotationTowardsCollectable ();
			idleTimeline.Stop ();
			collectedTimeline.Play ();
			StartCoroutine(TimelineLifeCoroutine());
		}
	}

	void SetPlayerRotationTowardsCollectable(){

		Vector3 lookDirection = transform.position - player.transform.position;
		lookDirection.y = 0;
		player.transform.rotation = Quaternion.LookRotation (lookDirection);
	}

	IEnumerator TimelineLifeCoroutine(){
		playerCutsceneSpeedController.SetPlayerSpeed ();
		thirdPersonUserControl.inputAllowed = false;
		yield return new WaitForSeconds((float)collectedTimeline.duration);
		thirdPersonUserControl.inputAllowed = true;
		Destroy (gameObject);
	}
}
