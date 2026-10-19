using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PressInteractable : Interactable
{
    [SerializeField] private List<Transform> _interactableObjects;
    [SerializeField] private float _downDistance = 5;
    [SerializeField] private float _duration = 5;

    private bool _isInteracted = false;



    public override void Interact(InputActions input)
    {
        if (!_isInteracted)
        {
            foreach (Transform t in _interactableObjects)
            {
                t.DOMoveY(-_downDistance, _duration).SetRelative();
            }

            _isInteracted = true;
        }
        else
        {
            foreach (Transform t in _interactableObjects)
            {
                t.DOMoveY(_downDistance, _duration).SetRelative();
            }

            _isInteracted = false;
        }
    }
}


