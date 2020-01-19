using GoogleARCore.Examples.AugmentedImage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Types { Cup, Bottle}
public enum MenuTypes { Scan, Info, Explore,  ScanAgain }


public class SceneManagerController : MonoBehaviour
{
    private Types currentType = 0;
    private MenuTypes currentMenuType = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SelectObjectId(int id)
    {
        var selectedItem = FindObjectOfType<AugmentedImageVisualizer>();
        if (selectedItem == null)
            return;

        if (selectedItem.tag == "Cup")
            currentType = Types.Cup;
        else if (selectedItem.tag == "Bottle")
            currentType = Types.Bottle;
    }
  

    public void StartGame()
    {
        if (currentType == Types.Cup)
            SceneManager.LoadScene(1);
        else if (currentType == Types.Bottle)
            SceneManager.LoadScene(2);
    }

    public void BackToTheMainPage()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowInfoPage()
    {
       //check id
       //setInfoPage with id VIsible
    }

}
