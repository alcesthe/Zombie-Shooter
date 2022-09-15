using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    public float transitionTime = 3f;
    [SerializeField] GameObject loaderCanvas;
    private GameManager gameManager;
    private Player player;
     
/*    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }*/

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public async void LoadScene(string sceneName)
    {
        Time.timeScale = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);
        await Task.Delay(5000);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadStartScene()
    {
        LoadScene("StartScene");
    }

    public void LoadEndScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadScene("EndScene");
    }

    public void LoadCoreScene()
    {
        LoadScene("CoreScene");
    }

    public void RestartCoreScene()
    {
        Destroy(gameManager);
        LoadCoreScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
