using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light luz;
    public float min = 2f, max = 4f, velocidad = 3f;

    void Update()
    {
        float n = Mathf.PerlinNoise(Time.time * velocidad, 0f);
        luz.intensity = Mathf.Lerp(min, max, n);
    }
}