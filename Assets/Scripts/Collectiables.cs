using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectiables : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] int coinValueToAdd;
    // Start is called before the first frame update

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.AddScoreCoins(coinValueToAdd);
            Destroy(gameObject);
        }
    }
}
