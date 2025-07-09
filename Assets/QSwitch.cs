using UnityEngine;

public class QSwitch : MonoBehaviour
{
    [Header("Script a activar")]
    [SerializeField] private MonoBehaviour scriptToEnable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (scriptToEnable != null)
                scriptToEnable.enabled = true;

            this.enabled = false;
        }
    }
}
