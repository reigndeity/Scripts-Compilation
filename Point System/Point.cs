using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int score = 10;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        scoreManager.AddScore(score);
        Debug.Log("Score: " + scoreManager.totalScore);
        this.gameObject.SetActive(false);
    }
}