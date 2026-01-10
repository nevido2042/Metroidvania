using UnityEngine;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    public Text waveText;
    public Text remainText;
    public Text countdown;

    public void SetWaveText(int wave)
    {
        waveText.text = string.Format("Wave {0}", wave);
    }

    public void SetRemainText(int remain)
    {
        remainText.text = string.Format("Remain {0}", remain);
    }

    public void SetCountdownText(int remain)
    {
        countdown.text = string.Format("Wave Start In...{0}", remain);
    }
}
