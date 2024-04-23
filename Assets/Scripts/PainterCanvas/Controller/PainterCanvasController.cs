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
            if(_painterCanvasView.GetPainterTexture().GetPixels(0,0,1,1)[0].a == 0) //if image is not initialized ie first pixel is not white, create new image
            {
                _painterCanvasView.CreateImage();
            }
        }
    }
}