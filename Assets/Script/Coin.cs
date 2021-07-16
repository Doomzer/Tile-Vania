using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int score = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(score);
        Destroy(gameObject);
    }
}
