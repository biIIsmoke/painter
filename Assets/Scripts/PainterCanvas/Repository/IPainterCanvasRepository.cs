using System.Collections.Generic;
using UnityEngine;

namespace PainterCanvas.Repository
{
    public interface IPainterCanvasRepository
    {
        string dirPath { get; set; }
        string textureFileName { get; set; }
        
        void Reset();
    }
}