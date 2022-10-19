using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    public const string FriendlyTag = "Friendly";
    public const string FinishTag = "Finish";


    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] private AudioClip crashAudio;
    [SerializeField] private AudioClip successAudio;

    private AudioSource _audioSource;


    private void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case FriendlyTag:
                print("dbg: bump into friend!");
                break;
            case FinishTag:
                print("dbg: bump into finish!");
                StartSuccessSequence();
                break;
            default:
                print("dbg: bump into something else!");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // TODO: add SFX  upon crash
        _audioSource.PlayOneShot(crashAudio);
        // TODO: add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void StartSuccessSequence() 
    {
        // TODO: add SFX  upon level complete
        _audioSource.PlayOneShot(successAudio);
        // TODO: add particle effect upon level complete
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
