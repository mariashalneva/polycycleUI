using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public enum State { one, two, three, four }

public class CubeManager : MonoBehaviour
{
    public GameObject[] cubes;
    public AudioClip[] audio;

    public Slider sliderVal;
    public AudioSource source;

    private void Awake()
    {
        source = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        sliderVal.value = 0;
    }

    public void OnValueChanged()
    {
        if (sliderVal.value <= 0)
            ChangeView(1);
        else if (sliderVal.value > 0.2 && sliderVal.value <= 0.3)
            ChangeView(2);
        else if (sliderVal.value > 0.4 && sliderVal.value <= 0.7)
            ChangeView(3);
        else if (sliderVal.value > 0.75 && sliderVal.value <= 1)
            ChangeView(4);

    }

    public void ChangeView(int state)
    {
        switch (state)
        {
            case 1:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[0].gameObject.SetActive(true);
                    source.clip = audio[0];
                }
                break;
            case 2:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[1].gameObject.SetActive(true);
                    source.clip = audio[1];
                }
                break;
            case 3:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[2].gameObject.SetActive(true);
                    source.clip = audio[2];
                }
                break;
            case 4:
                {
                    cubes.ToList().ForEach(x => x.SetActive(false));
                    cubes[3].gameObject.SetActive(true);
                    source.clip = audio[3];
                }
                break;
        }

        source.Play();
    }

}
