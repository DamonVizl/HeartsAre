
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance; 

    [SerializeField] SceneAsset[] _scenes;

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    /// <summary>
    /// switch to scene. 0 - start, 1 - play, 2 - lose, 3 - win. 
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void SetScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

}
