using UnityEngine;

public class HpBarffects : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private RectTransform healthBarAnimated;

    [Header("Animación - Valores máximos")]
    [SerializeField] private float maxPulseMagnitude = 0.1f;
    [SerializeField] private float maxShakeMagnitude = 6f;
    [SerializeField] private float maxPulseSpeed = 10f;

    [Header("Brillo (color de la imagen)")]
    [SerializeField] private UnityEngine.UI.Image healthImage; // La imagen a la que se le cambiará el color (debería ser la misma barra)
    [SerializeField] private Color maxGlowColor = new Color(1f, 1f, 1f, 1f); // Blanco brillante
    [SerializeField] private float glowPulseSpeed = 4f;
    [SerializeField] private float maxGlowIntensity = 0.4f;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Player player;

    void Start()
    {
        if (healthBarAnimated == null)
        {
            Debug.LogError("¡Falta asignar 'healthBarAnimated'!");
            return;
        }

        originalScale = healthBarAnimated.localScale;
        originalPosition = healthBarAnimated.localPosition;

        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("No se encontró ningún objeto con el script 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float cordura = Mathf.Clamp(player._cordura, 0f, 100f);
        float t = 1f - (cordura / 100f); // t = 0 si tiene 100 de vida, t = 1 si tiene 0

        // Gradualmente aumenta la intensidad con t
        float pulseMagnitude = Mathf.Lerp(0f, maxPulseMagnitude, t);
        float shakeMagnitude = Mathf.Lerp(0f, maxShakeMagnitude, t);
        float pulseSpeed = Mathf.Lerp(0f, maxPulseSpeed, t);

        if (cordura < 100f) // Evita calcular si está en estado normal
        {
            // Pulso (escala)
            float scaleFactor = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseMagnitude;
            healthBarAnimated.localScale = originalScale * scaleFactor;

            // Temblores (posición)
            Vector2 offset = Random.insideUnitCircle * shakeMagnitude;
            healthBarAnimated.localPosition = originalPosition + new Vector3(offset.x, offset.y, 0);
        }
        else
        {
            // Reset visual
            healthBarAnimated.localScale = originalScale;
            healthBarAnimated.localPosition = originalPosition;
        }

        if (healthImage != null)
        {
            float glow = Mathf.Sin(Time.time * glowPulseSpeed) * 0.5f + 0.5f; // oscila entre 0 y 1
            float glowStrength = Mathf.Lerp(0f, maxGlowIntensity, t) * glow;

            Color baseColor = Color.white;
            Color glowColor = Color.Lerp(baseColor, maxGlowColor, glowStrength);
            healthImage.color = glowColor;
        }

    }
}
