﻿using UnityEngine;
using UnityEngine.Playables;

public class LerpMoveObjectPlayable : PlayableBehaviour
{
    private GameObject _gameObject;
    private Transform _lerpMoveTo;

    Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private Vector3 _originalScale;

    public void Initialize(GameObject gameObject, Transform lerpMoveTo)
    {
        _gameObject = gameObject;
        _lerpMoveTo = lerpMoveTo;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) 
    {
        if (playable.GetTime() <= 0 || _lerpMoveTo == null || _gameObject == null)
            return;
        
        _gameObject.transform.position = Vector3.Lerp (_originalPosition, _lerpMoveTo.position, (float)(playable.GetTime() / playable.GetDuration()));
        _gameObject.transform.rotation = Quaternion.Lerp (_originalRotation, _lerpMoveTo.rotation, (float)(playable.GetTime() / playable.GetDuration()));
        _gameObject.transform.localScale = Vector3.Lerp (_originalScale, _lerpMoveTo.localScale, (float)(playable.GetTime() / playable.GetDuration()));
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (_gameObject != null)
        {
            _originalPosition = _gameObject.transform.position;
            _originalRotation = _gameObject.transform.rotation;
            _originalScale = _gameObject.transform.localScale;
        }
    }
    public override void OnBehaviourPause(Playable playable, FrameData info) 
    {
        if (_gameObject != null)
        {
            _originalPosition = _gameObject.transform.position;
            _originalRotation = _gameObject.transform.rotation;
            _originalScale = _gameObject.transform.localScale;
        }
    }
}
