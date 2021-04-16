using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class IntroPanels : MonoBehaviour
{
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Show(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(0, 0.3f).SetDelay(delay);
    }

    public void HideForPrevious(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(rectTransform.rect.width, 0.3f).SetDelay(delay);
    }

    public void HideForNext(float delay = 0f)
    {
        rectTransform.DOAnchorPosX(rectTransform.rect.width * -1, 0.3f).SetDelay(delay);
    }

    public void InitialOffset(int i)
    {
        if(i > 0)
            rectTransform.DOAnchorPosX(rectTransform.rect.width, 0f);
        else
            rectTransform.DOAnchorPosX(0, 0f);
    }

    override public string ToString()
    {
        return gameObject.name;
    }
}
