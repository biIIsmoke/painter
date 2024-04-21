using System;
using PainterCanvas.Repository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PainterCanvas.View
{
    public class PainterCanvasView : MonoBehaviour, IPainterCanvasView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _mousePosition;

        private IPainterCanvasRepository _painterCanvasRepository;
        
        public event Action OnImageLoad;
        
        [Inject]
        public void Construct(IPainterCanvasRepository painterCanvasRepository)
        {
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
            Vector3 cameraVector = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePos = new Vector3(cameraVector.x,.5f,cameraVector.z);
            return mousePos;
        }
        
        public void CreateImage()
        {
            _painterCanvasRepository.CanvasImage = new Texture2D(Screen.width, Screen.height);
            GetComponent<SpriteRenderer>().sprite = Sprite.Create(_painterCanvasRepository.CanvasImage, new Rect(0,0,Screen.width,Screen.height), Vector2.one * 0.5f);
            
            float screenHeight = _camera.orthographicSize * 2f;
            float screenWidth = screenHeight * Screen.width / Screen.height;
            
            GetComponent<BoxCollider2D>().size = new Vector2(screenWidth, screenHeight);
        }
    }
}