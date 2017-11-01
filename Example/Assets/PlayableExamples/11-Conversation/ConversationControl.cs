using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ConversationControl : MonoBehaviour {
	
	public PlayableDirector director;

	private bool m_isWaitingForInput;
	private int m_Step = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isWaitingForInput && Input.GetMouseButtonDown(0)) {
			m_isWaitingForInput = false;
			director.Play ();
		}
	}

	public void WaitForUserContinue(int step)
	{
		if (m_Step + 1 != step) {
			return;
		}

		director.Pause ();
		m_Step = step;
		m_isWaitingForInput = true;
	}
}
