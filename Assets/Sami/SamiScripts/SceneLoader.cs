using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /* ========== أسامي المشاهد ========== */
    [Header("Scene names (exactly as in Build Settings)")]
    [SerializeField] private string mainMenuScene = "UI_MainMenu_Sami";   // NEW ✨
    [SerializeField] private string playScene     = "GameScene";
    [SerializeField] private string optionsScene  = "Options";
    [SerializeField] private string creditsScene  = "Credits";

    /* ========== أزرار القائمة الرئيسية ========== */
    public void PlayGame()        => LoadSceneByName(playScene);
    public void OpenOptions()     => LoadSceneByName(optionsScene);
    public void OpenCredits()     => LoadSceneByName(creditsScene);
    public void QuitGame()        => Application.Quit();

    /* ========== زر إضافي (Next) ========== */
    public void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            Debug.LogWarning("ما فيه مشهد بعد الحالي!");
    }

    /* ========== زر “رجوع للقائمة” ========== */
    public void ReturnToMainMenu() => LoadSceneByName(mainMenuScene);   // NEW ✨

    /* ========== Helpers ========== */
    private void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("اسم المشهد فاضي أو غلط!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
