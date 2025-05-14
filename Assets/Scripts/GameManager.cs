using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask beverageLayer;
    public LayerMask clientLayer;
    public Animator cameraDown;

    public Clients clientCode;
    public InputManager piumbaCode;

    public TMP_Text scoreText;
    public TMP_Text requestText;
    public GameObject cliente;
    public GameObject JumpsCareSus;

    private string selectedDrink = null;
    private string currentRequest;
    private int score = 0;

    void Start()
    {
        clientCode = GetComponent<Clients>();
        NuevaPeticion();
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, beverageLayer))
            {
                Beverage drinkType = hit.collider.GetComponent<Beverage>();
                if (drinkType != null)
                {
                    selectedDrink = drinkType.drinkType.ToString();
                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, clientLayer))
            {
                EntregarBebida();
            }
        }
        if (piumbaCode.piumba && !clientCode._imposter && clientCode.pidiendo)
        {
            Debug.Log("naooo me mataste :c");
            score -= 1;
            if (score < 0) score = 0;
            scoreText.text = "Puntos: " + score;

        }

        else if (piumbaCode.piumba && clientCode._imposter && clientCode.pidiendo)
        {
            score += 1;
            Debug.Log("que pete");
            scoreText.text = "Puntos: " + score;
        }

    }

    void EntregarBebida()
    {
        if (score < 0) score = 0;

        if (selectedDrink == null) return;

        if (selectedDrink == currentRequest)
        {
            //Debug.Log("Si");
            if (!clientCode._imposter)
            {
                score += 100;
                cameraDown.SetBool("Imposter", false);

            }

            else
            {
                score -= 100;
                if (score < 0) score = 0;
                StartCoroutine(ScaryJumpscary());
            }
        }
        else
        {
            score -= 10;
            if (score < 0) score = 0;
            // Debug.Log("No");
        }



        scoreText.text = "Puntos: " + score;
        selectedDrink = null;
        NuevaPeticion();
    }

    void NuevaPeticion()
    {
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        currentRequest = opciones[Random.Range(0, opciones.Length)];
        requestText.text = "El cliente quiere: " + currentRequest;

        cliente.GetComponent<NPCRequest>().requestedItem = currentRequest;
    }

    IEnumerator ScaryJumpscary()
    {
        JumpsCareSus.SetActive(true);
        cameraDown.SetBool("Imposter", true);
        yield return new WaitForSeconds(1);
        cameraDown.SetBool("Imposter", false);
        JumpsCareSus.SetActive(false);


    }
}
