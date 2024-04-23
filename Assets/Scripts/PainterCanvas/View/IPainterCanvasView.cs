using System;
using UnityEngine;

namespace PainterCanvas.View
{
    public interface IPainterCanvasView
    {
        event Action OnImageLoad;

        void CreateImage();
        void SaveImage(Texture2D newTexture);
        void LoadImage();
        Texture2D GetPainterTexture();
    }
}