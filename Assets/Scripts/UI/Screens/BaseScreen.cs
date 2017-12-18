using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HappyPassengers.Scripts.UI.Screens
{
    public abstract class BaseScreen: MonoBehaviour
    {
        [SerializeField]
        private GameObject defaultSelection;

        private EventSystem eventSystem;

        protected EventSystem EventSystem
        {
            get
            {
                eventSystem = eventSystem ?? GameObject.FindObjectOfType<EventSystem>();
                return eventSystem;
            }
        }

        public virtual void Open()
        {
            Display(true);
            Focus();
        }

        public virtual void Close()
        {
            Display(false);
        }

        public virtual void Display(bool show)
        {
            gameObject.SetActive(show);
        }

        public virtual void Focus()
        {
           EventSystem.SetSelectedGameObject(defaultSelection);
        }
    }
}
