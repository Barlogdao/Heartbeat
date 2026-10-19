using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUi : MonoBehaviour
{
    TimelineManager _timelineManager;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;


    private void Start()
    {
        _timelineManager = TimelineManager.Instance;
        _timelineManager.EpisodeStarted += OnEpisodeStarted;
    }

    private void OnEpisodeStarted(int episode)
    {
        _text.text = episode.ToString();

        if (episode == 5)
        {
            Destroy(_text.gameObject);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _image.fillAmount = _timelineManager.GetRemainnigTimePercent;
        
    }

    private void OnDestroy()
    {
        _timelineManager.EpisodeStarted -= OnEpisodeStarted;
    }
}
