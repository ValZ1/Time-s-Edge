using UnityEngine;

public class DamageBlinkEffect : MonoBehaviour
{
    [SerializeField] public SpriteRenderer characterRenderer; // Ссылка на SpriteRenderer персонажа
    [SerializeField] public float blinkDuration = 0.5f; // Общая длительность мигания
    [SerializeField] public float blinkInterval = 0.1f; // Интервал между сменами цвета

    private Color originalColor; // Исходный цвет персонажа

    private void Awake()
    {
        if (characterRenderer == null)
        {
            // Попытаться автоматически найти SpriteRenderer, если не установлен в инспекторе
            characterRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (characterRenderer != null)
        {
            originalColor = characterRenderer.color;
        }
        else
        {
            Debug.LogError("DamageBlinkEffect: SpriteRenderer не найден!");
        }
    }

    // Метод для запуска эффекта мигания
    public void StartBlink()
    {
        if (characterRenderer != null)
        {
            StopAllCoroutines(); // Остановить все предыдущие эффекты мигания
            StartCoroutine(BlinkEffect());
        }
    }

    private System.Collections.IEnumerator BlinkEffect()
    {
        float elapsedTime = 0f;
        bool isWhite = false;

        while (elapsedTime < blinkDuration)
        {
            // Переключаем цвет между белым и исходным
            characterRenderer.color = isWhite ? originalColor : Color.white;
            isWhite = !isWhite;

            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // Убедимся, что в конце возвращается исходный цвет
        characterRenderer.color = originalColor;
    }
}
