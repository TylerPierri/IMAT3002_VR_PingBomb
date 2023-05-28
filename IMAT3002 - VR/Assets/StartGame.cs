using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void loadScene()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
