using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerText : MonoBehaviour
{
    public static PlayerText Instance;

    [Header("必须拖入")]
    public GameObject damageTextPrefab; // 你的伤害文字预制件（UI Text）
    public Canvas mainCanvas;            // 场景里的UI画布
    public float textUpOffset = 1.5f;    // 头顶偏移，避免被模型挡住
    public float fadeTime = 1.8f;        // 飘字存在时间

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 外部调用入口：在TakeDamage里用这个
    public void SpawnDamageText(float dmg, Transform target)
    {
        if (damageTextPrefab == null || mainCanvas == null || target == null) return;

        // 玩家头顶
        Vector3 worldPos = target.position + new Vector3(0, 1.5f, 0);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 核心修复
        RectTransform canvasRt = mainCanvas.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRt, screenPos, Camera.main, out Vector2 uiPos);

        GameObject textObj = Instantiate(damageTextPrefab, mainCanvas.transform);//
        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.anchoredPosition = uiPos;

        Text t = textObj.GetComponent<Text>();
        t.text = "-" + dmg;

        StartCoroutine(DamageTextAnim(textObj));
        Destroy(textObj, 1.8f);
}

    // 飘字+渐隐动画协程
    IEnumerator DamageTextAnim(GameObject textObj)
    {
        RectTransform rect = textObj.GetComponent<RectTransform>();
        Text txt = textObj.GetComponent<Text>();
        Color color = txt.color;
        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            // 向上飘（UI用anchoredPosition，不用transform.Translate）
            rect.anchoredPosition += new Vector2(0, 60 * Time.deltaTime);

            // 逐渐透明
            color.a = 1 - (time / fadeTime);
            txt.color = color;

            yield return null;
        }
    }
}