using System;
using PainterCanvas.Repository;
using TMPro;
using Tools.Repository;
using Tools.View;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace PainterCanvas.View
{
    public class PainterCanvasView : MonoBehaviour, IPainterCanvasView
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector2 _mousePosition;
        [SerializeField] private Color _resetColor = new Color(255,255,255,255);
        
        private bool _mouseDown = false;

        private Sprite _painterSprite;
        private Texture2D _painterTexture;
        private Color[] _currentColors;
        private Color[] _cleanColors;

        private IToolsRepository _toolsRepository;
        private IPainterCanvasRepository _painterCanvasRepository;
        
        public event Action OnImageLoad;
        
        [Inject]
        public void Construct(IToolsRepository toolsRepository,
            IPainterCanvasRepository painterCanvasRepository)
        {
            _toolsRepository = toolsRepository;
            _painterCanvasRepository = painterCanvasRepository;
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

        private void Awake()
        {
            
            _camera = Camera.main;
            _painterSprite = this.GetComponent<SpriteRenderer>().sprite;
            _painterTexture = _painterSprite.texture;
        }

        private void OnMouseDown()
        {
            _mouseDown = true;
        }

        private void OnMouseDrag()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _mousePosition = GetMousePos();
                Collider2D hit = Physics2D.OverlapPoint(_mousePosition, _layerMask);
                if (hit != null && hit.transform != null)
                {
                    switch (_toolsRepository.SelectedTool)
                    {
                        case PaintTool.Pen:
                            PaintCanvas(_mousePosition, _toolsRepository.SelectedColor);
                            break;
                        case PaintTool.Bucket:
                            if (_mouseDown)
                            {
                                FillCanvas();
                                _mouseDown = false;
                            }
                            break;
                        case PaintTool.Stamp:
                            if (_mouseDown)
                            {
                                //use stamp
                                _mouseDown = false;
                            }
                            break;
                        case PaintTool.Eraser:
                            PaintCanvas(_mousePosition, _resetColor);
                            break;
                        case PaintTool.Splash:
                            if (_mouseDown)
                            {
                                //use splash
                                _mouseDown = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void OnMouseUp()
        {
            _mouseDown = false;
        }

        private Vector2 GetMousePos()
        {
            Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return mouseWorldPosition;
        }

        private void PaintCanvas(Vector2 mousePosition, Color color)
        {
            Vector2 pixelPosition = WorldToPixelCoordinates(mousePosition);
            
            _currentColors = _painterTexture.GetPixels();
            
            int pixelX = (int)pixelPosition.x;
            int pixelY = (int)pixelPosition.y;

            int thickness = 2;
            
            for (int x = pixelX - thickness; x <= pixelX + thickness; x++)
            {
                if (x >= (int)_painterSprite.rect.width || x < 0)
                    continue;

                for (int y = pixelY - thickness; y <= pixelY + thickness; y++)
                {
                    MarkPixelToChange(x, y, color);
                }
            }
            
            _painterTexture.SetPixels(_currentColors);
            _painterTexture.Apply();
        }

        private void FillCanvas()
        {
            _currentColors = _painterTexture.GetPixels();
            for (int x = 0; x < _currentColors.Length; x++)
                _currentColors[x] = _toolsRepository.SelectedColor;
            
            _painterTexture.SetPixels(_currentColors);
            _painterTexture.Apply();
        }
        
        public Vector2 WorldToPixelCoordinates(Vector2 mousePosition)
        {
            Vector3 localPosition = transform.InverseTransformPoint(mousePosition);
            
            float pixelWidth = _painterSprite.rect.width;
            float pixelHeight = _painterSprite.rect.height;
            float unitsToPixels = pixelWidth / _painterSprite.bounds.size.x * transform.localScale.x;
            
            float centeredX = localPosition.x * unitsToPixels + pixelWidth / 2;
            float centeredY = localPosition.y * unitsToPixels + pixelHeight / 2;
            
            Vector2 pixelPosition = new Vector2(Mathf.RoundToInt(centeredX), Mathf.RoundToInt(centeredY));

            return pixelPosition;
        }
        
        public void MarkPixelToChange(int x, int y, Color color)
        {
            int arrayPos = y * (int)_painterSprite.rect.width + x;
            
            if (arrayPos > _currentColors.Length || arrayPos < 0)
                return;

            _currentColors[arrayPos] = color;
        }
        
        public void CreateImage()
        {
            // Initialize clean pixels to use
            _cleanColors = new Color[(int)_painterSprite.rect.width * (int)_painterSprite.rect.height];
            for (int x = 0; x < _cleanColors.Length; x++)
                _cleanColors[x] = _resetColor;
            
            _painterTexture.SetPixels(_cleanColors);
            _painterTexture.Apply();
        }
    }
}