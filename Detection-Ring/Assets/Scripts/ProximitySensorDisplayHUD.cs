using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximitySensorDisplayHUD : MonoBehaviour
{
    [SerializeField] private Vector2Int _textureSize = new Vector2Int(512, 512);
    [SerializeField] private ProximitySensorB _proximitySensor;
    [SerializeField] private RawImage _image;
    [SerializeField] private Gradient _colourGradient;
    [SerializeField] private Material _radarMaterial;

    private CameraSwitcher _cameraSwitcher;

    private void OnEnable()
    {
        _proximitySensor.OnSetIntensityValues += UpdateShader;

        _radarMaterial.SetTexture("Texture", CreateConicGradeientTexture(_textureSize, 360f / _proximitySensor.IntensityValues.Length));
    }

    private void OnDisable()
    {
        _proximitySensor.OnSetIntensityValues -= UpdateShader;
    }

    //private void OnValidate()
    //{
    //    _radarMaterial.SetTexture("_MainTex", CreateConicGradeientTexture(_textureSize, 360f / _proximitySensor.Nodes.Length));
    //}

    private void Awake()
    {
        _cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        _radarMaterial.SetTexture("_MainTex", CreateConicGradeientTexture(_textureSize, 360f / _proximitySensor.IntensityValues.Length));
    }

    private void Update()
    {
        float z = CameraManager.Current.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }

    private void UpdateShader(float[] nodes)
    {
        _radarMaterial.SetFloatArray("_Segments", nodes);
        _radarMaterial.SetInt("_SegmentsCount", nodes.Length);
    }

    public Texture2D CreateConicGradeientTexture(Vector2Int size, float angle)
    {
        Texture2D texture = new Texture2D(size.x, size.y, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Bilinear;

        float rise, run;
        float r = 0f;
        float g = 0f;
        Vector2Int midPoint = size / 2;
        float maxDistance = size.magnitude;

        for (int y = 0; y < size.y; y++)
        {
            rise = midPoint.y - y;
            run = midPoint.x;

            for (int x = 0; x < size.x; x++)
            {
                // Conic gradeient;
                r = Mathf.Atan2(run, -rise) * Mathf.Rad2Deg;
                r = (r + 360f) % 360f;
                r /= 360f;

                // Distance
                g = Vector2Int.Distance(new Vector2Int(x, y), midPoint) * 0.01f;

                Color col = new Color(1f-r, g, 0f);
                texture.SetPixel(x, y, col);
                run--;
            }
        }

        texture.Apply();
        return texture;
    }
}