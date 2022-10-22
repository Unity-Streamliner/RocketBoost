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
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;

    private AudioSource _audioSource;


    private bool _isTransitioning = false;


    private void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning) 
        {
            return;
        }

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
        _isTransitioning = true;
        // TODO: add SFX  upon crash
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashAudio);
        // TODO: add particle effect upon crash
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
        _isTransitioning = false;
    }

    void StartSuccessSequence() 
    {
        _isTransitioning = true;
        // TODO: add SFX  upon level complete
        _audioSource.Stop();
        _audioSource.PlayOneShot(successAudio);
        // TODO: add particle effect upon level complete
        successParticles.Play();
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
