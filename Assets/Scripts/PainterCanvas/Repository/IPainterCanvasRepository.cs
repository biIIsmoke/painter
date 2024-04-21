using System.Collections.Generic;
using UnityEngine;

namespace PainterCanvas.Repository
{
    public interface IPainterCanvasRepository
    {
        Texture2D CanvasImage { get; set; }
        
        void Reset();
    }
}