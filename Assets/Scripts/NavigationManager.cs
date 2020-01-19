using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    private SceneManagerController sceneManagerController;
    public Button Explore;
    public Button Info;

    public GameObject[] InfoPanels;

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
        var currentType = sceneManagerController.currentType;
        if (currentType == PlasticTypes.Cup)
            InfoPanels[0].SetActive(true);
        else if (currentType == PlasticTypes.Bottle)
            InfoPanels[1].SetActive(true);
        else if (currentType == PlasticTypes.Straw)
            InfoPanels[1].SetActive(true);
    }
}
