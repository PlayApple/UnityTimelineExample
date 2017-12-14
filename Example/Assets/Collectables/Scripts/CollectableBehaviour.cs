using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityStandardAssets.Characters.ThirdPerson;

public class CollectableBehaviour : MonoBehaviour
{
	[Header ("Playable Director References")]
	public PlayableDirector idleTimeline;
	public PlayableDirector collectedTimeline;

	[Header ("Collider")]
	public Collider collectableCollider;

	[Header ("Player Settings")]
	public string playerTag;
	public string playerAnimationTrackName;

	private GameObject m_player;
	private ThirdPersonUserControl m_thirdPersonUserControl;
	private PlayerCutsceneSpeedController m_playerCutsceneSpeedController;


	// Use this for initialization
	void Start ()
	{
		m_player = GameObject.FindWithTag (playerTag);
		m_thirdPersonUserControl = m_player.GetComponent<ThirdPersonUserControl> ();
		m_playerCutsceneSpeedController = m_player.GetComponent<PlayerCutsceneSpeedController> ();
		BindPlayerToAnimationTrack ();
	}

	void BindPlayerToAnimationTrack ()
	{
		foreach (var playableAssetOutput in collectedTimeline.playableAsset.outputs) {
			if (playableAssetOutput.streamName == playerAnimationTrackName) {
				collectedTimeline.SetGenericBinding (playableAssetOutput.sourceObject, m_player);
			}
		}
	}

	void OnTriggerEnter (Collider theCollider)
	{
		if (theCollider.gameObject == m_player) {
			collectableCollider.enabled = false;
			SetPlayerRotationTowardsCollectable ();
			idleTimeline.Stop ();
			collectedTimeline.Play ();
			StartCoroutine (TimelineLifeCoroutine ());
		}
	}

	void SetPlayerRotationTowardsCollectable ()
	{

		Vector3 lookDirection = transform.position - m_player.transform.position;
		lookDirection.y = 0;
		m_player.transform.rotation = Quaternion.LookRotation (lookDirection);
	}

	IEnumerator TimelineLifeCoroutine ()
	{
		m_playerCutsceneSpeedController.SetPlayerSpeed ();
		m_thirdPersonUserControl.inputAllowed = false;
		yield return new WaitForSeconds ((float)collectedTimeline.duration);
		m_thirdPersonUserControl.inputAllowed = true;
		Destroy (gameObject);
	}
}
