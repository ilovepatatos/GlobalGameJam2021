using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int NextScene;

    private void Start()
    {
        SceneManager.LoadScene(NextScene);
    }
}
