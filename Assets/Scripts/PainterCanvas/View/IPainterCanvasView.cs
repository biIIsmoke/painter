using System;
using UnityEngine;

namespace PainterCanvas.View
{
    public interface IPainterCanvasView
    {
        event Action OnImageLoad;

        void CreateImage();
        Texture2D GetPainterTexture();
    }
}