using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public interface IToolsRepository
    {
        PaintTool SelectedTool { get; set; }
        Color SelectedColor { get; set; }
        void Reset();
        void ChangeTool(PaintTool tool);
        void ChangeColor(Color color);
    }
}