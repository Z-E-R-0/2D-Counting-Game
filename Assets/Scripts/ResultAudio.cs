using UnityEngine;

public class ResultAudio : MonoBehaviour
{
    [SerializeField] public AudioClip[] correctAudio;
    [SerializeField] public AudioClip[] wrongAudio;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayCorrectAudio()
    {
        int index = Random.Range(0, correctAudio.Length);
        audioSource.clip = correctAudio[index];
        audioSource.Play();



    }
    public void PlayWrongAudio()
    {
        int index = Random.Range(0, wrongAudio.Length);
        audioSource.clip = wrongAudio[index];
        audioSource.Play();



    }

}
