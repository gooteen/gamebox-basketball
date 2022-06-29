using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private PlayerController controller;

    [SerializeField] private UIController ui;

    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string ballTag = "Ball";

    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce;

    [SerializeField] private float speed;
    [SerializeField] private float followDistance;
    [SerializeField] private LayerMask mask;

    private bool isBallNear;


    void Update()
    {
        isBallNear = Physics.CheckSphere(transform.position, followDistance, mask);
        if (isBallNear)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if ((col.gameObject.tag == ballTag || col.gameObject.tag == playerTag) && controller.CheckBall())
        {
            Debug.Log("s");
            ThrowAway();
        }
    }

    void ThrowAway()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 direction = throwPoint.position - ball.position;
        direction = direction.normalized;
        controller.Release();
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
        ui.PlayAnimation();
    }
}
