using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public void Retry()
    {
        if (!string.IsNullOrEmpty(SceneTracker.ultimaEscenaJugable))
        {
            SceneManager.LoadScene(SceneTracker.ultimaEscenaJugable);
        }
        else
        {
            SceneManager.LoadScene("Noche1");
        }
    }
}
