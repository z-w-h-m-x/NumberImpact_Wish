using UnityEngine;
using UnityEngine.UI;

public class ChangeLoadText : MonoBehaviour
{
    public Text text;

    public static ChangeLoadText instance;

    private void Awake()
    {
        text = this.GetComponent<Text>();
        instance = this;
    }

    public void Change(string content)
    {
        text.text = content;
    }

}