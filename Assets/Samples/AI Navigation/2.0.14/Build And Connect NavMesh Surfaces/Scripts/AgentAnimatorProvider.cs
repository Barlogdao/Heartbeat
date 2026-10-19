using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentAnimatorProvider : MonoBehaviour
{
    private Animator _animator;
    NavMeshAgent agent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float speed = agent.velocity.magnitude;
        _animator.SetFloat("Speed", speed);
    }
}