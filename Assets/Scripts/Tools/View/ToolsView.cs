using System;
using System.Collections.Generic;
using Tools.View;
using Tools.Repository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Tools.View
{
    public class ToolsView : MonoBehaviour, IToolsView
    {
        [SerializeField] private List<Image> _buttonImages;
        
        private IToolsView _toolsView;
        private IToolsRepository _toolsRepository;
        
        public event Action<PaintTool> OnToolButtonClicked;
        public event Action<Color> OnColorButtonClicked;
        
        [Inject]
        public void Construct(IToolsView toolsView,
            IToolsRepository toolsRepository)
        {
            _toolsView = toolsView;
            _toolsRepository = toolsRepository;
            //TODO: create tool buttons here under the tools panel
        }

        private void OnEnable()
        {
            //TODO: do onclick add listeners here for each button and change their text to match the order of the enum
            //_nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnDisable()
        {
            //_nextButton.onClick.RemoveListener(OnNextButtonClicked);
        }
        
        public void OnToolButtonClick(int toolIndex) //change tool
        {
            _toolsRepository.ChangeTool((PaintTool)toolIndex);
            OnToolButtonClicked?.Invoke(_toolsRepository.SelectedTool);
        }

        public void OnColorButtonClick(GameObject button) 
        {    //for only three colors, no need to make it complicated by index etc. if there is more, we can use event triggers or create an rgb color selector
            Color color = button.GetComponent<Image>().color;
            _toolsRepository.ChangeColor(color);
            _buttonImages[0].color = color;
            _buttonImages[1].color = color;
            OnColorButtonClicked?.Invoke(_toolsRepository.SelectedColor);
        }
        
        
    }
}