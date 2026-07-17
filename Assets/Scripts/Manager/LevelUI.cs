using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public static LevelUI Instance;
    public Button[] levelBtns;//按钮数组

    private void Awake()
    {
        Instance = this;
    }
    void Start()//关卡选择界面每次被激活的时候(瞬间)，调用一次
    {
        RefreshButton();
        Debug.Log("刷新按钮!");
    }

    //for循环批量刷新按钮,读取存档里的isUnlock状态，从而设置按钮是否可点击
    public void RefreshButton()
    {
        for (int i = 0; i < 3; i++)
        {
            levelBtns[i].interactable = SaveManager.Instance.saveData.levelList[i].isUnlock;
        }
    }
}