using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI minText;
    [SerializeField] private TextMeshProUGUI maxText;
    [SerializeField] private TextMeshProUGUI avgText;

    [Header("Settings")]
    [SerializeField] private float updateInterval = 0.5f; // cada cuánto actualizar la UI (segundos)

    private float timeSinceLastUpdate;
    private int frameCount;
    private float totalFPS;

    private float minFPS = float.MaxValue;
    private float maxFPS = float.MinValue;
    private float avgFPS;

    private void Update()
    {
        // calcular fps instantáneo
        float currentFPS = 1f / Time.unscaledDeltaTime;

        // acumular datos
        frameCount++;
        totalFPS += currentFPS;
        if (currentFPS < minFPS) minFPS = currentFPS;
        if (currentFPS > maxFPS) maxFPS = currentFPS;

        timeSinceLastUpdate += Time.unscaledDeltaTime;

        // actualizar la UI cada cierto intervalo (para evitar jitter visual)
        if (timeSinceLastUpdate >= updateInterval)
        {
            avgFPS = totalFPS / frameCount;

            if (minText) minText.text = $"Min: {minFPS:F1}";
            if (maxText) maxText.text = $"Max: {maxFPS:F1}";
            if (avgText) avgText.text = $"Avg: {avgFPS:F1}";

            // reiniciar acumuladores
            timeSinceLastUpdate = 0f;
            frameCount = 0;
            totalFPS = 0f;
        }
    }

    [ContextMenu("Reset Stats")]
    public void ResetStats()
    {
        minFPS = float.MaxValue;
        maxFPS = float.MinValue;
        avgFPS = 0f;
        frameCount = 0;
        totalFPS = 0f;

        if (minText) minText.text = "Min: --";
        if (maxText) maxText.text = "Max: --";
        if (avgText) avgText.text = "Avg: --";
    }
}