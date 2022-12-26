using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    AudioSource _audioSource;

    int score = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score++;
            _audioSource.Play();
        }
    }
}
