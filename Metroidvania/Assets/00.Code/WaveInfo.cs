using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    public Text waveText;
    public Text remainText;

    public void SetWaveText(int wave)
    {
        waveText.text = string.Format("Wave {0}", wave);
    }

    public void SetRemainText(int remain)
    {
        remainText.text = string.Format("Remain {0}", remain);
    }
}
