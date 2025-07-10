using UnityEngine;

public class Vasos : MonoBehaviour
{
    [System.Serializable]
    public class CorduraBreak
    {
        public GameObject targetObject;
        public AudioClip breakSound;
        [HideInInspector] public bool triggered = false;
    }

    [Header("Dependencias")]
    [SerializeField] private Player player;

    [Header("Configuración")]
    [SerializeField] private CorduraBreak[] corduraBreaks;
    [SerializeField] private float startingCordura = 100f;
    [SerializeField] private float corduraStep = 10f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float basePitch = 1f;
    [SerializeField] private float pitchStep = 0.1f;

    void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return;

        float currentCordura = Mathf.Clamp(player._cordura, 0f, startingCordura);

        for (int i = 0; i < corduraBreaks.Length; i++)
        {
            float threshold = startingCordura - corduraStep * (i + 1);
            if (currentCordura <= threshold && !corduraBreaks[i].triggered)
            {
                TriggerBreak(i);
            }
        }
    }

    void TriggerBreak(int index)
    {
        CorduraBreak cb = corduraBreaks[index];
        cb.triggered = true;

        if (cb.targetObject != null)
            cb.targetObject.SetActive(false);

        if (cb.breakSound != null)
        {
            audioSource.pitch = basePitch + pitchStep * index;
            audioSource.PlayOneShot(cb.breakSound);
        }
    }
}
