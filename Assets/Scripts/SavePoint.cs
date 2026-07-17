using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelControl.Instance.LevelWin();

            Debug.Log("通关成功！下一关开启");
        }
    }
}