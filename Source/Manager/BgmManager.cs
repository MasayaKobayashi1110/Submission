using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    public class BgmManager : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private AudioClip clip;

        public void OnStartBGM() 
        {
            soundManager.PlayBgm(clip);
        }
    }
}

