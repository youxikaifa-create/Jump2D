using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public GameSave saveData;//点击加号可以看到里面的bool类型变量，因为有SaveData里面的[System.Serializable]

    private string path;//存文件路径

    void Awake()
    {
        Instance = this;

        path = Application.persistentDataPath + "/save.json";//拼接一个路径，不管save.json存不存在，不影响File.Exists(path)
        Debug.Log("存档完整路径：" + path);//获取文档完整路径,这个path存储的是路径
        LoadGame();
    }

    //初始化3关
    void InitLevel()
    {
        saveData.levelList.Clear();
        for (int i = 0; i < 3; i++)
        {
            LevelData ld = new LevelData();
            ld.isUnlock = i == 0;
            ld.isClear = false;
            saveData.levelList.Add(ld);
        }
        SaveGame();
    }

    public void SaveGame()//保存游戏通关进度，类似PlayerPrefs.Save();
    {
        string json = JsonUtility.ToJson(saveData);//把存档类数据，转成 JSON 文本，
        File.WriteAllText(path, json);//写入本地 save.json 文件完成存档，若不存在自动创建，存在则覆盖原有内容
    }

    public void OnStartNewName()
    {
        DeleteSaveGame();
    }

    public void LoadGame()//游戏存档读取
    {
        if (File.Exists(path))//判断指定路径的文件是否存在,与文件内容无关
        {
            string json = File.ReadAllText(path);//读取整个文本文件的全部内容，一次性返回一整段字符串。
            saveData = JsonUtility.FromJson<GameSave>(json);//Unity 自带反序列化方法,1
            Debug.Log("666!");
        }/*1:传入一个 JSON 文本字符串;< GameSave > 是泛型约束：指定要转换成哪个类的实例;
          函数会自动解析 JSON 里的键值，匹配类里同名公开字段，赋值进去;返回一个 GameSave 类型的对象。
        执行到saveData = JsonUtility.FromJson<GameSave>(json);就已经足够了，可以改变path路径下的文件内容来改变
        反序列化后saveData类里面的内容*/

        else
        {
            saveData = new GameSave();
            InitLevel();
            Debug.Log("777!");
        }
    }

    private void DeleteSaveGame()
    {
        if (File.Exists(path))
        {
            File.Delete(path);//文件存在删除该文件，不存在也不影响，无异常
        }
    }
}