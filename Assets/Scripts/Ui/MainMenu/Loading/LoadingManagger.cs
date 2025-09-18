using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingManagger : MonoBehaviour
{
    public static int targetedSceneID;
    void Start()
    {
        if (targetedSceneID == 1) // -1: dev | 1 = normal map
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (targetedSceneID == -1)
        {
            SceneManager.LoadScene("DeveloperScene");
        }
        else
        {
            Debug.LogWarning("The targetSceneID is not set.");
        }
    }
}
