using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour
{
    [System.Serializable]
    public class GlowingObject
    {
        public Transform targetTransform;
        public Renderer renderer;
        public bool enableShake = true;

        [HideInInspector] public Vector3 originalPosition;
        [HideInInspector] public bool isGlowing;
        [HideInInspector] public float shakeTimer;
    }

    [Header("Configuración visual")]
    [SerializeField] private List<GlowingObject> glowingObjects = new List<GlowingObject>();
    [SerializeField] private float glowIntensity = 2f;
    [SerializeField] private Color glowColor = Color.yellow;

    [Header("Configuración de sacudido")]
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.05f;
    [SerializeField] private float shakeSpeed = 0.025f;

    private GlowingObject currentGlowing = null;

    private void Start()
    {
        foreach (var obj in glowingObjects)
        {
            obj.originalPosition = obj.targetTransform.localPosition;
        }
    }
    private void Update()
    {
        DetectMouseHover();
        HandleShaking();
    }
    private void DetectMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            foreach (var obj in glowingObjects)
            {
                if (hit.transform == obj.targetTransform)
                {
                    if (Input.GetMouseButton(0))
                    {
                        DisableEffects(obj);
                        return;
                    }

                    if (currentGlowing != obj)
                    {
                        if (currentGlowing != null)
                            DisableEffects(currentGlowing);

                        EnableGlow(obj);
                        if (obj.enableShake)
                            StartShake(obj);
                    }
                    return;
                }
            }
        }

        if (currentGlowing != null)
        {
            DisableEffects(currentGlowing);
        }
    }

    private void HandleShaking()
    {
        foreach (var obj in glowingObjects)
        {
            if (obj.shakeTimer > 0f)
            {
                obj.shakeTimer -= Time.deltaTime;
                float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude;
                float offsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeMagnitude;
                obj.targetTransform.localPosition = obj.originalPosition + new Vector3(offsetX, offsetY, 0);
            }
            else if (obj.isGlowing)
            {
                obj.targetTransform.localPosition = obj.originalPosition;
            }
        }
    }

    private void EnableGlow(GlowingObject obj)
    {
        currentGlowing = obj;
        obj.isGlowing = true;
        obj.renderer.material.EnableKeyword("_EMISSION");
        obj.renderer.material.SetColor("_EmissionColor", glowColor * glowIntensity);
    }

    private void DisableEffects(GlowingObject obj)
    {
        obj.isGlowing = false;
        obj.shakeTimer = 0f;
        obj.targetTransform.localPosition = obj.originalPosition;
        obj.renderer.material.SetColor("_EmissionColor", Color.black);
        currentGlowing = null;
    }

    private void StartShake(GlowingObject obj)
    {
        obj.shakeTimer = shakeDuration;
    }
}
