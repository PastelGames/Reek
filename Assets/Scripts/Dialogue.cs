using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisp;
    public string[] sentences;
    private int index;

    public IEnumerator Type(){
        textDisp.text = "";
        foreach(char letter in sentences[index].ToCharArray()){
            textDisp.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

}
