using System.Collections;
using System.Collections.Generic;
using PainterCanvas.Repository;
using PainterCanvas.View;
using UnityEngine;

namespace PainterCanvas.Repository
{
    public class PainterCanvasRepository : IPainterCanvasRepository
    {
        public Texture2D CanvasImage { get; set; }

        public PainterCanvasRepository()
        {
            
        }
        
        public void Reset()
        {
            
        }
    }
}

