using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CinemachineCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        vcam = GetComponent<CinemachineCamera>();
        noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity,float frequency, float time)
    {
        StartCoroutine(ShakeRoutine(intensity, frequency, time));
    }

    IEnumerator ShakeRoutine(float intensity, float frequency, float time)
    {
        noise.AmplitudeGain = intensity;
        noise.FrequencyGain = frequency;
        yield return new WaitForSeconds(time);
        noise.AmplitudeGain = 0f;
    }
}
