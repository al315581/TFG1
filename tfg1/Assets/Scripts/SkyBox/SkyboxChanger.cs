using UnityEngine;
using UnityEngine.UI;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] Skyboxes;
    private Dropdown _dropdown;
    public int skybox = 0;

    public void Awake()
    {
       // _dropdown = GetComponent<Dropdown>();
        //var options = Skyboxes.Select(skybox => skybox.name).ToList();
        //_dropdown.AddOptions(options);
    }

    public void Start()
    {
        RenderSettings.skybox = Skyboxes[skybox];
    }

    public void ChangeSkybox()
    {
        if(skybox < Skyboxes.Length)
        {
            skybox += 1;
        }
        else
        {
            skybox = 0;
        }

        RenderSettings.skybox = Skyboxes[skybox];
        RenderSettings.skybox.SetFloat("_Rotation", 10);

    }

    public void ResetSkybox()
    {
        skybox = 0;
        RenderSettings.skybox = Skyboxes[skybox];

    }

}