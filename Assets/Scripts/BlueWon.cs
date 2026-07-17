using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlueWon : MonoBehaviour
{
    public GameObject pausePanel;
    private void OnTriggerEnter2D(Collider2D other)//该方法必须勾选Is Trigger才可以执行
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }
}
