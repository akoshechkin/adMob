using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Вывод текста в Text consol на экран

public class ClassDebugConsol : MonoBehaviour {

    public Text consol = null;
    int countTextStr = 0;

    virtual public void setDedugVisible(bool isVisible) {
        consol.enabled = isVisible;
    }

    public void showTextConsol(string text = "") {
        if (++countTextStr > 20) {
            consol.text = "";
            countTextStr = 0;
        }
        consol.text += "\n " + text;
        Debug.Log(text);
    }

}
