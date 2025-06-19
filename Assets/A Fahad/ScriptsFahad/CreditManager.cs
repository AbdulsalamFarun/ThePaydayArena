using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    [System.Serializable]
    public class CreditEntry
    {
        public string name;
        public string role;
    }

    public GameObject rollingCreditsObject; // ScrollView
    public RectTransform rollingContent;    // Content
    public float rollSpeed = 50f;

    public TextMeshProUGUI creditText;
    public List<CreditEntry> credits = new List<CreditEntry>();
    public float displayDuration = 3f;
    public float fadeDuration = 1f;

    private void Start()
    {
        rollingCreditsObject.SetActive(false); // <-- هذا مهم
        StartCoroutine(PlayCredits());
    }

    private IEnumerator PlayCredits()
    {
        foreach (CreditEntry entry in credits)
        {
            // ظهور تدريجي
            yield return StartCoroutine(FadeInText($"{entry.name}\n<size=70%>{entry.role}</size>"));
            yield return new WaitForSeconds(displayDuration);
            // اختفاء تدريجي
            yield return StartCoroutine(FadeOutText());
        }

        // إخفاء النص الفردي
        creditText.gameObject.SetActive(false);

        // إظهار الرول
        rollingCreditsObject.SetActive(true);

        // بدء تحريك الرول
        StartCoroutine(RollCredits());
    }

    IEnumerator FadeInText(string message)
    {
        creditText.text = message;
        creditText.alpha = 0f;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            creditText.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        creditText.alpha = 1f;
    }

    IEnumerator FadeOutText()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            creditText.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
        creditText.alpha = 0f;
    }
    
    private IEnumerator RollCredits()
{
    float startY = rollingContent.anchoredPosition.y;
    float targetY = rollingContent.sizeDelta.y;

    while (rollingContent.anchoredPosition.y < targetY + 500)
    {
        rollingContent.anchoredPosition += new Vector2(0, rollSpeed * Time.deltaTime);
        yield return null;
    }

    // بعد الانتهاء، تقدر تروح لمشهد أو تظهر رسالة
    Debug.Log("انتهت الكريديت!");
    
    // مثلاً: عرض رسالة النهاية (تحتاج تكون UI ثانية غير creditsText اللي أخفيته)
    // أو تروح للمينيو
    SceneManager.LoadSceneAsync("UI_MainMenu_Sami");

    // أو تفعل زر
    // yourButton.SetActive(true);
}

}
