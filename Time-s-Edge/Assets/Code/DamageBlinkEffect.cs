using UnityEngine;

public class DamageBlinkEffect : MonoBehaviour
{
    [SerializeField] public SpriteRenderer characterRenderer; // ������ �� SpriteRenderer ���������
    [SerializeField] public float blinkDuration = 0.5f; // ����� ������������ �������
    [SerializeField] public float blinkInterval = 0.1f; // �������� ����� ������� �����

    private Color originalColor; // �������� ���� ���������

    private void Awake()
    {
        if (characterRenderer == null)
        {
            // ���������� ������������� ����� SpriteRenderer, ���� �� ���������� � ����������
            characterRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (characterRenderer != null)
        {
            originalColor = characterRenderer.color;
        }
        else
        {
            Debug.LogError("DamageBlinkEffect: SpriteRenderer �� ������!");
        }
    }

    // ����� ��� ������� ������� �������
    public void StartBlink()
    {
        if (characterRenderer != null)
        {
            StopAllCoroutines(); // ���������� ��� ���������� ������� �������
            StartCoroutine(BlinkEffect());
        }
    }

    private System.Collections.IEnumerator BlinkEffect()
    {
        float elapsedTime = 0f;
        bool isWhite = false;

        while (elapsedTime < blinkDuration)
        {
            // ����������� ���� ����� ����� � ��������
            characterRenderer.color = isWhite ? originalColor : Color.white;
            isWhite = !isWhite;

            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // ��������, ��� � ����� ������������ �������� ����
        characterRenderer.color = originalColor;
    }
}
