using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public class ToolsRepository : IToolsRepository
    {
        public PaintTool SelectedTool { get; set; }
        public Color SelectedColor { get; set; }

        public ToolsRepository()
        {
            SelectedColor = Color.white;
        }

        public void Reset()
        {
            SelectedColor = Color.white;
        }

        public void ChangeTool(PaintTool tool)
        {
            SelectedTool = tool;
        }

        public void ChangeColor(Color color)
        {
            SelectedColor = color;
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