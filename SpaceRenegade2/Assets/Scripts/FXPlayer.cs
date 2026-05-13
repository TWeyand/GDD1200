using System.Linq;
using UnityEngine;

public class FXPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] VFX = new GameObject[0];

    [SerializeField]
    private AudioClip[] SFX = new AudioClip[0];

    [SerializeField]
    private GameObject SFXPlayer;

    public void PlayVFX(int index)
    {
        if (VFX.Count() > index && VFX[index] != null)
        {
            GameObject.Instantiate(VFX[index], transform.position, Quaternion.identity);
        }
    }

    public void PlaySFX(int index)
    {
        if (SFX.Count() > index && SFX[index] != null)
        {
            var sfxGenerator = GameObject.Instantiate(SFXPlayer, transform.position, Quaternion.identity);
            sfxGenerator.GetComponent<SFXSetup>().PlayAudio(SFX[index]);
        }
    }
}
