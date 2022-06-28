using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehaviour : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float offsetHor;
    [SerializeField] private float offsetVer;

    [SerializeField] private float speedHor;
    [SerializeField] private float speedVer;

    [SerializeField] private bool move;
    [SerializeField] private bool isHorizontal;
    [SerializeField] private bool toTheRight;
    [SerializeField] private bool atTheBottom;
  
    void Start()
    {
        atTheBottom = true;
        toTheRight = false;
        move = false;
        isHorizontal = true;
    }

    void Update()
    {
        if (move)
        {
            if(isHorizontal)
            {
                MoveHorizontally();
            } else
            {
                MoveVertically();
            }
        }
        
    }

    public void SetDirection()
    {
        isHorizontal = !isHorizontal;
    }

    public void SetMove()
    {
        move = !move;
    }

    void MoveHorizontally()
    {
        if (toTheRight)
        {
            if (transform.position.x > (spawnPoint.position.x - offsetHor + 0.1f))
            {
                Debug.Log($"{transform.position.x} > {spawnPoint.position.x - offsetHor}");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(spawnPoint.position.x - offsetHor, transform.position.y, transform.position.z), speedHor * Time.deltaTime);
            }
            else
            {
                toTheRight = false;
            }
        }
        else
        {
            if (transform.position.x < (spawnPoint.position.x + offsetHor))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(spawnPoint.position.x + offsetHor, transform.position.y, transform.position.z), speedHor * Time.deltaTime);
            }
            else
            {
                toTheRight = true;
            }
        }
    }

    void MoveVertically()
    {
        if (atTheBottom)
        {
            if (transform.position.y < (spawnPoint.position.y + offsetVer))
            {
                //Debug.Log($"{transform.position.x} > {spawnPoint.position.x - offsetHor}");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, spawnPoint.position.y + offsetVer, transform.position.z), speedVer * Time.deltaTime);
            }
            else
            {
                atTheBottom = false;
            }
        }
        else
        {
            if (transform.position.y > (spawnPoint.position.y))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, spawnPoint.position.y, transform.position.z), speedVer * Time.deltaTime);
            }
            else
            {
                atTheBottom = true;
            }
        }
    }
   
    
}
