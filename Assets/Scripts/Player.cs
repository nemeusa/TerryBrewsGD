using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask _beverageLayer;
    [SerializeField] LayerMask _clientLayer;

    public string currentRequest;

    private string _selectedDrink = null;

    public int _score = 0;
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

    [SerializeField] Volume volume;
    [SerializeField] Vignette vignette;

    Flash _flash;
   
    Client _client;

    public TalksTeme _talkTheme;

    public bool help;

    private void Awake()
    {
        _flash = GetComponent<Flash>();

        if (volume.profile.TryGet(out vignette))
        {
            Debug.Log("Vi�eta Encontrada");
        }
        else
        {
            Debug.LogWarning("Vignette no encontrado en el Volume.");
        }

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
        _scoreText.text = "$ " + _score;
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

        if (Input.GetKeyDown(KeyCode.A)) help = !help;

        if (vignette != null)
        {
         Debug.Log("Vi�eta RE Encontrada");
         vignette.intensity.value = 1f - (_cordura / 100f);// Intensidad inversamente proporcional a la vida
        }


    }

    public void EntregarBebida(Client client)
    {
        _client = client;

        if (_usePump)
        {
            client.isDeath = true;
            if (client.imposter)
            {
                _score += 50;
                _cordura += 10;
                StartCoroutine(correct());
            }
            else
            {
                _score -= 200;
                _cordura -= 15;
                StartCoroutine(Incorrect());
            }
            StartCoroutine(Shoot());
        }

        if (_selectedDrink == null) return;

        if (_selectedDrink == currentRequest)
        {
            Debug.Log("pedido correcto");
            if (!client.imposter)
            {
                StartCoroutine(correct());
                _score += 100;
                _cordura += 5;
            }
            else
            {
                StartCoroutine(Incorrect());
                _score -= 50;
                _cordura -= 30;
                StartCoroutine(_flash.PostActive());
            }
            client.goodOrder = true;
        }
        else
        {
            StartCoroutine(Incorrect());

            _score -= 50;        
        }

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
    IEnumerator Incorrect()
    {
        _client.visor.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        _client.visor.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    IEnumerator correct()
    {
        _client.visor.GetComponent<MeshRenderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        _client.visor.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
