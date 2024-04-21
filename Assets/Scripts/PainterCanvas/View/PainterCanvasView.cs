using System;
using PainterCanvas.Repository;
using TMPro;
using Tools.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PainterCanvas.View
{
    public class PainterCanvasView : MonoBehaviour, IPainterCanvasView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _mousePosition;

        private IToolsView _toolsView;
        private IPainterCanvasRepository _painterCanvasRepository;
        
        public event Action OnImageLoad;
        
        [Inject]
        public void Construct(IToolsView toolsView,
            IPainterCanvasRepository painterCanvasRepository)
        {
            _toolsView = toolsView;
            _painterCanvasRepository = painterCanvasRepository;
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            OnImageLoad?.Invoke();
            //TODO: do onclick add listeners here for each button and change their text to match the order of the enum
            //_nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnDisable()
        {
            //_nextButton.onClick.RemoveListener(OnNextButtonClicked);
        }
        
        private void OnMouseDown()
        {
            _mousePosition = GetMousePos();
        }

        private void OnMouseDrag()
        {
            _mousePosition = GetMousePos();
        }
        private void OnMouseUp()
        {
            _mousePosition = GetMousePos();
        }

        private Vector3 GetMousePos()
        {
            Vector2 cameraVector = _camera.ScreenToWorldPoint(Input.mousePosition);
            return cameraVector;
        }
        
        public void CreateImage()
        {
            float screenAspect = (float)Screen.width / Screen.height;
            float cameraHeight = _camera.orthographicSize * 2;
            float screenWorldWidth = cameraHeight * screenAspect;
            Debug.Log($"my width is {screenWorldWidth} and height is {cameraHeight}");
            
            transform.localScale = new Vector3(screenWorldWidth, cameraHeight, 1);
        }
    }
}