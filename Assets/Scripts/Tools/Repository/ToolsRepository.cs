using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public class ToolsRepository : IToolsRepository
    {
        public PaintTool Tool { get; set; }

        public ToolsRepository()
        {
            
        }

        public void Reset()
        {
            
        }
    }

    public enum PaintTool
    {
        Pen,
        Bucket,
        Stamp,
        Eraser,
        Splash
    }
}