using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;


    public static NavMeshManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _surface.UpdateNavMesh(_surface.navMeshData);
    }

    public void UpdateMesh()
    {
        _surface.UpdateNavMesh(_surface.navMeshData);
    }
}
