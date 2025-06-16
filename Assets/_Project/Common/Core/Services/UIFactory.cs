using Project.Core.UI.Animtions;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class UIFactory
    {
        private readonly GameObject _menuWindowPrefab;
        private readonly GameObject _loseWindowPrefab;
        private readonly GameObject _winWindowPrefab;
        private readonly float _windowAnimationDuration;

        public UIFactory(
            GameObject menuWindowPrefab, 
            GameObject loseWindowPrefab, 
            GameObject winWindowPrefab, 
            float windowAnimationDuration)
        {
            _menuWindowPrefab = menuWindowPrefab;
            _loseWindowPrefab = loseWindowPrefab;
            _winWindowPrefab = winWindowPrefab;
            _windowAnimationDuration = windowAnimationDuration;
        }

        public UICreateData Create()
        {
            UICreateData data = new();

            data.MenuWindowGameObject = GameObject.Instantiate(_menuWindowPrefab);
            data.LoseWindowGameObject = GameObject.Instantiate(_loseWindowPrefab);
            data.WinWindowGameObject = GameObject.Instantiate(_winWindowPrefab);

            data.MenuWindowModel = new MenuWindowModel(data.MenuWindowGameObject);
            data.MenuWindowController = new MenuWindowController(
                data.MenuWindowModel,
                data.MenuWindowGameObject.GetComponent<BaseWindowComponents>().Button,
                new BaseWindowAnimtion(
                    data.MenuWindowGameObject.GetComponentInChildren<CanvasGroup>(),
                    new AlphaAnimation(_windowAnimationDuration)));

            data.WinWindowModel = new WinWindowModel(
                data.WinWindowGameObject,
                data.WinWindowGameObject.GetComponentInChildren<CanvasGroup>());
            data.WinWindowController = new WinWindowController(
                data.WinWindowModel,
                data.WinWindowGameObject.GetComponent<BaseWindowComponents>().Button,
                new AlphaAnimation(_windowAnimationDuration));

            data.LoseWindowModel = new LoseWindowModel(
                data.LoseWindowGameObject, 
                data.LoseWindowGameObject.GetComponentInChildren<CanvasGroup>());
            data.LoseWindowController = new LoseWindowController(
                data.LoseWindowModel,
                data.LoseWindowGameObject.GetComponent<BaseWindowComponents>().Button,
                new AlphaAnimation(_windowAnimationDuration));

            return data;
        }
    }
}