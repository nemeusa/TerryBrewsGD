using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class SceneChanger : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;
    public void LoadScene(string sceneName)
    {
       // SceneManager.LoadScene(sceneName);
        StartCoroutine(ChangeToSceneB(sceneName));
    }
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator ChangeToSceneB(string scene)
    {
        transitionAnim.SetTrigger("exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator ChangeToSceneA()
    {
        transitionAnim.SetTrigger("exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

}