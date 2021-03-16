using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class LocationPermission : MonoBehaviour
{
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (Input.location.isEnabledByUser)
            {
                SceneManager.LoadScene("Outside");
            }
        }
    }

    public void RequestPermission()
    {
        Permission.RequestUserPermission(Permission.FineLocation);
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (Input.location.isEnabledByUser)
            {
                SceneManager.LoadScene("Outside");
            }
        }
    }
}
