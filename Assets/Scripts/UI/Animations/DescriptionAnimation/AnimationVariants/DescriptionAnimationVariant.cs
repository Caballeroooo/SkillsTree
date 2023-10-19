using DG.Tweening;
using UnityEngine;

public class DescriptionAnimationVariant : MonoBehaviour
{
    [SerializeField] private DescriptionAnimationSequenceType _sequenceType;
    [SerializeField] private float _joinDelayDuration;

    public DescriptionAnimationSequenceType SequenceType => _sequenceType;
    public float JoinDelayDuration => _joinDelayDuration;

    public virtual void Initialize()
    {
    }

    public virtual Sequence Play()
    {
        return DOTween.Sequence();
    }
}