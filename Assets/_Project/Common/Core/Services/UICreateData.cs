using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.Services
{
    public struct UICreateData
    {
        public MenuWindowController MenuWindowController;
        public WinWindowController WinWindowController;
        public LoseWindowController LoseWindowController;
        public MenuWindowModel MenuWindowModel;
        public WinWindowModel WinWindowModel;
        public LoseWindowModel LoseWindowModel;
        public GameObject MenuWindowGameObject;
        public GameObject LoseWindowGameObject;
        public GameObject WinWindowGameObject;
    }
}