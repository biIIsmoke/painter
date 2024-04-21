using System.Collections.Generic;
using UnityEngine;

namespace Tools.Repository
{
    public interface IToolsRepository
    {
        PaintTool Tool { get; set; }
        
        void Reset();
    }
}