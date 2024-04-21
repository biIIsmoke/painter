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
        private IToolsView _toolsView;
        private IToolsRepository _toolsRepository;
        
        public event Action<PaintTool> OnToolButtonClicked;
        
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
        
        public void OnToolButtonClick(int tool) //change tool
        {
            _toolsRepository.Tool = (PaintTool)tool;
            OnToolButtonClicked?.Invoke(_toolsRepository.Tool);
        }
    }
}