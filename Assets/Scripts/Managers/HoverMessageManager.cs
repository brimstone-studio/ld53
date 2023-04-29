using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoverMessageManager : MonoBehaviour
{
    public static HoverMessageManager Instance;
    public TMPro.TMP_Text HoverMessageTextObject;

    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            _message = value;
            if (_message == String.Empty)
            {
                HoverMessageTextObject.text = String.Empty;
            }
            else
            {
                HoverMessageTextObject.text = $"Press <color=\"yellow\">E</color> to {value}";
            }
        }
    }

    private void Start()
    {
        Instance = this;
    }

    private string _message = String.Empty;
}
