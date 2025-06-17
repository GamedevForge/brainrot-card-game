using Project.Core.UI.Animtions;
using Project.Core.UI.Windows;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class UIFactory : IInitializable
    {
        private readonly GameObject _menuWindowPrefab;
        private readonly GameObject _loseWindowPrefab;
        private readonly GameObject _winWindowPrefab;
        private readonly RectTransform _windowsParent;
        private readonly float _windowAnimationDuration;

        private UICreateData _data;

        public UIFactory(
            GameObject menuWindowPrefab,
            GameObject loseWindowPrefab,
            GameObject winWindowPrefab,
            float windowAnimationDuration,
            RectTransform windowsParent)
        {
            _menuWindowPrefab = menuWindowPrefab;
            _loseWindowPrefab = loseWindowPrefab;
            _winWindowPrefab = winWindowPrefab;
            _windowAnimationDuration = windowAnimationDuration;
            _windowsParent = windowsParent;
        }

        public void Initialize()
        {
            _data.MenuWindowGameObject.SetActive(false);
            _data.LoseWindowGameObject.SetActive(false);
            _data.WinWindowGameObject.SetActive(false);
        }
        
        public UICreateData Create()
        {
            _data = new UICreateData();

            _data.MenuWindowGameObject = GameObject.Instantiate(_menuWindowPrefab, _windowsParent);
            _data.LoseWindowGameObject = GameObject.Instantiate(_loseWindowPrefab, _windowsParent);
            _data.WinWindowGameObject = GameObject.Instantiate(_winWindowPrefab, _windowsParent);

            _data.MenuWindowModel = new MenuWindowModel(_data.MenuWindowGameObject);
            _data.MenuWindowController = new MenuWindowController(
                _data.MenuWindowModel,
                _data.MenuWindowGameObject.GetComponent<BaseWindowComponents>().Button,
                new BaseWindowAnimtion(
                    _data.MenuWindowGameObject.GetComponentInChildren<CanvasGroup>(),
                    new AlphaAnimation(_windowAnimationDuration)));

            _data.WinWindowModel = new WinWindowModel(
                _data.WinWindowGameObject,
                _data.WinWindowGameObject.GetComponentInChildren<CanvasGroup>());
            _data.WinWindowController = new WinWindowController(
                _data.WinWindowModel,
                _data.WinWindowGameObject.GetComponent<BaseWindowComponents>().Button,
                new AlphaAnimation(_windowAnimationDuration));

            _data.LoseWindowModel = new LoseWindowModel(
                _data.LoseWindowGameObject,
                _data.LoseWindowGameObject.GetComponentInChildren<CanvasGroup>());
            _data.LoseWindowController = new LoseWindowController(
                _data.LoseWindowModel,
                _data.LoseWindowGameObject.GetComponent<BaseWindowComponents>().Button,
                new AlphaAnimation(_windowAnimationDuration));

            return _data;
        }
    }
}