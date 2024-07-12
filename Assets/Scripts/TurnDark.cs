using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDark : MonoBehaviour
{
 // Start is called before the first frame update
    void Start()
    {
        TurnSceneDark();
    }

    void TurnSceneDark()
    {
        // Set the ambient light color to black
        RenderSettings.ambientLight = Color.black;

        // Find all directional lights in the scene and turn them off
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light.type == LightType.Directional)
            {
                light.enabled = false;
            }
        }
    }
}
