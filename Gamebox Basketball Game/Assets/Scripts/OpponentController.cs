using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform dutyPoint;
    [SerializeField] private PlayerController controller;

    [SerializeField] private UIController ui;

    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string ballTag = "Ball";

    [SerializeField] private float throwForce;

    [SerializeField] private float speed;
    [SerializeField] private float followDistance;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float blinkRate;

    [SerializeField] private float lastBlinkTime;

    private Color[] blinkColors = {Color.red, Color.blue};
    private bool isBallNear;
    private bool soundOn;
    private bool isRed;

    private AudioSource audio;

    void Start()
    {
        lastBlinkTime = blinkRate;
        soundOn = false;
        audio = GetComponent<AudioSource>();
        transform.position = dutyPoint.position;
    }
   
    void Update()
    {
        isBallNear = Physics.CheckSphere(transform.position, followDistance, mask);
        if (controller.CheckBall())
        {
            if (isBallNear)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.position.x, transform.position.y, ball.position.z), speed * Time.deltaTime);
                if (!soundOn)
                {
                    audio.Play();
                    soundOn = true;

                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(dutyPoint.position.x, transform.position.y, dutyPoint.position.z), speed * Time.deltaTime);
                if (soundOn)
                {
                    audio.Stop();
                    soundOn = false;

                }
            }
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(dutyPoint.position.x, transform.position.y, dutyPoint.position.z), speed * Time.deltaTime);
            if (soundOn)
            {
                audio.Stop();
                soundOn = false;

            }
                   
        }
        Blink(soundOn);
        
    }

    public void SetSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    /*
    public void SetRadius(float r)
    {
        followDistance = r;
    }
    */

    void OnTriggerEnter(Collider col)
    {
        Debug.Log($"Collided with {col.gameObject.tag}");
        if ((col.gameObject.tag == ballTag || col.gameObject.tag == playerTag) && controller.CheckBall())
        {
            Debug.Log("s");
            ThrowAway();
        }
    }

    void ThrowAway()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 direction = ball.position - transform.position;
        direction = direction.normalized;
        controller.Release();
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
        ui.PlayAnimation();
        controller.Grunt();
    }

    void Blink(bool check)
    {
        if (!check) 
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0, 0.5f));
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            isRed = true;
        } else
        {
            if(Time.time - lastBlinkTime > blinkRate)
            {
                if (isRed)
                {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 1, 0.5f));
                    GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                }
                else
                {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0, 0.5f));
                    GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                }
                isRed = !isRed;
                lastBlinkTime = Time.time;
            }
            
        }
    }
}
