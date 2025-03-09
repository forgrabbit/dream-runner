using UnityEngine;

public class Transparency : MonoBehaviour
{
    public float minAlpha = 0.5f; // 最小透明度
    public float maxAlpha = 1.0f; // 最大透明度

    private SpriteRenderer spriteRenderer; // 用于2D精灵
    private Material material; // 用于3D对象

    void OnEnable()
    {
        // 获取组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = GetComponent<Renderer>()?.material;

        // 设置随机透明度
        SetRandomTransparency();
    }

    void SetRandomTransparency()
    {
        // 生成随机透明度
        float randomAlpha = Random.Range(minAlpha, maxAlpha);

        // 检查是否是2D精灵
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = randomAlpha;
            spriteRenderer.color = color;
        }
        // 检查是否是3D对象
        else if (material != null)
        {
            Color color = material.color;
            color.a = randomAlpha;
            material.color = color;
        }
        // 如果既不是2D也不是3D对象，输出警告
        else
        {
            Debug.LogWarning("该对象既没有SpriteRenderer也没有Renderer组件，无法设置透明度。");
        }
    }
}