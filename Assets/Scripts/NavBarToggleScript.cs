using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarToggleScript : MonoBehaviour
{
    public void HideNavBar()
    {
        GameControl.control.NavBar.SetActive(false);
    }

    public void ShowNavBar()
    {
        GameControl.control.NavBar.SetActive(true);
    }
}
