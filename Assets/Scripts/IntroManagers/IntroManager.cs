using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class IntroManager : MonoBehaviour
{
    RectTransform rectTransform;

    private Transform[] allChildren;
    private IntroPanels[] Panels;
    private int index = 0;

    #region Getter
    static IntroManager instance;
    public static IntroManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<IntroManager>();
            if (instance == null)
                Debug.LogError("HomeUIManager not found");
            return instance;
        }
    }
    #endregion Getter

    private void Start()
    {
        Panels = new IntroPanels[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
                Panels[i] = child.gameObject.GetComponent<IntroPanels>();
                Panels[i].InitialOffset(i);
                i++;
        }


    }

    public void ShowPreviousMenu()
    {
        if (index - 1 >= 0 && Panels[index - 1] != null)
        {
            Panels[index].HideForPrevious();
            Panels[index - 1].Show();
            index--;
        }
    }

    public void ShowNextScreen()
    {
        if (index + 1 < Panels.Length && Panels[index + 1] != null)
        {
            Panels[index].HideForNext();
            Panels[index + 1].Show();
            index++;
        }
    }
}