using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text pickupTip;
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
        levelUI.text = levelUIText + levelController.GetLevel();
    }

    private void ManageThePickupTip()
    {
        if (playerView.CheckIfPickable()) {
            pickupTip.enabled = true;
        } else
        {
            pickupTip.enabled = false;
        }
    }

    public void FillScale(float dividend, float divider)
    {
        scale.fillAmount = dividend / divider;
    }

    public void PlayAnimation()
    {
        anim.SetTrigger(triggerName);
    }
}
