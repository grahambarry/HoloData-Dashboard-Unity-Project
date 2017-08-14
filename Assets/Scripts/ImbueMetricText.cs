using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImbueMetricText : MonoBehaviour {
    public Text textPrefab;
  
    // Use this for initialization
    void Start () {
        Text tempTextBox = Instantiate(textPrefab) as Text;
        //Parent to the panel
       
        tempTextBox.fontSize = 50;
        //Set the text box's text element to the current textToDisplay:
        string textToDisplay = "IMBUER XXXXXXXXXXXXXXXXXXXXX the  metric text";
        tempTextBox.text = textToDisplay;
        Debug.Log(tempTextBox.text + "IMBUER XXXXXXXXXXXXXXXXXXXXX the  metric text");

        // ImbueMetricIntTextToPoint ImbueMetricConstrutor = new ImbueMetricIntTextToPoint(12);
    }

    // Update is called once per frame
    void Update () {
		
	}
    public class ImbueMetricIntTextToPoint
    {
        GameObject _metricTextGameObject = new GameObject();

        int _ImbuedMetricInt = 0;
        string _ImbuedMetricIntToText;
        TextMesh theMetricText = new TextMesh();
        public ImbueMetricIntTextToPoint(int ImbuedMetricInt)
        {
            this._ImbuedMetricInt = ImbuedMetricInt;
            _ImbuedMetricIntToText = this._ImbuedMetricInt.ToString();
            GameObject MetricText = Instantiate(this._metricTextGameObject);
            this.theMetricText = MetricText.transform.GetComponent<TextMesh>();
            //Sets the text.
            theMetricText.text = _ImbuedMetricIntToText;
        
          //  Debug.Log(theMetricText.text + "IMBUER XXXXXXXXXXXXXXXXXXXXX the  ImbuedInputValue");
        

        }
    }
}
