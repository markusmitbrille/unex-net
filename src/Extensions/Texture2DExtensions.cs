using UnityEngine;

public struct Texture2DCropResult
{
    public int Width;
    public int Height;
    public Color[] Pixels;
}

public static class Texture2DExtensions
{
    public static Texture2DCropResult Crop(this Texture2D texture, int x, int y, int w, int h)
    {
        if (x < 0) { x = 0; }
        if (y < 0) { y = 0; }
        if (w < 0) { w = 0; }
        if (h < 0) { h = 0; }

        Texture2DCropResult cropResult = new Texture2DCropResult();

        if (x + w <= texture.width)
        {
            cropResult.Width = w;
        }
        else
        {
            cropResult.Width = texture.width - x;
            if (cropResult.Width < 0) { cropResult.Width = 0; }
        }

        if (y + h <= texture.height)
        {
            cropResult.Height = h;
        }
        else
        {
            cropResult.Height = texture.height - y;
            if (cropResult.Height < 0) { cropResult.Height = 0; }
        }

        if (cropResult.Width > 0 &&
            cropResult.Height > 0)
        {
            cropResult.Pixels = texture.GetPixels(x, y, cropResult.Width, cropResult.Height, 0);
        }
        else
        {
            cropResult.Pixels = new Color[0];
        }

        return cropResult;
    }
}