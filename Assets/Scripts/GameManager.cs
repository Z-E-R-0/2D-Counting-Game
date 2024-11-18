using UnityEngine;
using TMPro;
using System.Collections; // Required for coroutines
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("LivesUI")]
    public GameObject heartUIPrefab;  // Reference to the Heart UI prefab
    public Transform livesContainer;  // Parent container for the hearts in the UI
    public int lives = 3;  // Initial number of lives
    private List<GameObject> heartUIInstances = new List<GameObject>();  // Store references to the heart instances
    public float heartSpacing = 1.0f;  // Distance between hearts


    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winScoreText;
    public GameObject winScreen;
    public GameObject gameOverScreen;
    public int levelSelected;
    private int randomNumber;
    private int correctAnswer;
    private string currentPlace;
    [SerializeField] AudioSource coinAudio;
    [SerializeField] AudioSource wrongAnswerAudio;
    [SerializeField] AudioClip correctAudioClip;

    private int star = 0;
    private int wrongAnswers = 0;
    public int correctAnswersToWin = 5;
    public int maxWrongAnswers = 3;
    public bool isCorrect;

    public GameObject gemPrefab;
    public Transform[] spawnPoints;
    public int numberOfWrongGems = 3;
    public float delayBeforeNextQuestion = 2f; // Add this to control the delay
    [SerializeField] private ResultAudio resultAudio;

   
    private void Awake()
    {
        Time.timeScale = 1f;
        SetupLivesUI();
        resultAudio = FindAnyObjectByType<ResultAudio>();
    }
    private void SetupLivesUI()
    {
        // Instantiate hearts with spacing
        for (int i = 0; i < lives; i++)
        {
            Vector3 heartPosition = livesContainer.position + new Vector3(i * heartSpacing, 0, 0);
            GameObject heart = Instantiate(heartUIPrefab, heartPosition, Quaternion.identity, livesContainer);
            heartUIInstances.Add(heart);
        }
    }
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            Destroy(heartUIInstances[lives]);  // Remove a heart icon
            heartUIInstances.RemoveAt(lives);
            

            if (lives <= 0)
            {

                CheckForGameOver(); // End the game if no lives are left
            }
        }
    }
    public void SelectLevel(int level)
    {
        levelSelected = level;
        GenerateRandomNumber(levelSelected);
        AskQuestion();
        SpawnGems();
    }

    void GenerateRandomNumber(int numberOfDigits)
    {
        int minValue = (int)Mathf.Pow(10, numberOfDigits - 1);
        int maxValue = (int)Mathf.Pow(10, numberOfDigits) - 1;
        randomNumber = Random.Range(minValue, maxValue);
    }

    void AskQuestion()
    {
        int randomPlaceValue = Random.Range(1, levelSelected + 1);
        correctAnswer = ExtractDigitAtPlace(randomNumber, randomPlaceValue);
        currentPlace = GetPlaceName(randomPlaceValue);
        questionText.text = $"How many {currentPlace} are in {randomNumber}?";
        Debug.Log($"Correct answer for {currentPlace}: {correctAnswer}");
    }

    int ExtractDigitAtPlace(int number, int place)
    {
        return (number / (int)Mathf.Pow(10, place - 1)) % 10;
    }

    string GetPlaceName(int place)
    {
        switch (place)
        {
            case 1: return "ones";
            case 2: return "tens";
            case 3: return "hundreds";
            case 4: return "thousands";
            case 5: return "ten thousands";
            case 6: return "lakhs";
            case 7: return "ten lakhs";
            default: return "";
        }
    }

    public bool CheckAnswer(int playerAnswer)
    {
        if (playerAnswer == correctAnswer)
        {
            isCorrect = true;
            AddPoints(1);
            resultAudio.PlayCorrectAudio();
            correctAnswersToWin--;
            CheckForWin();
            StartCoroutine(WaitBeforeNextQuestion()); // Call coroutine to wait before next question
            return isCorrect;
        }
        else
        {
            wrongAnswers++;
            isCorrect = false;
            LoseLife();
            resultAudio.PlayWrongAudio();
            CheckForGameOver();
            return isCorrect;
        }
    }

    void CheckForWin()
    {
        if (correctAnswersToWin <= 0)
        {
           StartCoroutine(WaitBeforePopup2());
           
        }
    }

    void CheckForGameOver()
    {
        if (wrongAnswers >= maxWrongAnswers)
        {
            StartCoroutine(WaitBeforePopup());
           
        }
    }

    // Coroutine to wait before generating the next question
    IEnumerator WaitBeforeNextQuestion()
    {
        yield return new WaitForSeconds(delayBeforeNextQuestion); // Wait for the set delay
        GenerateNewQuestion(); // Call method to generate the next question after the delay
    }
    IEnumerator WaitBeforePopup()
    {
        yield return new WaitForSeconds(delayBeforeNextQuestion); // Wait for the set delay
         // Call method to generate the next question after the delay
        ShowGameOverScreen();
    }
    IEnumerator WaitBeforePopup2()
    {
        yield return new WaitForSeconds(delayBeforeNextQuestion); // Wait for the set delay
                                                                  // Call method to generate the next question after the delay
        ShowWinScreen();
    }

    void GenerateNewQuestion()
    {
        GenerateRandomNumber(levelSelected); // Re-generate the random number
        AskQuestion(); // Ask the next question based on the new number
        SpawnGems(); // Re-spawn the gems
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        winScoreText.text = star.ToString();
        Time.timeScale = 0f;
    }

    public void ShowGameOverScreen()
    {
       
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AddPoints(int points)
    {
        star += points;
        UpdateScoreUI();
        coinAudio.PlayOneShot(coinAudio.clip);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + star;
    }

    void SpawnGems()
    {
        ClearExistingGems();
        Transform[] shuffledSpawnPoints = ShuffleArray(spawnPoints);
        SpawnGem(correctAnswer, shuffledSpawnPoints[0]);

        for (int i = 1; i <= numberOfWrongGems; i++)
        {
            int wrongAnswer = GenerateWrongAnswer();
            SpawnGem(wrongAnswer, shuffledSpawnPoints[i]);
        }
    }

    void SpawnGem(int value, Transform spawnPoint)
    {
        GameObject gem = Instantiate(gemPrefab, spawnPoint.position, Quaternion.identity);
        Gem gemScript = gem.GetComponent<Gem>();
        gemScript.gemValue = value;
        gemScript.gameManager = this;
    }

    int GenerateWrongAnswer()
    {
        int wrongAnswer;
        do
        {
            wrongAnswer = Random.Range(0, 10);
        } while (wrongAnswer == correctAnswer);
        return wrongAnswer;
    }

    Transform[] ShuffleArray(Transform[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
    public void PlayBlastSound()
    {


        wrongAnswerAudio.PlayOneShot(wrongAnswerAudio.clip);
    }
    public void PlayCorrectBlastSound()
    {


        wrongAnswerAudio.PlayOneShot(correctAudioClip);
    }

    void ClearExistingGems()
    {
        foreach (GameObject gem in GameObject.FindGameObjectsWithTag("Gem"))
        {
            Destroy(gem);
        }
    }

}
