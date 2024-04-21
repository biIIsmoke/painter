using PainterCanvas.Repository;
using PainterCanvas.View;

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
            //TODO: check if there is image, if there is, display, else create one
            if (false) //if there is an image saved, load it
            {
                
            }
            else //else create new image
            {
                _painterCanvasView.CreateImage();
            }
        }
    }
}