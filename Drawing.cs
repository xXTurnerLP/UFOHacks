using System.Reflection;
using UnityEngine;

public static class Drawing
{
    private static Texture2D aaLineTex = null;

    private static Texture2D lineTex = null;

    private static Material blitMaterial = null;

    private static Material blendMaterial = null;

    private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);

    public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
    {
        float num = pointB.x - pointA.x;
        float num2 = pointB.y - pointA.y;
        float num3 = Mathf.Sqrt(num * num + num2 * num2);
        if (num3 < 0.001f)
        {
            return;
        }
        Texture2D texture2D;
        if (antiAlias)
        {
            width *= 3f;
            texture2D = Drawing.aaLineTex;
            Material material = Drawing.blendMaterial;
        }
        else
        {
            texture2D = Drawing.lineTex;
            Material material2 = Drawing.blitMaterial;
        }
        float num4 = width * num2 / num3;
        float num5 = width * num / num3;
        Matrix4x4 identity = Matrix4x4.identity;
        identity.m00 = num;
        identity.m01 = -num4;
        identity.m03 = pointA.x + 0.5f * num4;
        identity.m10 = num2;
        identity.m11 = num5;
        identity.m13 = pointA.y - 0.5f * num5;
        GL.PushMatrix();
        GL.MultMatrix(identity);
        GUI.color = color;
        GUI.DrawTexture(Drawing.lineRect, texture2D);
        GL.PopMatrix();
    }

    public static void Initialize()
    {
        if (lineTex == null)
        {
            lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            lineTex.SetPixel(0, 1, UnityEngine.Color.white);
            lineTex.Apply();
        }
        if (aaLineTex == null)
        {
            aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, false);
            aaLineTex.SetPixel(0, 0, new UnityEngine.Color(1, 1, 1, 0));
            aaLineTex.SetPixel(0, 1, UnityEngine.Color.white);
            aaLineTex.SetPixel(0, 2, new UnityEngine.Color(1, 1, 1, 0));
            aaLineTex.Apply();
        }

        blitMaterial = (Material)typeof(GUI).GetMethod("get_blitMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
        blendMaterial = (Material)typeof(GUI).GetMethod("get_blendMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
    }
}