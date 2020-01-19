using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    private SceneManagerController sceneManagerController;
    public Button Explore;
    public Button Info;

    private void Awake()
    {
        sceneManagerController = FindObjectOfType<SceneManagerController>();
    }

    private void Update()
    {
        if (sceneManagerController.IsObjectDetected)
        {
            Explore.interactable = true;
            Info.interactable = true;
        }
        else
        {
            Explore.interactable = false;
            Info.interactable = false;
        }
    }

    public void ScanAgain()
    {
        sceneManagerController.BackToTheMainPage();
    }

    public void ExploreGame()
    {
        sceneManagerController.StartGame();
    }

    public void ShowInfo()
    {
        sceneManagerController.ShowInfoPage();
    }
}
