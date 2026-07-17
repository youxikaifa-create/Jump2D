using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    //继续游戏按钮绑定
    public void ContinuePlayGame()
    {
        SceneManager.LoadScene("Game");//你的游戏场景名
    }

    public void RestartPlayGame()//开始游戏
    {
        //PlayerPrefs.DeleteKey("Score");
        //PlayerPrefs.DeleteKey("PlayerHP");
        //PlayerPrefs.DeleteKey("PlayerPosX");
        //PlayerPrefs.DeleteKey("PlayerPosY");
        //PlayerPrefs.DeleteKey("NowLevel");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene("Game");
    }

    //退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
}