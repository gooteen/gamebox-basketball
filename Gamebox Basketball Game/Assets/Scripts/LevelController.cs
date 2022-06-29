using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] RingBehaviour ringBehaviour;
    [SerializeField] GuardBehaviour guardBehaviour;
    [SerializeField] private float guardSpeedBoost;

    [SerializeField] PlayerController playerController;

    [SerializeField] private GameObject ringGuard;
    [SerializeField] private GameObject springPlatform;

    [SerializeField] private Transform ground;
    [SerializeField] private float groundShiftPosition = -0.45f;

    [SerializeField] private Transform ballSpawnPoint;
    [SerializeField] private float ballSpawnOffset;
    [SerializeField] private Transform ringSpawnPoint;

    [SerializeField] Transform ball;
    [SerializeField] Transform ring;


    [SerializeField] int[] levelThreshold;
    [SerializeField] private int level;
    private int score;

    private bool groundMoved;
    
    void Start()
    {
        groundMoved = false;
        level = 1;
    }

    void Update()
    {
        //RespawnPlayer();
        score = scoreManager.GetScore();
        if (score == levelThreshold[0] && level == 1)
        {
            ToLevel2();
            level++;
        }

        if (score == levelThreshold[1] && level == 2)
        {
            ToLevel3();
            level++;
        }

        if (score == levelThreshold[2] && level == 3)
        {
            ToLevel4();
            level++;
        }

    }

    public int GetLevel()
    {
        return level;
    }


    public void RespawnPlayer()
    {
        playerController.SetBeingSpawned();
        //player.rotation = playerSpawnPoint.rotation;
    }

    public void RespawnBall()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, 0f);
        ball.position = new Vector3(ballSpawnPoint.position.x, ballSpawnPoint.position.y + ballSpawnOffset, ballSpawnPoint.position.z);
        ball.rotation = ballSpawnPoint.rotation;
    }

    public void RespawnRing()
    {
        ring.position = ringSpawnPoint.position;
        ring.rotation = ringSpawnPoint.rotation;
    }

    void ToLevel2()
    {
        RespawnPlayer();
        RespawnBall();
        //RespawnRing();
        ringBehaviour.SetMove();
    }

    void ToLevel3()
    {
        ringGuard.SetActive(true);
        RespawnPlayer();
        RespawnBall();
        RespawnRing();
        ringBehaviour.SetMove();
        MoveGround();
    }

    void ToLevel4()
    {
        RespawnRing();
        springPlatform.SetActive(true);
        ringBehaviour.SetDirection();
        ringBehaviour.SetMove();
        RespawnPlayer();
        RespawnBall();
        guardBehaviour.SetSpeed(guardSpeedBoost);
    }

    void MoveGround()
    {
        if (groundMoved)
        {
            ground.localPosition = new Vector3(ground.localPosition.x, ground.localPosition.y, 0f);
        } else
        {
            ground.localPosition = new Vector3(ground.localPosition.x, ground.localPosition.y, groundShiftPosition);
        }
        groundMoved = !groundMoved;
    }
}
