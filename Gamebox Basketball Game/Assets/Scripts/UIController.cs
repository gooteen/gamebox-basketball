using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject[] bulbs;
    [SerializeField] private GameObject pickupPrompt;
    [SerializeField] private Image scale;

    [SerializeField] private Animator anim;
    [SerializeField] private string triggerName = "BallLost";

    [SerializeField] private CameraController playerView;
    [SerializeField] private LevelController levelController;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private Text scoreUI;
    [SerializeField] private string scoreUIText = "Score: ";

    [SerializeField] private Text levelUI;
    [SerializeField] private string levelUIText = "Level: ";

    void Update()
    {
        ManageThePickupTip();
        scoreUI.text = scoreUIText + scoreManager.GetScore();
        //levelUI.text = levelUIText + levelController.GetLevel();
    }

    public void FillScale(float dividend, float divider)
    {
        scale.fillAmount = dividend / divider;
        
    }

    public void PlayAnimation()
    {
        anim.SetTrigger(triggerName);
    }

    public void ManageLevelIndicator(int level)
    {
        for (int i = 0; i < bulbs.Length; i++)
        {
            Debug.Log(i);
            if (i < level - 1)
            {
                bulbs[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                bulbs[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            } else 
            {
                bulbs[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.196f, 0.196f, 0.196f, 0.196f));
                bulbs[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(0,0,0,0));
            }
        }
    }

    private void ManageThePickupTip()
    {
        if (playerView.CheckIfPickable())
        {
            pickupPrompt.SetActive(true);
        }
        else
        {
            pickupPrompt.SetActive(false);
        }
    }
}
