using UnityEngine;
using TMPro;

public class TextGlitch : MonoBehaviour
{
    public TMP_Text text;       // TextMeshPro ÄÄÆ÷³ÍÆ®
    public float intensity = 1f; // Èçµé¸² °­µµ
    public float speed = 50f;    // Èçµé¸² ¼Óµµ

    Vector3 originalPos;

    void Start()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
        originalPos = text.rectTransform.localPosition;
    }

    void Update()
    {
        float offsetX = Mathf.PerlinNoise(Time.time * speed, 0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time * speed) - 0.5f;
        text.rectTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0) * intensity;
    }
}
