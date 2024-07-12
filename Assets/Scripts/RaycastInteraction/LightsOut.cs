using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class LightsOut : MonoBehaviour, IInteractable
{
    public RawImage rawImage;
    public Texture newTexture;
    private Texture originalTexture;
    private List<GameObject> flashlights = new List<GameObject>();

    private bool isToggled = false;
    private Color originalAmbientLight;
    private Light[] directionalLights;
    private bool[] originalLightStates;

    void Start()
    {
        originalAmbientLight = RenderSettings.ambientLight;
        directionalLights = FindObjectsOfType<Light>();
        originalLightStates = new bool[directionalLights.Length];

        for (int i = 0; i < directionalLights.Length; i++)
        {
            if (directionalLights[i].type == LightType.Directional)
            {
                originalLightStates[i] = directionalLights[i].enabled;
            }
        }

        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.hideFlags == HideFlags.None && t.gameObject.name == "flashlight")
            {
                flashlights.Add(t.gameObject);
            }
        }

        foreach (GameObject flashlight in flashlights)
        {
            flashlight.SetActive(false);
        }

        if (rawImage != null)
        {
            originalTexture = rawImage.texture;
        }
    }

    public void ChangeTextureAtRuntime(Texture texture)
    {
        if (texture != null)
        {
            rawImage.texture = texture;
        }
        else
        {
            Debug.LogError("Provided texture is null.");
        }
    }

    public void RevertToOriginalTexture()
    {
        if (originalTexture != null)
        {
            rawImage.texture = originalTexture;
        }
    }

    public void Interact()
    {
        isToggled = !isToggled;

        ToggleLights(isToggled);
    }

    private void ToggleLights(bool toggled)
    {
        isToggled = toggled;

        if (isToggled)
        {
            // Set ambient light to a darker color
            RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.1f); // Adjust as needed

            // Disable directional lights
            foreach (Light light in directionalLights)
            {
                if (light.type == LightType.Directional)
                {
                    light.enabled = false;
                }
            }

            // Enable flashlights
            foreach (GameObject flashlight in flashlights)
            {
                flashlight.SetActive(true);
            }

            // Change texture to new texture
            ChangeTextureAtRuntime(newTexture);
        }
        else
        {
            // Restore original ambient light
            RenderSettings.ambientLight = originalAmbientLight;

            // Restore directional lights
            for (int i = 0; i < directionalLights.Length; i++)
            {
                if (directionalLights[i].type == LightType.Directional)
                {
                    directionalLights[i].enabled = originalLightStates[i];
                }
            }

            // Disable flashlights
            foreach (GameObject flashlight in flashlights)
            {
                flashlight.SetActive(false);
            }

            // Revert to original texture
            RevertToOriginalTexture();
        }

        Debug.Log($"Lights toggled to {isToggled}. Ambient light: {RenderSettings.ambientLight}");
    }
}
