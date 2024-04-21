using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public class ToolsRepository : IToolsRepository
    {
        public PaintTool Tool { get; set; }
        public Color SelectedColor { get; set; }

        public ToolsRepository()
        {
            
        }

        public void Reset()
        {
            
        }

        public void ChangeTool(PaintTool tool)
        {
            Tool = tool;
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