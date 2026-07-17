using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int level;

    public Text scoreText;//用Text组件赋值
    public int score = 0;//暂时改为public方便存储分

    public GameObject pausePanel;
    private bool isPause = false;

    public PlayerHealth playerhealth;

    [Header("视频播放器设置")]
    public VideoPlayer videoPlayer;       // 拖拽赋值你的VideoPlayer组件
    public RenderTexture videoRenderTex;  // 视频渲染纹理（用来清屏）
    public GameObject videoCanvas;         // 视频显示的UI画布/对象

    void Awake()
    {
        //Instance = this;//脚本组件引用,单利初始化
        //单例初始化，保证场景里只有一个GameManager
        if (Instance == null)
        {
            Instance = this;//脚本组件引用
            //DontDestroyOnLoad(gameObject); // 切换场景不销毁,但是会导致某些脚本里的某些游戏对象Missing
        }
        else
        {
            Destroy(gameObject);
        }
        //出大问题了，重新开始只能点一下

        GetAll();
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;

        // 新增：注册视频播放完成事件
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoPlayEnd;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    public void GetScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0;
        }
    }

    void Update()
    {
        //ESC暂停/继续
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPause = !isPause;//保证按一下暂停，再按一下继续
        if (isPause)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            BGMPlayer.Instance.audioSource.Pause();//暂停时音乐也暂停
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            BGMPlayer.Instance.audioSource.Play();//继续上次暂停后播放
        }
    }

    //重新开始本局
    public void RestartGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        OnVideoPlayEnd(videoPlayer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新加载当前场景
    }

    //存储一切属性
    public void SaveAll()
    {
        SaveScore();
        playerhealth.SaveHP();
        playerhealth.SavePosition();
        LevelControl.Instance.SavenowLevel();
    }
    public void GetAll()
    {
        GetScore();
        playerhealth.GetHp();
        playerhealth.GetPosition();
        LevelControl.Instance.GetnowLevel();
    }

    //返回主菜单
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");//改成你自己场景名
    }

    //可选：关卡初始化方法，比如从PlayerPrefs读取
    //public void LoadLevelProgress()
    //{
    //    level = PlayerPrefs.GetInt("NowLevel", 0); // 默认从第1关开始
    //}

    //开始播放视频
    public void PlayOpeningVideo()
    {
        if (videoPlayer == null) return;

        // 显示视频画布、准备播放
        videoCanvas.SetActive(true);
        videoPlayer.Play();
    }

    //视频播放完毕回调：清除画面，不留最后一帧
    private void OnVideoPlayEnd(VideoPlayer vp)//视频播完自动触发 OnVideoPlayEnd
    {
        // 1. 停止视频播放
        vp.Stop();

        // 2. 清空渲染纹理（关键：彻底清除最后一帧画面）
        if (videoRenderTex != null)
        {
            RenderTexture.active = videoRenderTex;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = null;
        }

        // 3. 隐藏视频画布，回到游戏画面
        videoCanvas.SetActive(false);
    }
    

    // 可选：销毁时取消事件订阅，避免内存泄漏
    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoPlayEnd;
        }
    }
}