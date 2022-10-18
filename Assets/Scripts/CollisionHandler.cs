using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    public const string FriendlyTag = "Friendly";
    public const string FinishTag = "Finish";

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case FriendlyTag:
                print("dbg: bump into friend!");
                break;
            case FinishTag:
                print("dbg: bump into finish!");
                LoadNextLevel();
                break;
            default:
                print("dbg: bump into something else!");
                ReloadLevel();
                break;
        }
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
