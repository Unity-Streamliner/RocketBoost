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
}
