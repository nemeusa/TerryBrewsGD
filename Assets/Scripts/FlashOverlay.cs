using UnityEngine;

public class FlashOverlay : MonoBehaviour
{
    public Material flashMaterial;
    private float timer = 0f;
    private bool isFlashing = false;

    void Update()
    {
        if (isFlashing)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01((3f - timer) / 3f); // 1 → 0 en 3s
            flashMaterial.SetFloat("_Alpha", alpha);

            if (timer >= 3f)
            {
                isFlashing = false;
                timer = 0f;
            }
        }
    }

    public void TriggerFlash()
    {
        isFlashing = true;
        timer = 0f;
        flashMaterial.SetFloat("_Alpha", 1f);
    }
}
