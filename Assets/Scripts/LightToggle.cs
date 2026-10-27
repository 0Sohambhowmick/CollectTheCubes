
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light targetLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && targetLight != null)
        {
            targetLight.enabled = !targetLight.enabled;
        }
    }
}
