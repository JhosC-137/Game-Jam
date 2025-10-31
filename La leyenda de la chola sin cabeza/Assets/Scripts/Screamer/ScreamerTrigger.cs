using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreamerTrigger : MonoBehaviour
{
    [Header("Referencias visuales y de audio")]
    [SerializeField] private GameObject screamerPanel;
    [SerializeField] private CanvasGroup screamerCanvasGroup;
    [SerializeField] private AudioSource screamerSound;

    [Header("Configuración de activación")]
    [SerializeField] private string playerTag = "Borracho1_0";
    [SerializeField] private float screamerDuration = 3f;
    [SerializeField] private float fadeSpeed = 1f;

    [Header("Escena de destino")]
    [SerializeField] private string menuSceneName = "Menu";

    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag(playerTag))
        {
            hasTriggered = true;
            Debug.Log("🎃 Screamer activado por: " + other.name);

            if (screamerPanel != null)
                screamerPanel.SetActive(true);
            else
                Debug.LogWarning("⚠️ screamerPanel no está asignado");

            if (screamerSound != null)
                screamerSound.Play();
            else
                Debug.LogWarning("⚠️ screamerSound no está asignado");

            StartCoroutine(FadeInScreamer());
        }
    }

    IEnumerator FadeInScreamer()
    {
        float alpha = 0f;
        Time.timeScale = 0f;

        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.unscaledDeltaTime;
            screamerCanvasGroup.alpha = Mathf.Clamp01(alpha);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(screamerDuration);

        if (screamerPanel != null)
            screamerPanel.SetActive(false); // Oculta el screamer antes de cambiar de escena

        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName, LoadSceneMode.Single);
    }
}