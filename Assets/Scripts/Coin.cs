using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("显示激活/禁用框");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(10);//注意前面添加一个GameManager
            Destroy(gameObject);
            Debug.Log("脚本禁用不影响OnTriggerEnter2D函数执行");
        }
    }
}