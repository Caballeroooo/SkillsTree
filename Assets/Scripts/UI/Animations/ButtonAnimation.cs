using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private float _amplitude = -0.1f;
    [SerializeField] private float _durationDown = 0.1f;
    [SerializeField] private float _durationUp = 0.4f;

    private Vector3 _startScale;
    private Image _image;

    private void Awake()
    {
        _startScale = transform.localScale;
        _image = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public void Enable()
    {
        _image.raycastTarget = true;
    }

    public void Disable()
    {
        _image.raycastTarget = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Scale();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnScale();
    }

    public void OnPointerUp(PointerEventData data)
    {
        UnScale();
    }
    
    private void Scale()
    {
        transform.DOKill();
        transform.DOScale(_startScale + _amplitude * Vector3.one, _durationDown).SetEase(Ease.OutCubic);
    }

    private void UnScale()
    {
        transform.DOKill();
        transform.DOScale(_startScale, _durationUp).SetEase(Ease.OutCubic);
    }
}
