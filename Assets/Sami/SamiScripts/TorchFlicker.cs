using UnityEngine;

[RequireComponent(typeof(Light))]
public class TorchFlicker : MonoBehaviour
{
    [Header("Intensity Settings")]
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.3f;

    [Header("Flicker Speed")]
    public float flickerSpeed = 0.1f;

    private Light torchLight;
    private float targetIntensity;
    private float timer;

    void Start()
    {
        torchLight = GetComponent<Light>();
        PickNewIntensity();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            PickNewIntensity();
        }

        torchLight.intensity = Mathf.Lerp(torchLight.intensity, targetIntensity, Time.deltaTime * 10f);
    }

    void PickNewIntensity()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        timer = Random.Range(flickerSpeed * 0.5f, flickerSpeed * 1.5f);
    }
}
