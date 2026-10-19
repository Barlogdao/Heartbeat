using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 3f;
    [field: SerializeField] public float RunSpeed { get; private set; } = 3f;
    [field: SerializeField] public float JumpHeight { get; private set; } = 2f;

    [field: SerializeField] public float MouseSensitivity { get; private set; } = 2f;
}
