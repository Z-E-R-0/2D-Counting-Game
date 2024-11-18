using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager =FindFirstObjectByType<GameManager>();
            Debug.Log("Player Dead");
            gameManager.ShowGameOverScreen();
        }
    }
}
