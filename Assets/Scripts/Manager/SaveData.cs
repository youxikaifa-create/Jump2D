using System.Collections.Generic;//引入泛型集合命名空间，代码里的List<T> (列表)
using UnityEngine;

[System.Serializable]//编辑器面板显示；确保JsonUtility序列化/反序列化LeveData的两个布尔值能正常转成JSON文本
public class LevelData
{
    public bool isUnlock;//标记按钮可点击状态
    public bool isClear;//标记通关
}

[System.Serializable]
public class GameSave
{
    //public int playerHP;
    //public int coinNum;
    public List<LevelData> levelList = new List<LevelData>();
}