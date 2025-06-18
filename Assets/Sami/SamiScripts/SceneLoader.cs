using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;   // <— NEW ⭐️

public class SceneLoader : MonoBehaviour
{
    /* ====== أسماء المشاهد ====== */
    [Header("Scene names (exactly as in Build Settings)")]
    [SerializeField] private string mainMenuScene = "UI_MainMenu_Sami";
    [SerializeField] private string playScene     = "GameScene";
    [SerializeField] private string optionsScene  = "Options";
    [SerializeField] private string creditsScene  = "Credits";

    /* ====== إعدادات الـTimeline ====== */
    [Header("Timeline Auto-Advance")]
    [Tooltip("حط الـPlayableDirector حق التيم لاين هنا (اختياري)")]
    [SerializeField] private PlayableDirector director;

    [Tooltip("إذا فعلته، بيحمّل المشهد التالي مباشرة بعد انتهاء التيم لاين")]
    [SerializeField] private bool loadNextWhenTimelineStops = false;

    /* ====== Mono ====== */
    private void Start()
    {
        // لو المستخدم مفعّل الخاصية، اربط الحدث
        if (loadNextWhenTimelineStops && director != null)
        {
            director.stopped += OnTimelineStopped;
            director.Play();
        }
    }

    /* ====== دوال أزرار المينيو ====== */
    public void PlayGame()        => LoadSceneByName(playScene);
    public void OpenOptions()     => LoadSceneByName(optionsScene);
    public void OpenCredits()     => LoadSceneByName(creditsScene);
    public void ReturnToMainMenu() => LoadSceneByName(mainMenuScene);
    public void QuitGame()        => Application.Quit();

    /* ====== زر/دالة Next Scene عادي ====== */
    public void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            Debug.LogWarning("🚧 ما فيه مشهد بعد الحالي!");
    }

    /* ====== يُستدعى تلقائيًا بعد Timeline ====== */
    private void OnTimelineStopped(PlayableDirector _) => LoadNextScene();

    /* ====== لو تبي تستدعيها عن طريق Signal داخل الـTimeline ====== */
    public void LoadNextSceneFromSignal() => LoadNextScene();

    /* ====== Helpers ====== */
    private void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("❌ اسم المشهد فاضي أو غلط!");
            return;
        }
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void OnDisable()
    {
        director.stopped -= OnTimelineStopped;
    }
}
