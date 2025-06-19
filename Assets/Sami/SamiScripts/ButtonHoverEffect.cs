using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// يُطبِّق تأثير تكبير وتغيير لون النص عند مرور المؤشر على الزر.
/// - يحفظ حجم الزر الأصلي كما هو في الـ Editor.
/// - يطبّق التكبير بالنسبة للحجم الأصلي.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ButtonHoverEffect : MonoBehaviour,
                                 IPointerEnterHandler,
                                 IPointerExitHandler,
                                 ISelectHandler,
                                 IDeselectHandler
{
    /*----------- إعدادات الألوان -----------*/
    [Header("Color Settings")]
    [Tooltip("لون النص في الحالة العادية.")]
    public Color normalColor    = new Color(1f, 0.90f, 0.80f);  // بيج
    [Tooltip("لون النص أثناء الـ Hover.")]
    public Color highlightColor = new Color(1f, 0.84f, 0.00f);  // ذهبي

    /*----------- إعدادات التكبير -----------*/
    [Header("Scale Settings")]
    [Tooltip("نسبة التكبير عند الـ Hover (مثلاً 1.10 = 10٪ تكبير).")]
    [Range(1f, 2f)]
    public float hoverScale = 1.10f;
    [Tooltip("سرعة الانتقال بين المقاسين.")]
    public float scaleSpeed = 12f;

    /*----------- مراجع خاصة -----------*/
    private TextMeshProUGUI text;      // مرجع للنص
    private RectTransform   rectTf;    // مرجع لـ RectTransform

    private Vector3 initialScale;      // حجم الزر الأصلي وقت التشغيل
    private Vector3 targetScale;       // الحجم الهدف في كل فريم

    /*----------- التهيئة -----------*/
    private void Awake()
    {
        rectTf = GetComponent<RectTransform>();
        text   = GetComponentInChildren<TextMeshProUGUI>(true);

        // خزّن الحجم الأصلي كمرجع
        initialScale = rectTf.localScale;
        targetScale  = initialScale;

        if (text != null)
            text.color = normalColor;
    }

    /*----------- التحديث كل فريم -----------*/
    private void Update()
    {
        // حَرِّك الحجم بسلاسة نحو الحجم الهدف
        rectTf.localScale = Vector3.Lerp(rectTf.localScale,
                                         targetScale,
                                         Time.unscaledDeltaTime * scaleSpeed);
    }

    /*----------- دخول المؤشر -----------*/
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (text != null)
            text.color = highlightColor;

        // حجم الزر = الحجم الأصلي × نسبة التكبير
        targetScale = initialScale * hoverScale;
    }

    /*----------- خروج المؤشر -----------*/
    public void OnPointerExit(PointerEventData eventData)
    {
        if (text != null)
            text.color = normalColor;

        // ارجع للحجم الأصلي
        targetScale = initialScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (text != null)
            text.color = highlightColor;

        // حجم الزر = الحجم الأصلي × نسبة التكبير
        targetScale = initialScale * hoverScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (text != null)
            text.color = normalColor;

        // ارجع للحجم الأصلي
        targetScale = initialScale;
    }
}
