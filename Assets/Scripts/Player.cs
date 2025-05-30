using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask _beverageLayer;
    [SerializeField] LayerMask _clientLayer;

    public string currentRequest;

    private string _selectedDrink = null;

    private int _score = 0;
    private int _cordura = 100;

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _selectionText;
    [SerializeField] TMP_Text _corduraText;

    [SerializeField] MeshRenderer meshPumpBar;
    [SerializeField] MeshRenderer meshPumpHand;

    bool _usePump;

    Pump _pumpCode = null;

    [SerializeField] ParticleSystem _smokeParticle;

    [SerializeField] Scene _sceneName;

    Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _beverageLayer))
            {
                Beverage drinkType = hit.collider.GetComponent<Beverage>();
                Pump pump = hit.collider.GetComponent<Pump>();
                _pumpCode = pump;

                if (drinkType != null)
                {
                    _selectedDrink = drinkType.drinkType.ToString();
                    PumpOff();
                    _pumpCode = null;
                }
                if (_pumpCode != null)
                {
                    PumpOn();
                    _selectedDrink = null;
                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, _clientLayer))
            {
                Client client = hit.collider.GetComponent<Client>();

                if (client != null)
                { 
                    //client.player = this;
                    currentRequest = client.currentRequest.ToString();
                    //Debug.Log("pide : " + currentRequest);
                }

                EntregarBebida(client);
            }
        }

        if (_score < 0) _score = 0;
        _scoreText.text = "Puntos: " + _score;
        _selectionText.text = "Tienes: " + _selectedDrink;

        if (_cordura <= 0) SceneManager.LoadScene("Lose");
        if (_cordura > 100) _cordura = 100;
        _corduraText.text = "Cordura: " + _cordura;

        if (_score >= 1000)
        {
            //void LoadScene(string sceneName)
            //{
               SceneManager.LoadScene("Win");
            //}

        }


    }

    public void EntregarBebida(Client client)
    {

        if (_usePump)
        {
            client.isDeath = true;
            if (client.imposter)
            {
                _score += 50;
                _cordura += 10;
            }
            else
            {
                _score -= 200;
                _cordura -= 15;
            }
            StartCoroutine(Shoot());
        }

        if (_selectedDrink == null) return;

        if (_selectedDrink == currentRequest)
        {
            Debug.Log("pedido correcto");
            if (!client.imposter)
            {
                _score += 100;
                _cordura += 5;
            }
            else
            {
                _score -= 50;
                _cordura -= 30;
                StartCoroutine(_flash.PostActive());
            }
            client.goodOrder = true;
        }
        else
        {
            _score -= 50;        }

        _selectedDrink = null;
    }

    void PumpOn()
    {
        meshPumpBar.enabled = false;
        meshPumpHand.enabled = true;
        _usePump = true;
    }

    void PumpOff()
    {
        meshPumpBar.enabled = true;
        meshPumpHand.enabled = false;
        _usePump = false;
        _pumpCode = null;
    }

    IEnumerator Shoot()
    {
        _smokeParticle.Play();
        yield return new WaitForSeconds(0.3f);
        PumpOff ();
    }
}
