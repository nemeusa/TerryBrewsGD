using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool pausa = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                pausa = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
               
            }
            else if (pausa == true)
            {
                Reanudar();
            }
        }
    }

    public void Reanudar()
    {
            if (pausa == true)
            {
                ObjetoMenuPausa.SetActive(false);
                pausa = false;
                Time.timeScale = 1;
               // Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;

            }
    }

    public void Menu()
    {
        Reanudar();
        SceneManager.LoadScene("Menu");
    }
    public void Salir()
    {
        Application.Quit();
    }
}
