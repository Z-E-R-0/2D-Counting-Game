using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
   
   [SerializeField] private Transform heathCounter;
   [SerializeField] private Transform objectsToDestroy;
   [SerializeField] private Transform endGameUI;
   [SerializeField] private Transform winGameUI;
    [SerializeField] public TMP_Text scoreText;
    private int currentcoins;
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForGameOver();
        CheckForGameWin();
    }
    public void CheckForGameOver()
    {
        if (heathCounter.childCount == 0)
        {
            Debug.Log("GameOver");
            endGameUI.gameObject.SetActive(true);
        }
    }
    public void CheckForGameWin()
    {

        if (objectsToDestroy.childCount == 0)
        {
            Debug.Log("GameWon");
            winGameUI.gameObject.SetActive(true);
        }

    }

    public void AddScoreCoins(int coins)
    {

        currentcoins = currentcoins + coins;
        scoreText.text = "Coins:-" + currentcoins.ToString();
    }
}
