using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //..- -. .- / .--. .. .--- .- / -.. . / -.-. --- -.. .. --. --- .-.-. 
    [Header("Scene")]
    //[SerializeField] string _sceneLose, _sceneWin;
    [SerializeField] string _sceneWin;
    [SerializeField] int _cashCondition = 1000;
    public int getDamage = 10;
    public SpawnContador contador;
    private AsyncOperation _cargaEscenaWin;


    [Header("Audio")]
    [SerializeField] AudioSource _shotgunAudioSource;
    [SerializeField] AudioClip _shotgunSound;
    [SerializeField] private AudioClip _emptyShotSound;

    [Header("Bebidas y clientes")]
    [SerializeField] LayerMask _beverageLayer;
    [SerializeField] LayerMask _clientLayer;
    public string currentRequest;
    private string _selectedDrink = null;

    [Header("Puntos y Cordura")]
    public int cordura = 100;
    public int _corduraDanio = 10;
    public int _corduraMatarCliente = 15;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _selectionText;
    [SerializeField] TMP_Text _corduraText;
    public int _score { get; private set; } = 0;
    public Image _corduraImageFill;

    [Header("Condición para usar escopeta")]
    [SerializeField] private int _corduraMinEscopeta = 100;
    [SerializeField] private GameObject _shotgunUnlockFeedback;
    [SerializeField] private float _fallDistance = 2f;
    [SerializeField] private Vector3 _fallOffset = new Vector3(0, 2f, 0); 
    [SerializeField] private float _fallDuration = 0.5f;
    private bool _shotgunUsable = false;
    [SerializeField] private GameObject _objetoAActivar;
    [SerializeField] private GameObject _objetoADesactivar;
    [SerializeField] private AudioClip _soundDesbloqueoEscopeta;
    [SerializeField] private AudioSource _audioSourceFeedback;
    private bool _hasPlayedShotgunSound = false;

    [Header("SHOTGUN")]
    [SerializeField] MeshRenderer meshPumpBar;
    [SerializeField] MeshRenderer meshPumpHand;
    bool _usePump;
    Pump _pumpCode = null;
    [SerializeField] ParticleSystem _smokeParticle;
    [SerializeField] private GameObject[] _ammoVisuals;
    public int _currentAmmo = 10;
    public int _maxAmmo = 10;
    private bool _isBlocked = false;
    [SerializeField] private float _blockDurationAfterShot = 1f;
    [SerializeField] private float _doubleShotCooldown = 5f; 
    [SerializeField] private int _corduraPenalty = 10;      
    private float _lastShotTime = -Mathf.Infinity;           


    [Header("URP")]
    private ChromaticAberration chromAberration;
    [SerializeField] private float aberrationOscStrength = 0.03f;
    [SerializeField] private float aberrationOscSpeed = 1f;
    [SerializeField] Volume volume;
    [SerializeField] Vignette vignette;
    [SerializeField] private float vignetteOscStrength = 0.03f;
    [SerializeField] private float vignetteOscSpeed = 1f;
    private DepthOfField depthOfField;
    private Coroutine depthCoroutine;
    public URP urp;

    [Header("NO SE XD")]
    [SerializeField] Scene _sceneName;
    public Flash flash;
    [HideInInspector] public Client _client;
    public TalksThemes _talkTheme;
    public bool help;

    MoveDrinks _moveDrinks;

    [SerializeField] Animator _pumpHandAni, _pumpBarAni;

    [SerializeField] ShowShop _showShop;


    private void Awake()
    {


        flash = GetComponent<Flash>();

        if (volume.profile.TryGet(out vignette))
        {
        }
        else
        {
        }
        if (volume.profile.TryGet(out chromAberration))
        {
        }
        else
        {
        }
        if (volume.profile.TryGet(out depthOfField))
        {
        }

        GameStats.impostoresEliminados = 0;
        GameStats.ordersCompletadas = 0;
        GameStats.sanity = 0;

    }
    private void Start()
    {
        UpdateAmmoVisuals();
        StartCoroutine(PrecargarEscenaVictoria());
    }

    private void Update()
    {
        if (_isBlocked) return;
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _beverageLayer))
            {
                MoveDrinks moveDrinks = hit.collider.GetComponent<MoveDrinks>();
                _moveDrinks = moveDrinks;
                Pump pump = hit.collider.GetComponent<Pump>();
                _pumpCode = pump;

                if (moveDrinks != null)
                    if (moveDrinks.isDraggingDrink)
                    {
                        PumpOff();
                        _pumpCode = null;
                    }

                if(!_showShop.useShop)
                if (_pumpCode != null)
                {
                        if (!_usePump)
                        {
                            if (_currentAmmo > 0)
                            {
                                PumpOn();
                            }
                            else
                            {
                                if (_shotgunAudioSource != null && _emptyShotSound != null)
                                {
                                    _shotgunAudioSource.PlayOneShot(_emptyShotSound);
                                }
                            }
                        }
                        else
                        {
                            PumpOff();
                        }
                }
                if (!_shotgunUsable && cordura <= _corduraMinEscopeta)
                {
                    _shotgunUsable = true;
                    StartCoroutine(ShowShotgunUnlockFeedback());
                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, _clientLayer))
            {
                Client client = hit.collider.GetComponent<Client>();

               
                    _client = client;
                    Pump();

            }   
        }

        if (_score < 0) _score = 0;
        _scoreText.text = "$ " + _score;
        _selectionText.text = "Tienes: " + _selectedDrink;


        if (cordura <= 0) 
        {
            StartCoroutine(Defeat());
        }
        if (cordura > 100) cordura = 100;
        _corduraText.text = "Cordura: " + cordura;

        if (_score >= _cashCondition)
        {
            GameStats.sanity = cordura;
            StartCoroutine(Win());
        }

        if (Input.GetKeyDown(KeyCode.A)) help = !help;

        vinetta(); 


        _corduraImageFill.fillAmount = cordura / 100f;

        if (!_shotgunUsable && cordura <= _corduraMinEscopeta)
        {
            _shotgunUsable = true;
            StartCoroutine(ShowShotgunUnlockFeedback());
        }
    }

    IEnumerator Defeat()
    {
        yield return new WaitForSeconds(0.7f);
        urp._shootURP.SetActive(false);
        urp._damageURP.SetActive(false);

        SceneTracker.ultimaEscenaJugable = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Lose");
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.25f); 
        urp._shootURP.SetActive(false);
        urp._damageURP.SetActive(false);

        if (_cargaEscenaWin != null)
        {
            _cargaEscenaWin.allowSceneActivation = true; 
        }
        else
        {
            SceneManager.LoadScene(_sceneWin); 
        }
    }

    void vinetta()
    {

        if (vignette != null)
        {
            float baseVignette = 1f - (cordura / 100f);// Inversamente proporcional a la vida      
            float oscillation = Mathf.Sin(Time.time * vignetteOscSpeed) * vignetteOscStrength;
            vignette.intensity.value = Mathf.Clamp01(baseVignette + oscillation);
        }
        if (chromAberration != null)
        {
            float baseAberration = 1f - (cordura / 100f);// Inversamente proporcional a la vida  
            float oscillation = Mathf.Sin(Time.time * aberrationOscSpeed) * aberrationOscStrength;

            chromAberration.intensity.value = Mathf.Clamp01(baseAberration + oscillation);
        }

    }

    void Pump()
    {
        if (_usePump)
        {
            if (_currentAmmo > 0)
            {

                _currentAmmo--;
                UpdateAmmoVisuals();
                urp.StartCoroutine(urp.ShootURP());
                _client.isDeath = true;

                if (_shotgunAudioSource != null && _shotgunSound != null)
                    _shotgunAudioSource.PlayOneShot(_shotgunSound);

                if (_client.imposter)
                {
                    GameStats.impostoresEliminados++;
                    MoreMoney(100);
                    StartCoroutine(correct());
                }
                else
                {
                    LessMoney(200);
                    cordura -= _corduraMatarCliente;
                    StartCoroutine(Incorrect());
                }

                StartCoroutine(Shoot());
            }
            else
            {
                if (_shotgunAudioSource != null && _emptyShotSound != null)
                {
                    _shotgunAudioSource.PlayOneShot(_emptyShotSound);
                }
            }
        }
    }

    public void MoreMoney(int money)
    {
        _score += money;
        contador.MostrarGanancia(money);
    }

    public void LessMoney(int money)
    {
        _score -= money;
        contador.DescontarGanancia(money);
    }


    void PumpOn()
    {

        _pumpHandAni.SetBool("Hand gets it", true);
        _pumpBarAni.SetBool("Bar gets it", false);
      
        meshPumpBar.enabled = false;
        meshPumpHand.enabled = true;

        _usePump = true;
        _selectedDrink = null;

        if (!_hasPlayedShotgunSound && _audioSourceFeedback != null && _soundDesbloqueoEscopeta != null)
        {
            _audioSourceFeedback.PlayOneShot(_soundDesbloqueoEscopeta);
            _hasPlayedShotgunSound = true;

            if (_objetoAActivar != null)
                _objetoAActivar.SetActive(true);

            if (_objetoADesactivar != null)
                _objetoADesactivar.SetActive(false);
        }
    }

    public void ReloadOneBullet()
    {
        if (_currentAmmo < _maxAmmo)
        {
            _currentAmmo += 2;
            UpdateAmmoVisuals();
        }
    }
    IEnumerator BlockPlayerInput(float duration)
    {
        _isBlocked = true;
        yield return new WaitForSeconds(duration);
        _isBlocked = false;
    }
    private void UpdateAmmoVisuals()
    {
        for (int i = 0; i < _ammoVisuals.Length; i++)
        {
            _ammoVisuals[i].SetActive(i < _currentAmmo);
        }
    }

    void PumpOff()
    {
        _pumpHandAni.SetBool("Hand gets it", false);
        _pumpBarAni.SetBool("Bar gets it", true);
        
        meshPumpBar.enabled = true;
        meshPumpHand.enabled = false;

        _usePump = false;
        _pumpCode = null;
    }
    IEnumerator ShowShotgunUnlockFeedback()
    {
        if (_shotgunUnlockFeedback == null) yield break;

        _shotgunUnlockFeedback.SetActive(true);

        Vector3 startPos = _shotgunUnlockFeedback.transform.position + Vector3.down * _fallDistance;
        Vector3 targetPos = _shotgunUnlockFeedback.transform.position;
        _shotgunUnlockFeedback.transform.position = startPos;

        float elapsed = 0f;
        while (elapsed < _fallDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _fallDuration);
            _shotgunUnlockFeedback.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        
        yield return new WaitForSeconds(2f);
        _shotgunUnlockFeedback.SetActive(false);
    }

    IEnumerator Shoot()
    {
        _smokeParticle.Play();
        yield return new WaitForSeconds(0.1f);
        ActivateDepthOfField(3f, 0.5f);
        PumpOff();
    }

    private void ActivateDepthOfField(float duration, float startDistance)
    {
        if (depthOfField == null) return;
        StartCoroutine(Activate(duration, startDistance));
    }

    private IEnumerator Activate(float duration, float startDistance)
    {
        depthOfField.active = true;
        depthOfField.focusDistance.value = startDistance;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            depthOfField.focusDistance.value = Mathf.Lerp(startDistance, 9f, elapsed / duration);
            yield return null;
        }
        depthOfField.active = false;
        depthOfField.focusDistance.value = 9f;
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
    IEnumerator PrecargarEscenaVictoria()
    {
        _cargaEscenaWin = SceneManager.LoadSceneAsync(_sceneWin);
        _cargaEscenaWin.allowSceneActivation = false;
        yield return null;
    }
}
