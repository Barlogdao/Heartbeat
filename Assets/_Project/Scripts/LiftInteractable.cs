using DG.Tweening;
using UnityEngine;

public class LiftInteractable : Interactable
{
    public Transform Lift;
    public Transform EndPoint;
    public float Duration = 5f;
    public Ease Ease;
    private Vector3 _startPosition;
    private Vector3 _endPosition;


    protected override void OnAwake()
    {
        base.OnAwake();
        _startPosition = Lift.position;
        _endPosition = EndPoint.position;
    }


    public override void Interact(InputActions input)
    {
        if (IsBusy) return;

        IsBusy = true;

        Lift.DOMove(_endPosition, Duration).SetEase(Ease).SetUpdate(UpdateType.Fixed)
            .OnComplete(
            () => { IsBusy = false; ChangeDirection(); });
    }

    private void ChangeDirection()
    {
       (_startPosition, _endPosition) = (_endPosition, _startPosition);
    }
}
