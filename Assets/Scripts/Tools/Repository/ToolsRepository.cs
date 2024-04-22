using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public class ToolsRepository : IToolsRepository
    {
        public PaintTool SelectedTool { get; set; }
        public Color SelectedColor { get; set; }
        public bool CanDraw { get; set; }

        public ToolsRepository()
        {
            SelectedColor = Color.red;
            CanDraw = true;
        }

        public void Reset()
        {
            SelectedColor = Color.red;
            CanDraw = true;
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