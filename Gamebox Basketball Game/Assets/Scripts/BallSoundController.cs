using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] bounceClips;
    [SerializeField] private float volumeParameter;
    private AudioSource source;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        SetRandomClip();
        SetVolume();
        source.Play();
    }

    public void CallSound()
    {
        SetRandomClip();
        source.volume = 1;
        source.Play();
    }

    void SetRandomClip()
    {
        int random = Random.Range(0, bounceClips.Length);
        source.clip = bounceClips[random];
    }

    void SetVolume()
    {
        source.volume = Mathf.Clamp(rb.velocity.magnitude / volumeParameter, 0, 1);
    }
}
