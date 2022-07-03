using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSlideSwitcher : MonoBehaviour
{
    [SerializeField] private float slideSwitchRate;
    [SerializeField] private GameObject[] slides;
    private float switchStart;
    private int slideIndex;
   
    void Start()
    {
        switchStart = Time.time;
        slideIndex = 0;
        Switch();
    }

    void Update()
    {
        if (Time.time - switchStart > slideSwitchRate)
        {
            Debug.Log("switched");
            Switch();
            switchStart = Time.time;
        }
    }

    void Switch()
    {
        if (slideIndex == slides.Length - 1)
        {
            slideIndex = 0;
        } else
        {
            slideIndex++;
        }
        for (int i = 0; i < slides.Length; i++) 
        {
            if (i == slideIndex)
            {
                slides[i].SetActive(true);
            }else
            {
                slides[i].SetActive(false);
            }
        }
    }
}
