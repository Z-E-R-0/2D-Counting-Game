using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Text field to display the question
    public int levelSelected; // Number of digits to generate
    private int randomNumber; // The randomly generated number
    private int correctAnswer; // The correct answer based on the question
    private string currentPlace; // The place value being asked about (ones, tens, etc.)

    public TextMeshProUGUI scoreText;  // UI Text for displaying the score
    private int star = 0;  // Player's score

    public GameObject gemPrefab; // Prefab of the gem
    public Transform[] spawnPoints; // Array of possible spawn points for gems
    public int numberOfWrongGems = 3; // Number of incorrect gems to spawn

    // Call this when a level is selected
    public void SelectLevel(int level)
    {
        levelSelected = level;
        GenerateRandomNumber(levelSelected);
        AskQuestion();
        SpawnGems(); // Spawn gems after generating the question
    }

    // Generate a random number based on the level (number of digits)
    void GenerateRandomNumber(int numberOfDigits)
    {
        int minValue = (int)Mathf.Pow(10, numberOfDigits - 1); // Smallest number
        int maxValue = (int)Mathf.Pow(10, numberOfDigits) - 1; // Largest number
        randomNumber = Random.Range(minValue, maxValue);
    }

    // Randomly select a place value and ask a question
    void AskQuestion()
    {
        // Randomly pick a place value (ones, tens, hundreds, etc.) based on the level selected
        int randomPlaceValue = Random.Range(1, levelSelected + 1); // Randomly pick between 1 (ones) to the selected level
        correctAnswer = ExtractDigitAtPlace(randomNumber, randomPlaceValue);
        currentPlace = GetPlaceName(randomPlaceValue);

        // Display the question
        questionText.text = $"How many {currentPlace} are in {randomNumber}?";
        Debug.Log($"Correct answer for {currentPlace}: {correctAnswer}");
    }

    // Extract the digit at a given place (1 for ones, 2 for tens, etc.)
    int ExtractDigitAtPlace(int number, int place)
    {
        return (number / (int)Mathf.Pow(10, place - 1)) % 10;
    }

    // Return the name of the place based on the place value
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

    // Check if the player hit the correct gem
    public bool CheckAnswer(int playerAnswer)
    {
        if (playerAnswer == correctAnswer)
        {
            // If the answer is correct, increase the score and generate a new question
            Debug.Log("Correct! Generating new number...");
            AddPoints(1);  // Add points (stars) for correct answer
            GenerateNewQuestion();
            return true;
        }
        return false;
    }

    // Generate a new question after a correct answer
    void GenerateNewQuestion()
    {
        GenerateRandomNumber(levelSelected);  // Re-generate the random number
        AskQuestion();  // Ask the next question based on the new number
        SpawnGems();  // Re-spawn the gems
    }

    public void AddPoints(int points)
    {
        star += points;  // Increase the score
        UpdateScoreUI();  // Update the UI
    }

    // Update the score UI
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + star;
    }

    // Spawn the correct gem and a few incorrect ones
    void SpawnGems()
    {
        // Clear previous gems in the scene
        ClearExistingGems();

        // Shuffle the spawn points to randomize the position
        Transform[] shuffledSpawnPoints = ShuffleArray(spawnPoints);

        // Spawn the correct gem at one of the spawn points
        SpawnGem(correctAnswer, shuffledSpawnPoints[0]);

        // Spawn wrong gems at other points
        for (int i = 1; i <= numberOfWrongGems; i++)
        {
            int wrongAnswer = GenerateWrongAnswer();
            SpawnGem(wrongAnswer, shuffledSpawnPoints[i]);
        }
    }

    // Spawn a gem with the given value at the given position
    void SpawnGem(int value, Transform spawnPoint)
    {
        GameObject gem = Instantiate(gemPrefab, spawnPoint.position, Quaternion.identity);
        Gem gemScript = gem.GetComponent<Gem>();
        gemScript.gemValue = value; // Set the gem's value
        gemScript.gameManager = this; // Assign the GameManager to the gem
    }

    // Generate a wrong answer that is different from the correct answer
    int GenerateWrongAnswer()
    {
        int wrongAnswer;
        do
        {
            wrongAnswer = Random.Range(0, 10); // Generate a number between 0 and 9
        } while (wrongAnswer == correctAnswer); // Ensure it's not the correct answer
        return wrongAnswer;
    }

    // Shuffle an array of spawn points to randomize gem positions
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

    // Clear any existing gems in the scene before spawning new ones
    void ClearExistingGems()
    {
        foreach (GameObject gem in GameObject.FindGameObjectsWithTag("Gem"))
        {
            Destroy(gem); // Destroy existing gems
        }
    }
}
