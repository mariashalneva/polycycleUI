using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum State { one, two, three, four }

public class CubeManager : MonoBehaviour
{
    public GameObject[] cubes;

    public void ChangeView(int state)
    {
        switch (state)
        {
            case 1:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[0].gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[1].gameObject.SetActive(true);
                }
                break;
            case 3:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[2].gameObject.SetActive(true);
                }
                break;
            case 4:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[3].gameObject.SetActive(true);
                }
                break;
        }
    }

}
