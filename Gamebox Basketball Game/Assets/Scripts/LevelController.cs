using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private RingBehaviour ringBehaviour;
    [SerializeField] private UIController ui;

    [SerializeField] PlayerController playerController;

    [SerializeField] private GameObject ringGuard;
    [SerializeField] private GameObject Video;
    [SerializeField] private GameObject opponent;
    [SerializeField] private GameObject spawnPoint;

    [SerializeField] private float newOpponentSpeed;

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

    private AudioSource audio;

    private OpponentController opController;
    
    void Start()
    {
        opController = opponent.GetComponent<OpponentController>();
        audio = GetComponent<AudioSource>();
        groundMoved = false;
        level = 1;
        ui.ManageLevelIndicator(level);
    }

    void Update()
    {
        //RespawnPlayer();
        score = scoreManager.GetScore();
        if (score == levelThreshold[0] && level == 1)
        {
            ToLevel2();
            level++;
            ui.ManageLevelIndicator(level);
        }

        if (score == levelThreshold[1] && level == 2)
        {
            ToLevel3();
            level++;
            ui.ManageLevelIndicator(level);
        }

        if (score == levelThreshold[2] && level == 3)
        {
            ToLevel4();
            level++;
            ui.ManageLevelIndicator(level);
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
        opController.SetSpeed(newOpponentSpeed);
        ringBehaviour.SetMove();
    }

    void ToLevel3()
    {
        ringGuard.SetActive(true);
        spawnPoint.SetActive(true);
        RespawnPlayer();
        RespawnBall();
        RespawnRing();
        ringBehaviour.SetMove();
        MoveGround();
    }

    void ToLevel4()
    {
        spawnPoint.SetActive(false);
        ringGuard.SetActive(false);
        opponent.SetActive(false);
        audio.Stop();
        Video.SetActive(true);
        Debug.Log("Game beaten!");
        MoveGround();
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
