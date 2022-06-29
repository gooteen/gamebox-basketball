using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string counterTag;
    [SerializeField] private int score;
    [SerializeField] private float velocityCheckValue = 0.5f;

    [SerializeField] private bool canScore;

    [SerializeField] private PlayerController controller;

    [SerializeField] private float scoreCoolDownTotal = 0f;

    private float scoreCoolDownStart;
    private Rigidbody rb;

    void Start()
    {
        scoreCoolDownStart = scoreCoolDownTotal;
        canScore = true;
        rb = GetComponent<Rigidbody>();
        score = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("here");
        if (col.gameObject.tag == counterTag)
        {
            if (rb.velocity.y < velocityCheckValue)
            {
                if (canScore && ((Time.time - scoreCoolDownStart) > scoreCoolDownTotal))
                {
                    Debug.Log($"Can Score: {canScore}");
                    AddPoint();
                }
            } else
            {
                canScore = false;
            }
            
        }
    }
    void AddPoint()
    {
        score++;
        controller.PlayScoreSound();
        Debug.Log($"score: {score}");
        scoreCoolDownStart = Time.time;
    }

     public int GetScore()
    {
        return score;
    }

    public void UnlockScoring()
    {
        canScore = true;
    }

}
