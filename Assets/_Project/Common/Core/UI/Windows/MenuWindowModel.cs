using System;
using UnityEngine;

namespace Project.Core.UI.Windows
{
    public class MenuWindowModel : IWindowModel
    {
        public GameObject WindowGameObject { get; } 
        public MenuWindowComponents MenuWindowComponents { get; }

        public MenuWindowModel(
            GameObject windowGameObject, 
            MenuWindowComponents menuWindowComponents)
        {
            WindowGameObject = windowGameObject;
            MenuWindowComponents = menuWindowComponents;
        }
    }
}