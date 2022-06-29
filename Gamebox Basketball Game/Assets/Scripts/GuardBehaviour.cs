using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private float speed; 

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(ball.position.x, ball.position.y, transform.position.z), speed * Time.deltaTime);       
    }

    public void SetSpeed(float newSpeed)
    {
        speed += newSpeed;
    }

}
