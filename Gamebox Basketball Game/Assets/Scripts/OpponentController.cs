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

    private bool isBallNear;

    void Start()
    {
        transform.position = dutyPoint.position;
    }
    void Update()
    {
        isBallNear = Physics.CheckSphere(transform.position, followDistance, mask);
        if (controller.CheckBall())
        {
            if (isBallNear)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.position.x, transform.position.y, ball.position.z), speed * Time.deltaTime);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(dutyPoint.position.x, transform.position.y, dutyPoint.position.z), speed * Time.deltaTime);
        }
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
}
