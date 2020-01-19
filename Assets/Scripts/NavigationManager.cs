using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    private SceneManagerController sceneManagerController;

    private void Awake()
    {
        sceneManagerController = FindObjectOfType<SceneManagerController>();
    }


    public void ScanAgain()
    {
        sceneManagerController.BackToTheMainPage();
    }
}
