using System;
using System.IO;
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
        [SerializeField] private int _thickness = 2;
        [SerializeField] private Vector2 _mouseWorldPosition;
        [SerializeField] private Vector2 _lastMouseWorldPosition = Vector2.zero;
        [SerializeField] private Color _resetColor = new Color(255,255,255,255);
        [SerializeField] private Texture2D _stampTexture;

        private Sprite _painterSprite;
        private Color[] _currentColors;
        private Color[] _cleanColors;
        private bool _mouseDown;

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
            //button.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnDisable()
        {
            //button.onClick.RemoveListener(OnNextButtonClicked);
        }

        private void Awake()
        {
            _camera = Camera.main;
            _mouseDown = false;
            _painterSprite = this.GetComponent<SpriteRenderer>().sprite;
        }
        private void OnMouseDown()
        {
            _mouseDown = true;
        }

        private void OnMouseDrag()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _mouseWorldPosition = GetMouseWorldPosition();
                Collider2D hit = Physics2D.OverlapPoint(_mouseWorldPosition, _layerMask);
                if (hit != null && hit.transform != null)
                {
                    switch (_toolsRepository.SelectedTool)
                    {
                        case PaintTool.Pen:
                            if (_lastMouseWorldPosition == Vector2.zero)
                            {
                                PaintCanvas(_mouseWorldPosition, _toolsRepository.SelectedColor,_thickness);
                            }
                            else
                            {
                                DrawLine(_lastMouseWorldPosition, _mouseWorldPosition, _toolsRepository.SelectedColor,_thickness);
                            }
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
                                StampTexture(_mouseWorldPosition);
                                _mouseDown = false;
                            }
                            break;
                        case PaintTool.Eraser:
                            if (_lastMouseWorldPosition == Vector2.zero)
                            {
                                PaintCanvas(_mouseWorldPosition, _resetColor,_thickness);
                            }
                            else
                            {
                                DrawLine(_lastMouseWorldPosition, _mouseWorldPosition, _resetColor,_thickness);
                            }
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
                    //save last position
                    _lastMouseWorldPosition = _mouseWorldPosition;
                }
            }
        }
        private void OnMouseUp()
        {
            _mouseDown = false;
            _lastMouseWorldPosition = Vector2.zero;
            SaveImage(_painterSprite.texture);
        }

        private Vector2 GetMouseWorldPosition()
        {
            Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return mouseWorldPosition;
        }

        private void PaintCanvas(Vector2 mousePosition, Color color, int thickness)
        {
            Vector2 pixelPosition = WorldToPixelCoordinates(mousePosition);
            
            _currentColors = _painterSprite.texture.GetPixels();
            
            int pixelX = (int)pixelPosition.x;
            int pixelY = (int)pixelPosition.y;
            
            for (int x = pixelX - thickness; x <= pixelX + thickness; x++)
            {
                if (x >= (int)_painterSprite.rect.width || x < 0)
                    continue;

                for (int y = pixelY - thickness; y <= pixelY + thickness; y++)
                {
                    MarkPixelToChange(x, y, color);
                }
            }
            
            _painterSprite.texture.SetPixels(_currentColors);
            _painterSprite.texture.Apply();
        }

        private void DrawLine(Vector2 firstMouseWorldPosition, Vector2 mouseWorldPosition, Color color, int thickness)
        {
            float distance = Vector2.Distance(firstMouseWorldPosition, mouseWorldPosition);
            Vector2 currentPosition;
            float stepDistance = 1 / distance;
            for (float i = 0.0f; i <= 1.0f; i += stepDistance)
            {
                currentPosition = Vector2.Lerp(firstMouseWorldPosition, mouseWorldPosition, i);
                PaintCanvas(currentPosition,color,thickness);
            }
        }

        private void FillCanvas()
        {
            _currentColors = _painterSprite.texture.GetPixels();
            for (int x = 0; x < _currentColors.Length; x++)
                _currentColors[x] = _toolsRepository.SelectedColor;
            
            _painterSprite.texture.SetPixels(_currentColors);
            _painterSprite.texture.Apply();
        }
        
        private void StampTexture(Vector2 mousePosition)
        {
            Vector2 pixelPosition = WorldToPixelCoordinates(mousePosition);
            
            _currentColors = _painterSprite.texture.GetPixels();
            
            int pixelX = (int)pixelPosition.x;
            int pixelY = (int)pixelPosition.y;
            
            for (int x = 0; x < _stampTexture.width; x++)
            {
                for (int y = 0; y < _stampTexture.height; y++)
                {
                    
                    Color paintColor = _stampTexture.GetPixel(x, y);
                    
                    if (pixelX + x >= 0 && pixelY + y >= 0 && pixelX + x < _painterSprite.texture.width && pixelY + y < _painterSprite.texture.height)
                    {
                        MarkPixelToChange(pixelX + x, pixelY + y, paintColor);
                    }
                }
            }
            _painterSprite.texture.SetPixels(_currentColors);
            _painterSprite.texture.Apply();
        }
        
        private Vector2 WorldToPixelCoordinates(Vector2 mousePosition)
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
        
        private void MarkPixelToChange(int x, int y, Color color)
        {
            int arrayPos = y * (int)_painterSprite.rect.width + x;
            
            if (arrayPos > _currentColors.Length || arrayPos < 0)
                return;

            _currentColors[arrayPos] = color;
        }
        
        public void CreateImage()
        {
            Texture2D newTexture = new Texture2D(1000, 750, TextureFormat.RGBA32, false);
            _cleanColors = new Color[newTexture.width * newTexture.height];
            
            for (int x = 0; x < _cleanColors.Length; x++)
                _cleanColors[x] = _resetColor;
            
            newTexture.SetPixels(_cleanColors);
            newTexture.Apply();
            SaveImage(newTexture);
            Destroy(newTexture);
            LoadImage();
        }

        public void SaveImage(Texture2D newTexture)
        {
            byte[] bytes = newTexture.EncodeToPNG();
            if(!Directory.Exists(_painterCanvasRepository.dirPath)) {
                Directory.CreateDirectory(_painterCanvasRepository.dirPath);
            }
            File.WriteAllBytes(_painterCanvasRepository.dirPath + _painterCanvasRepository.textureFileName, bytes);
        }
        public void LoadImage()
        {
            byte[] bytes = File.ReadAllBytes(_painterCanvasRepository.dirPath + _painterCanvasRepository.textureFileName);
            _painterSprite.texture.LoadImage(bytes);
        }

        public Texture2D GetPainterTexture()
        {
            return _painterSprite.texture;
        }
    }
}