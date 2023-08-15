using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MK_Map;

namespace MK 
{
    public class RegenerationMapButton : MonoBehaviour, IPointerDownHandler
    {
        private MapGenerator mapGenerator;

        private void Awake() 
        {
            mapGenerator = FindObjectOfType<MapGenerator>();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            mapGenerator.ShowMap();
        }
    }
}