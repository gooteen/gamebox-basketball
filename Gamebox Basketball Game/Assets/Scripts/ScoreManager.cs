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
    //исключаем ситуацию, когда можно забить путем броска в кольцо снизу вверх и последующего падения мяча в кольцо
    //после броска снизу вверх в кольца для возвращения возможности получать очки игроку необходимо взять мяч в руки

    private Rigidbody rb;

    void Start()
    {
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
                if (canScore)
                {
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
        Debug.Log($"score: {score}");
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
