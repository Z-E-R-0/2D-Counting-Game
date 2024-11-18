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
    [SerializeField] private AudioSource clickSound;
    // This method is linked to the button's OnClick event

    private void Awake()
    {

        
           
       
       
    }
    public void OnLevelSelected()
    {
        clickSound.PlayOneShot(clickSound.clip);
        gameManager.SelectLevel(level);
        
    }
   

    
}
