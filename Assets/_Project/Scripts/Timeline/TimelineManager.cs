using System;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private float _episodeDuration = 1;
    [SerializeField] private EventBus _eventBus;

    [SerializeField] private EventData _episode1;
    [SerializeField] private EventData _episode2;
    [SerializeField] private EventData _episode3;
    [SerializeField] private EventData _episode4;

    const int EpisodeCount = 4;

    private float _timeCounter = 1f;

    public static TimelineManager Instance;

    public event Action<int> EpisodeStarted;
    private int _currentEpisode;

    public int GetCuttentEpisode => _currentEpisode;
    public float GetRemainnigTimePercent => _timeCounter/_episodeDuration;

    private void Awake()
    {
        Instance = this;
        _timeCounter = _episodeDuration;
        _currentEpisode = 1;
    }

    private void Start()
    {

        _eventBus.AddEvent(GetCurrentEpisode());
        EpisodeStarted?.Invoke(_currentEpisode); ;
    }

    private void Update()
    {
        if (_currentEpisode > EpisodeCount)
            return;

        _timeCounter -= Time.deltaTime;

        if (_timeCounter <= 0f)
        {
            

            _timeCounter = _episodeDuration;
            _currentEpisode++;


            _eventBus.AddEvent(GetCurrentEpisode());
            EpisodeStarted?.Invoke(_currentEpisode); ;
        }
    }

    private EventData GetCurrentEpisode()
    {
        return _currentEpisode switch
        {
            1 => _episode1,
            2 => _episode2,
            3 => _episode3,
            4 => _episode4,
        };
    }

}
