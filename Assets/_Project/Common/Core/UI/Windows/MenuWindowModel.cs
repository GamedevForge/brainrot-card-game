using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Core.UI.Windows
{
    public class MenuWindowModel : IWindowModel
    {
        public GameObject WindowGameObject { get; } 
    }

    public class WinWindowController : IWindowController
    {
        public UniTask Hide()
        {
            throw new System.NotImplementedException();
        }

        public UniTask Show()
        {
            throw new System.NotImplementedException();
        }
    }
}