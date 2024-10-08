using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public GameManager gameManager;
    public int level;
    public JetpackPlayerController jetpackPlayer;
    public GameObject player;
    public bool isGameLevel = false;
    // This method is linked to the button's OnClick event

    private void Awake()
    {

        
           
       
       
    }
    public void OnLevelSelected()
    {
        gameManager.SelectLevel(level);
        
    }
   

    
}
