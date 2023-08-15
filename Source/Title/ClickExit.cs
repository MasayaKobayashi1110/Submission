using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MK 
{
    public class ClickExit : MonoBehaviour, IPointerDownHandler
    {

        public void OnPointerDown(PointerEventData data) 
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
