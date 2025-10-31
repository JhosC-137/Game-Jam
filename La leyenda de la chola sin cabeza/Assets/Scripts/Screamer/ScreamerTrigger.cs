using UnityEngine;

public class ScreamerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject screamerPanel;
    [SerializeField] private AudioSource screamerSound;
    [SerializeField] private string playerTag = "Borracho1_0";
    [SerializeField] private float screamerDuration = 3f; // Tiempo que dura el screamer en pantalla

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

            // Detener el tiempo si quieres congelar el juego
            Time.timeScale = 0f;

            // Si quieres que el screamer desaparezca después de unos segundos
            Invoke(nameof(DesactivarScreamer), screamerDuration);
        }
    }

    void DesactivarScreamer()
    {
        if (screamerPanel != null)
            screamerPanel.SetActive(false);

        // Reanudar el tiempo si lo habías detenido
        Time.timeScale = 1f;
    }
}