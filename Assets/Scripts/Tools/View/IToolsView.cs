using System;
using Tools.Repository;
using UnityEngine;

namespace Tools.View
{
    public interface IToolsView
    {
        event Action<PaintTool> OnToolButtonClicked;
        void OnToolButtonClick(int tool);
    }
}