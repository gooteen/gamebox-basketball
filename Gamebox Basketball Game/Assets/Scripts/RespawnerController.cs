using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerController : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string ballTag = "Ball";
    [SerializeField] AudioSource audio;

   void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == playerTag)
        {
            levelController.RespawnPlayer();
        } else if (col.gameObject.tag == ballTag)
        {
            audio.Play();
            levelController.RespawnBall();
        }
    }
}
