using NUnit.Framework;
using System;
using UnityEngine;

public class SwitchCameraInteractable : Interactable
{


    [SerializeField] private RenderTexture cameraRenderTexture1;
    [SerializeField] private RenderTexture cameraRenderTexture2;

    private bool _isInteracted;

    [SerializeField] private Renderer _TV;
    private Material material;
    protected override void OnAwake()
    {

       material = _TV.material;

        material.SetTexture("_BaseMap", cameraRenderTexture2);

    }

    public override void Interact(InputActions input)
    {
        if (!_isInteracted)
        {
            material.SetTexture("_BaseMap", cameraRenderTexture1);
            _isInteracted = true;
        }
        else
        {
            material.SetTexture("_BaseMap", cameraRenderTexture2);
            _isInteracted = false;
        }
    }
}



