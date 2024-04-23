using System.Collections;
using System.Collections.Generic;
using PainterCanvas.Repository;
using PainterCanvas.View;
using UnityEngine;

namespace PainterCanvas.Repository
{
    public class PainterCanvasRepository : IPainterCanvasRepository
    {
        public string dirPath { get; set; }
        public string textureFileName { get; set; }

        public PainterCanvasRepository()
        {
            dirPath = Application.persistentDataPath + "/../Texture/";
            textureFileName = "painterTexture.png";
        }
        
        public void Reset()
        {
            dirPath = Application.persistentDataPath + "/../Texture/";
            textureFileName = "painterTexture.png";
        }
    }
}

