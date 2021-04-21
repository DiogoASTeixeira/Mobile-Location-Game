using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class IntroPanels : MonoBehaviour
{
    public IntroPanels instance;
    //public TMPro.TextMeshProUGUI textDebug;
    public bool isInitialPanel;
    RectTransform rectTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(isInitialPanel)
            rectTransform.DOAnchorPosX(0, 0f);
        else
            rectTransform.DOAnchorPosX(rectTransform.rect.width, 0f);
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

    }

    override public string ToString()
    {
        return gameObject.name;
    }

    public IntroPanels getInstance()
    {
        return instance;
    }
}
