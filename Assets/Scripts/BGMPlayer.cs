using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    private void Awake()
    {
        Instance = this;//单例初始化，防止在其他脚本调用时出现空引用报错
    }
}
