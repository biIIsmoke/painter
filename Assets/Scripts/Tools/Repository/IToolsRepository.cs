using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public interface IToolsRepository
    {
        PaintTool Tool { get; set; }
        Color SelectedColor { get; set; }
        void Reset();
        void ChangeTool(PaintTool tool);
    }
}