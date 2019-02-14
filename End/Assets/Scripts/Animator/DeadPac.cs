using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class DeadPac : MonoBehaviour
{
    private static DeadPac _instance;


    private void Awake()
    {
        _instance = this;
    }

    public static DeadPac Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    [Range(0, 1)]
    public float darkPercent;
    public Color color;
    public Shader shader;
    private Material curMaterial;



    public Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(shader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    void Start()
    {
        if (SystemInfo.supportsImageEffects == false)
        {
            enabled = false;
            return;
        }

        if (shader != null && shader.isSupported == false)
        {
            enabled = false;
        }
    }

    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }

    void Update()
    {
        color = Color.Lerp(Color.white, Color.black, darkPercent);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (shader != null)
        {
            material.SetColor("_Color", color);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }


}