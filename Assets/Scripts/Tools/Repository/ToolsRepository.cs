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
            
        }

        public void Reset()
        {
            
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

    public enum PaintColors
    {
        Red,
        Green,
        Blue
    }
}