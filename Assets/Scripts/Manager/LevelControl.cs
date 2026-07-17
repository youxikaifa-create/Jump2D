using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public static LevelControl Instance;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public int nowLevel;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowCurrentLevel();
    }

    //隐藏所有，只显示当前关卡=覆盖
    void ShowCurrentLevel()
    {
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        switch (nowLevel)
        {
            case 0: level1.SetActive(true); break;
            case 1: level2.SetActive(true); break;
            case 2: level3.SetActive(true); break;
        }
    }

    //通关调用
    public void LevelWin()
    {
        //标记通关
        SaveManager.Instance.saveData.levelList[nowLevel].isClear = true;

        //解锁下一关
        if (nowLevel < 2)
        {
            SaveManager.Instance.saveData.levelList[nowLevel + 1].isUnlock = true;
            nowLevel++;
        }

        SaveManager.Instance.SaveGame();
        LevelUI.Instance.RefreshButton();//刷新按钮的状态
        ShowCurrentLevel();//nowLevel++;自增展示下一关
    }

    //关卡按钮跳转
    public void GoLevel(int index)
    {
        if (SaveManager.Instance.saveData.levelList[index].isUnlock)
        {
            nowLevel = index;
            ShowCurrentLevel();
        }
    }

    public void SavenowLevel()
    {
        PlayerPrefs.SetInt("NowLevel", nowLevel);
        PlayerPrefs.Save();
    }

    public void GetnowLevel()
    {
        if (PlayerPrefs.HasKey("NowLevel"))
        {
            nowLevel = PlayerPrefs.GetInt("NowLevel");
        }
        else
        {
            nowLevel = 0;//限制了开始游戏后，只能从第一关开始，Unity编辑器修改无效
        }
    }
}