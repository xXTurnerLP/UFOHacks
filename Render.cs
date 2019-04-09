using System;
using UnityEngine;

public class Render
{
    public static void RectFilled(float x, float y, float width, float height, Color color)
    {
        bool flag = color != Render.textureColor;
        if (flag)
        {
            Render.textureColor = color;
            Render.texture.SetPixel(0, 0, color);
            Render.texture.Apply();
        }
        GUI.DrawTexture(new Rect(x, y, width, height), Render.texture);
    }

    public static void RectOutlined(float x, float y, float width, float height, Color color, float thickness = 1f)
    {
        Render.RectFilled(x, y, thickness, height, color);
        Render.RectFilled(x + width - thickness, y, thickness, height, color);
        Render.RectFilled(x + thickness, y, width - thickness * 2f, thickness, color);
        Render.RectFilled(x + thickness, y + height - thickness, width - thickness * 2f, thickness, color);
    }

    public static void Box(float x, float y, float width, float height, Color color, float thickness = 1f)
    {
        Render.RectOutlined(x - width / 2f, y - height, width, height, color, thickness);
    }

    public static void Health(float x, float y, float health, float maxHealth = 100f, float width = 50f, float height = 5f, float thickness = 1f)
    {
        float num = (width - thickness * 2f) * health / maxHealth;
        bool flag = num < 1f;
        if (flag)
        {
            num = 1f;
        }
        Color color = Color.magenta;
        bool flag2 = health < maxHealth * 0.6f;
        if (flag2)
        {
            color = Color.magenta;
        }
        bool flag3 = health < maxHealth * 0.3f;
        if (flag3)
        {
            color = Color.red;
        }
        Render.RectFilled(x - width / 2f, y - height, width, height, Color.gray);
        Render.RectFilled(x - width / 2f + thickness, y - height + thickness, num, height - thickness * 2f, color);
    }

    public static void String(float x, float y, string text, Color color, bool center = true)
    {
        GUIContent content = new GUIContent(text);
        if (center)
        {
            x -= Render.style.CalcSize(content).x / 2f;
        }
        Render.style.normal.textColor = color;
        GUI.Label(new Rect(x, y, 300f, 25f), content, Render.style);
    }

    private static Color textureColor;

    private static Texture2D texture = new Texture2D(1, 1);

    private static GUIStyle style = new GUIStyle(GUI.skin.label)
    {
        fontSize = 12
    };
}
