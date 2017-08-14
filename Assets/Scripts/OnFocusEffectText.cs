using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class OnFocusEffectText : MonoBehaviour, IFocusable
{
    Color SphereHoverWhite = new Color(255f, 255f, 100f, 1.0f);
    //Color SphereHoveParticleGreen = new Color(72f, 232f, 170f, 1.0f);
    Color32 SphereHoveParticleGreen = new Color32(72, 232, 170, 255);
    void Start()
    {
      
    }

    public void OnFocusEnter()
    {
        Text tempTextBox = this.gameObject.transform.parent.GetChild(1).GetChild(0).transform.GetComponent<Text>();
        this.gameObject.transform.GetComponent<Renderer>().material.SetColor("_Color", SphereHoverWhite);
      //  this.gameObject.transform.parent.parent.GetChild(0).transform.GetComponent<Color>();
        string textToDisplay = tempTextBox.text;

        tempTextBox.text = textToDisplay;
        tempTextBox.color = new Color(255f, 255f, 255f, 1.0f);
    }

    public void OnFocusExit()
    {
        Text tempTextBox = this.gameObject.transform.parent.GetChild(1).GetChild(0).transform.GetComponent<Text>();
       this.gameObject.transform.GetComponent<Renderer>().material.SetColor("_Color", SphereHoveParticleGreen);
        //Set the text box's text element to the current textToDisplay:
        string textToDisplay = tempTextBox.text;
       
        tempTextBox.text = textToDisplay;
        tempTextBox.color = new Color(255f, 255f, 255f, 0.0f);
    }


}





