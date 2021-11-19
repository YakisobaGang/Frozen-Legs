using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace YakisobaGang.Player
{
    [Serializable]
    public struct CameraShakeSettings
    {
        public CinemachineVirtualCamera camera;
        [NoiseSettingsProperty] public NoiseSettings noiseProfile;
        public float amplitude;
        public float frequency;
        public float shakeDuration;
    }
    public class GameFeelController : MonoBehaviour
    {
        [SerializeField] private CameraShakeSettings cameraShakeSettings;
        private static GameFeelController instance;

        private void Awake()
        {
            instance = GameObject.FindObjectOfType<GameFeelController>();
        }

        [ContextMenu("Camera Shake")]
        public static void ApplyCameraShake()
        {
           var noise = instance.cameraShakeSettings.camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

           noise.m_NoiseProfile = instance.cameraShakeSettings.noiseProfile;
           noise.m_AmplitudeGain = instance.cameraShakeSettings.amplitude;

           float Getter() => noise.m_FrequencyGain;
           void Setter(float x) => noise.m_FrequencyGain = x;

           DOTween.To(Getter, Setter, instance.cameraShakeSettings.frequency, instance.cameraShakeSettings.shakeDuration)
               .OnComplete(() => noise.m_FrequencyGain = 0);
        }
    }
}
