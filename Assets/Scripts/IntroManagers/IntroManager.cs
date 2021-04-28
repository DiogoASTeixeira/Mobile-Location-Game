using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class IntroManager : MonoBehaviour
{
    public RectTransform[] PanelsArray;
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
        Panels = new IntroPanels[PanelsArray.Length];
        for( int i = 0; i < PanelsArray.Length; i++)
              {
                  Panels[i] = PanelsArray[i].gameObject.GetComponent<IntroPanels>().getInstance();
              }
    }

    public void ShowPreviousPanel()
    {
        if (index - 1 >= 0 && Panels[index - 1] != null)
        {
            Panels[index].HideForPrevious();
            Panels[index - 1].Show();
            index--;
        }
    }

    public void ShowNextPanel()
    {
        if (index + 1 < Panels.Length && Panels[index + 1] != null)
        {
            Panels[index].HideForNext();
            Panels[index + 1].Show();
            index++;
        }
    }

    private int getCurrentPanelIndex()
    {
        return index;
    }
}