using System.Collections.Generic;
using UnityEngine;

public class CameraPoint: MonoBehaviour
{
    [SerializeField] private List<RenderTexture> _cameras;

    public List<RenderTexture> GetCameras()
    {
        return _cameras;
    }
}



