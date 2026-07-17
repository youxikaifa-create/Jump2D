using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;
    public Image bloodBar; //拖入UI血条填充图片,是子Image对象

    private void Awake()
    {
        
    }

    //受伤掉血
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        PlayerText.Instance.SpawnDamageText(damage,this.transform);
        UpdateBloodUI();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    //更新血条显示
    void UpdateBloodUI()
    {
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        bloodBar.fillAmount = currentHP / maxHP;
    }

    //死亡重置关卡
    void Die()
    {
        transform.position = new Vector3(-7, 0, 0); //重生起点
        currentHP = maxHP;
        UpdateBloodUI();
        SaveHP(); //复活也存一下
    }

    //存档血量
    public void SaveHP()//方便其他脚本调用
    {
        PlayerPrefs.SetFloat("PlayerHP", currentHP);
        PlayerPrefs.Save();
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.Save();
    }

    public void GetHp()
    {
        //开局读取存档血量
        if (PlayerPrefs.HasKey("PlayerHP"))
        {
            currentHP = PlayerPrefs.GetFloat("PlayerHP");
        }
        else
        {
            currentHP = maxHP;
        }

        UpdateBloodUI();//更新血条
    }

    public void GetPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX", transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerPosY", transform.position.y);
            transform.position = new Vector2(x, y);
        }
        else
        {
            transform.position = new Vector2(-9, -1.4f);
        }
    }

    public void OnApplicationQuit()//游戏彻底退出时触发
    {
        Debug.Log("退出游戏！");
    }
}