using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float hoverScaleMultiplier = 1.1f;  // Adjust the multiplier for the desired scaling effect

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScaleMultiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}

