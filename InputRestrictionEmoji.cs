using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class InputRestrictionEmoji : MonoBehaviour
{

    InputField mtext;

    private void Start()
    {
        mtext = GetComponent<InputField>();
        if (mtext)
        {
            mtext.onValueChanged.AddListener(OnInputValue);
        }
    }

    void OnInputValue(string value)
    {
        string msg = mtext.text;
        //屏蔽emoji
        string result = Regex.Replace(msg, @"\p{Cs}", "");
        mtext.text = result;
    }
}