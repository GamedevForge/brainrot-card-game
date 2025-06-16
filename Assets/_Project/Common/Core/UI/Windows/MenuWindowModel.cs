using System;
using UnityEngine;

namespace Project.Core.UI.Windows
{
    public class MenuWindowModel : IWindowModel
    {
        public GameObject WindowGameObject { get; } 
        
        public MenuWindowModel(GameObject windowGameObject)
        {
            WindowGameObject = windowGameObject;
        }


    }
}