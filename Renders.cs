using UnityEngine;

public class Renders
{

    public static bool initialized = false;

    private static UnityEngine.Color color_0;
    private static GUIStyle guistyle_0 = new GUIStyle(GUI.skin.label);
    private static Texture2D texture2D_0 = new Texture2D(1, 1);

    public static void BoxRect(Rect rect, Color color)
    {

        texture2D_0.SetPixel(0, 0, color);
        texture2D_0.Apply();
        color_0 = color;
        GUI.color = color;

        GUI.DrawTexture(rect, texture2D_0);
    }

    public static void DrawRadarBackground(Rect rect)
    {

        Color backgroundColor = new UnityEngine.Color(0f, 0f, 0f, 0.5f);
        texture2D_0.SetPixel(0, 0, backgroundColor);
        texture2D_0.Apply();
        GUI.color = backgroundColor;

        GUI.DrawTexture(rect, texture2D_0);
    }

}

