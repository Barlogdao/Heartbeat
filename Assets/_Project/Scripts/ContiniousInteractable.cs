using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ContiniousInteractable : Interactable
{
    [SerializeField] private float duration = 5;
    [SerializeField] private float _rewindDelay = 1;
    public ContiniousStopType StopType = ContiniousStopType.StayIfUnpressed;
    public ContiniousEndBehaviour EndType = ContiniousEndBehaviour.SwitchDirection;

    public Transform target;
    public Transform destination;
    public Vector3 startPosition;
    private Coroutine _moveRoutine;
    private Tween _currentTween;

    protected override void OnAwake()
    {
        startPosition = target.position;
    }

    public override void Interact(InputActions input)
    {
        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
            _moveRoutine = null;
        }

        _moveRoutine = StartCoroutine(ContinuousInteract(input));
    }

    private IEnumerator ContinuousInteract(InputActions input)
    {
        float totalDistance = (destination.position - startPosition).magnitude;
        if (totalDistance < 0.001f) yield break;

        // Запускаем твин вперёд
        MoveTowards(destination.position, startPosition, totalDistance);

        // Ждём, пока кнопка зажата или твин не закончится
        while (input.Player.Interact.IsPressed() && _currentTween != null && _currentTween.IsActive() && _currentTween.IsPlaying())
        {
            yield return null;
        }

        // Если твин ещё идёт — кнопку отпустили
        if (_currentTween != null && _currentTween.IsActive() && _currentTween.IsPlaying())
        {
            _currentTween.Kill();

            if (StopType == ContiniousStopType.BackIfUnpressed)
            {
                yield return new WaitForSeconds(_rewindDelay);
                MoveTowards(startPosition, destination.position, totalDistance);

                // Ждём завершения возврата
                while (_currentTween != null && _currentTween.IsActive() && _currentTween.IsPlaying())
                    yield return null;
            }
        }

        // Проверяем, достигли ли цели
        if ((target.position - destination.position).sqrMagnitude < 0.001f)
        {
            if (EndType == ContiniousEndBehaviour.SwitchDirection)
            {
                NavMeshManager.Instance.UpdateMesh();
                (startPosition, destination.position) = (destination.position, startPosition);
            }
        }

        _currentTween = null;
        _moveRoutine = null;
    }

    private void MoveTowards(Vector3 goal, Vector3 fullPathStart, float totalDistance)
    {
        if (_currentTween != null && _currentTween.IsActive())
            _currentTween.Kill();

        float remainingDistance = (goal - target.position).magnitude;
        float moveDuration = duration * (remainingDistance / totalDistance);

        _currentTween = target.DOMove(goal, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => target.position = goal);
    }
}


public enum ContiniousStopType
{
    BackIfUnpressed,
    StayIfUnpressed,
}

public enum ContiniousEndBehaviour
{
    SwitchDirection,
    Stay
}


