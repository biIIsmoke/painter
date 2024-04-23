using System.IO;
using PainterCanvas.Repository;
using PainterCanvas.View;
using UnityEngine;

namespace PainterCanvas.Controller
{
    public class PainterCanvasController
    {
        private IPainterCanvasView _painterCanvasView;
        private IPainterCanvasRepository _painterCanvasRepository;
        public PainterCanvasController(IPainterCanvasView painterCanvasView,
            IPainterCanvasRepository painterCanvasRepository)
        {
            _painterCanvasView = painterCanvasView;
            _painterCanvasRepository = painterCanvasRepository;

            _painterCanvasView.OnImageLoad += OnImageLoaded;
        }

        public void Dispose()
        {
            _painterCanvasView.OnImageLoad -= OnImageLoaded;
        }

        private void OnImageLoaded()
        {
            //if image in persistent path, else create one
            string path = _painterCanvasRepository.dirPath + _painterCanvasRepository.textureFileName;
            Debug.Log(path);
            if(File.Exists(path))
            {
                //load image from path
                _painterCanvasView.LoadImage();
            }
            else
            {
                _painterCanvasView.CreateImage();
            }
        }
    }
}