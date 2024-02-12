using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChangAniType : MonoBehaviour
{

    public PlayableDirector TA;
    public PlayableDirector TB;
    public PlayableDirector RE;
    public PlayableDirector TC;
    public PlayableDirector TCA;
    public LocalLanguage_Text_Influencer buttonText;
    public Swttings st;

    private int value=0;

    private void Start() {
        DODO(value);
    }

    public void DODO(int _value)
    {
        value = _value;
        ArchiveData.Data.archiveData.anitype = value + st.state;
        ArchiveData.Do.SaveArchive();
        switch (value + st.state)
        {
            case 0:
            RE.Play();
                buttonText.ChangeKey("generate");
                buttonText.Refresh();
                break;
            case 1:
            RE.Play();
                buttonText.ChangeKey("wish");
                buttonText.Refresh();
                break;
            case 2:
            RE.Play();
                buttonText.ChangeKey("beat");
                buttonText.Refresh();
                break;
            case 3:
            RE.Play();
                StartCoroutine(Ac());
                buttonText.ChangeKey("place");
                buttonText.Refresh();
                break;
            default:
                break;
        }
    }
    public void Chlivk()
    {
        switch (value + st.state)
        {
            case 0:
            RE.Play();
                break;
            case 1:
            RE.Play();
                TA.Play();
                break;
            case 2:
            RE.Play();
                TB.Play();
                break;
            case 3:
            //RE.Play();
                TC.Play();
                TCA.Stop();
                TCA.Play();
                break;
            default:
                break;
        }
    }

    IEnumerator Ac(){
        for (;;)
        {
            if (RE.state != PlayState.Playing)
            {
                TC.Play();
                break;
            }
            yield return new();
        }
    }
}
