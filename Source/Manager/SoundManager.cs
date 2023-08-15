using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MK 
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bgm;
        [SerializeField] private AudioSource se;
        [SerializeField] private Slider slider;


        public float bgmVolume { 
            get { return bgm.volume; } 
            set { bgm.volume = Mathf.Clamp01(value); }
        }

        public float seVolume { 
            get { return se.volume; } 
            set { se.volume = Mathf.Clamp01(value); }
        }

        private void Start() 
        {
            GameObject soundManager = CheckOtherSoundManager();
            bool checkResult = soundManager != null && soundManager != gameObject;

            if (checkResult) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            bgmVolume = 0.5f;
            slider.value = bgmVolume;
        }

        private GameObject CheckOtherSoundManager() 
        {
            return GameObject.FindGameObjectWithTag("SoundManager");
        }

        public void PlayBgm(AudioClip clip) 
        {
            bgm.clip = clip;

            if (clip == null) return;

            bgm.Play();
        }

        public void PlaySe(AudioClip clip) 
        {
            if (clip == null) return;

            se.PlayOneShot(clip);
        }

        public void OnMove() 
        {
            bgmVolume = slider.value;
        }
    }
}