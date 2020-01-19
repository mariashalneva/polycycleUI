using GoogleARCore.Examples.AugmentedImage;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlasticTypes { Cup, Bottle, Straw}
public enum MenuTypes { Scan, Info, Explore,  ScanAgain }


public class SceneManagerController : MonoBehaviour
{
    public PlasticTypes currentType = PlasticTypes.Bottle;
    public MenuTypes currentMenuType = MenuTypes.Scan;

    public bool IsObjectDetected = false;
    private static SceneManagerController instanceRef;


    public GameObject[] InfoPanels;

    void Awake()
    {

        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }


    public void SelectObjectId()
    {
        var selectedItem = FindObjectsOfType<AugmentedImageVisualizer>();
        if (selectedItem == null)
            return;

        var active = selectedItem.FirstOrDefault(x => x.isActiveAndEnabled);
        if (active == null)
            return;
        currentType = active.type;
    }

    public void SelectObjectId(string tag)
    {
        IsObjectDetected = true;
        if (tag == "Cup")
            currentType = PlasticTypes.Cup;
        else if (tag == "Bottle")
            currentType = PlasticTypes.Bottle;
    }

    public void SelectObjectId(PlasticTypes type)
    {
        IsObjectDetected = true;
        currentType = type;
    }

    public void StartGame()
    {
        if (!IsObjectDetected)
            return;

        SelectObjectId();
        if (currentType == PlasticTypes.Cup)
            SceneManager.LoadScene(1);
        else if (currentType == PlasticTypes.Bottle)
            SceneManager.LoadScene(2);
        else if(currentType == PlasticTypes.Straw)
            SceneManager.LoadScene(3);
    }

    public void BackToTheMainPage()
    {
        IsObjectDetected = false;
        SceneManager.LoadScene(0);
    }

    public void ShowInfoPage()
    {
        GameObject panel = new GameObject();
        SelectObjectId();
        if (currentType == PlasticTypes.Cup)
            panel = GameObject.FindGameObjectWithTag("InfoCup");
        else if (currentType == PlasticTypes.Bottle)
            panel = GameObject.FindGameObjectWithTag("InfoBottle");
        else if (currentType == PlasticTypes.Straw)
            panel = GameObject.FindGameObjectWithTag("InfoStraw");

        panel.SetActive(true);
    }

}
