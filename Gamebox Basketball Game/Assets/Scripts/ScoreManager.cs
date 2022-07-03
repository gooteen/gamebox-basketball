using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string counterTag;
    [SerializeField] private string counterTag2;
    [SerializeField] private int score;
    [SerializeField] private float velocityCheckValue = 0.5f;

    [SerializeField] private bool canScore;

    [SerializeField] private PlayerController controller;

    [SerializeField] private float scoreCoolDownTotal = 0f;
    [SerializeField] private bool hitFirstCollider;

    private float scoreCoolDownStart;

    private Rigidbody rb;

    void Start()
    {
        hitFirstCollider = false;
        scoreCoolDownStart = scoreCoolDownTotal;
        canScore = true;
        rb = GetComponent<Rigidbody>();
        score = 0;
    }


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("here");
        if (col.gameObject.tag == counterTag && (!controller.GetPicked()))
        {
            Debug.Log("first collider");
            if (rb.velocity.y < velocityCheckValue)
            {
                Debug.Log("Condition checked");
                if (canScore && ((Time.time - scoreCoolDownStart) > scoreCoolDownTotal))
                {
                    Debug.Log($"Can Score: {canScore}");
                    hitFirstCollider = true;
                }
                else
                {
                    Debug.Log("canScore: " + canScore + "coolDown: " + ((Time.time - scoreCoolDownStart) > scoreCoolDownTotal));
                }
            } else
            {
                canScore = false;
            }
            
        } else if(col.gameObject.tag == counterTag2 && (!controller.GetPicked()))
        {
            Debug.Log("second collider");
            if (hitFirstCollider)
            {
                AddPoint();
                hitFirstCollider = false;
            }
        }
        {

        }
    }

    public int GetScore()
    {
        return score;
    }

    public void UnlockScoring()
    {
        canScore = true;
    }

    public void ResetTrigger()
    {
        hitFirstCollider = false;
    }

    void AddPoint()
    {
        score++;
        controller.PlayScoreSound();
        Debug.Log($"score: {score}");
        scoreCoolDownStart = Time.time;
    }

    

}
