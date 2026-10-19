
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class TomBehaviour : MonoBehaviour
{

    NavMeshAgent m_Agent;
    public Transform[] goals = new Transform[3];

    public EventData Goal1Event;
    public EventData Goal2Event;
    public EventData Goal3Event;

    public Transform goal1;
    public Transform goal2;
    public Transform goal3;

    private EventBus _eventBus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        _eventBus = EventBus.Instance;
        _eventBus.EventStarted += OnEpisodeStarted;
    }

    private void OnEpisodeStarted(EventData data)
    {
        if (data == Goal1Event)
        {
            m_Agent.destination = goal1.position;
        }
        else if (data = Goal2Event)
        {
            m_Agent.destination = goal2.position;
        }
        else if (data == Goal3Event)
        {
            m_Agent.destination = goal3.position;
        }

    }

    private void OnDestroy()
    {
        _eventBus.EventStarted -= OnEpisodeStarted;

    }

}
