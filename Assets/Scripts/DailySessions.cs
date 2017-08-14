using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System;
using System.Linq;



public class DailySessions : DropdownScript {



    void Start()
    { //Timeline of the Level creator
      //   GetLevel(0);
   
    }
    public class GetLevelClass
    {
        string _parentXmlNodeLabel;
        string _childXmlNodeLabel;
        TextAsset _metricXMLGLC;
        List<Dictionary<string, int>> levels = new List<Dictionary<string, int>>();
        List<Dictionary<string, DateTime>> levelsdate = new List<Dictionary<string, DateTime>>();
        Dictionary<string, int> obj;
        Dictionary<string, DateTime> objdate;
        int _MetricXMLGB_Int_DD;

        // TextAsset _GameAssetb;
        // TextAsset _GameAssetc;
        List<int> sessionValuesindex = new List<int>();
        List<DateTime> sessionDatesindex = new List<DateTime>();
        XmlDocument xmlDoc = new XmlDocument();

        //   public GetLevelClass(int MetricXML)
        public GetLevelClass(int MetricXMLGB_Int_DD, TextAsset MetricXML)
        {
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._metricXMLGLC = MetricXML;
            
            this._parentXmlNodeLabel = metricsDropDownListParentNodeLabel[MetricXMLGB_Int_DD];
            this._childXmlNodeLabel = metricsDropDownListChildNodeLabel[MetricXMLGB_Int_DD];

        //this.parentXmlNodeLabel = metricsDropDownListParentNodeLabel[_metricXMLGLC];
        //  this.childXmlNodeLabel = metricsDropDownListChildNodeLabel[_metricXMLGLC];

         // xmlDoc is the new xml document.
            this.xmlDoc.LoadXml(this._metricXMLGLC.text);


            XmlNodeList levelsList = xmlDoc.GetElementsByTagName(_parentXmlNodeLabel); // array of the level nodes.
         
            foreach (XmlNode levelInfo in levelsList)
            {
                XmlNodeList levelcontent = levelInfo.ChildNodes;
                obj = new Dictionary<string, int>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.
                objdate = new Dictionary<string, DateTime>();

                foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
                {

                    if (levelsItens.Name == "xdate-sessions")
                    {
                        DateTime xmldate = DateTime.Now;
                        DateTime.TryParse(levelsItens.InnerText, out xmldate);
                        objdate.Add("xdate-sessions", xmldate); // put this in the dictionary.
                        sessionDatesindex.Add(xmldate);

                    }

                    if (levelsItens.Name == _childXmlNodeLabel)
                    {
                        int xmlint = 0;

                        Int32.TryParse(levelsItens.InnerText, out xmlint);
                        obj.Add(_childXmlNodeLabel, xmlint); // put this in the dictionary.CHILDXMLNODE FROM DROPDOWN LIST IN DROPDOWN SCRIPT
                        sessionValuesindex.Add(xmlint);
                    }
                }
                levels.Add(obj); // add whole obj dictionary in the levels[].

                levelsdate.Add(objdate); // add whole obj dictionary in the levels[].

            }
        }

        public List<int> SessionValuesindex()
           
        {

            return this.sessionValuesindex;

        }
        public List<DateTime> SessionDatesindex()
        {
            return this.sessionDatesindex;
        }

        public List<Dictionary<string, int>> LevelsMethod()
        {
            return this.levels;
        }
    }


public class GetStartDate
    {
        int _MetricXMLGB_Int_DD = 0;
        int _IndexDateStart = 0;
        DateTime _DateStart;
        TextAsset _GetLevelIndex;
       
        public GetStartDate(DateTime DateStart, int MetricXMLGB_Int_DD, TextAsset GetLevelIndex)
        {
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._GetLevelIndex = GetLevelIndex;
            //  this._getlevel = GetLevel(_GetLevelIndex);
           GetLevelClass GLCsessionDateGetter = new GetLevelClass(_MetricXMLGB_Int_DD, GetLevelIndex);
            this._IndexDateStart = GLCsessionDateGetter.SessionDatesindex().FindIndex(s => s == DateStart);
           
            this._DateStart = DateStart;
           // this._IndexDateStart = sessionDatesindex.FindIndex(s => s == DateStart);
        }

        public int CalculateStart()
        {
           
            // DailySessions SD = new DailySessions();
            // int Area3 = SD.GetLevel();
            // Debug.Log(Area3 + "otherclass3");
            return this._IndexDateStart;

        }
    }

    public class GetEndDate
    {
        int _IndexDateEnd = 0;
        int _MetricXMLGB_Int_DD = 0;
        DateTime _DateEnd;
        TextAsset _GetLevelIndex;
        public GetEndDate(DateTime DateEnd, int MetricXMLGB_Int_DD, TextAsset GetLevelIndex)
        {
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._GetLevelIndex = GetLevelIndex;
            GetLevelClass GLCsessionDateGetterGED = new GetLevelClass(_MetricXMLGB_Int_DD, GetLevelIndex);
            this._IndexDateEnd = GLCsessionDateGetterGED.SessionDatesindex().FindIndex(s => s == DateEnd);
            this._DateEnd = DateEnd;
           // this._IndexDateEnd = sessionDatesindex.FindIndex(s => s == DateEnd);
        }

        public int CalculateEnd()
        {
            // DailySessions SD = new DailySessions();
            // int Area3 = SD.GetLevel();
            // Debug.Log(Area3 + "otherclass3");
            return this._IndexDateEnd;


        }
    }
    public class GetNormalizedValue
    {
        int _MetricXMLGB_Int_DD = 0;
        int _IndexSpan1 = 0;
        int _IndexSpan2 = 0;
        int _IndexValue1 = 0;
        int _IndexValue2 = 0;
        int _IndexTakeAmount = 0;
        float _normalizedOutputValue = 0.00F;
        int _MaxValueFloatNormal;
        TextAsset _GetLevelIndex;

        public GetNormalizedValue(int IndexSpan1, int IndexSpan2, int MetricXMLGB_Int_DD, TextAsset GetLevelIndex)
        {
            this._GetLevelIndex = GetLevelIndex;
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._IndexSpan1 = IndexSpan1;
            this._IndexSpan2 = IndexSpan2;
            Debug.Log(IndexSpan1 + "Value of int index of where ARea4/5 Date Successfully Passed in");
            Debug.Log(IndexSpan2 + "Value of int index of where ARea4/5 Date Successfully Passed in");

              
               
                this._IndexTakeAmount = _IndexSpan2 - _IndexSpan1;
                Debug.Log(_IndexTakeAmount + "_Indetakeamount else");

            GetLevelClass GLCsessionDateGetterGNV = new GetLevelClass(_MetricXMLGB_Int_DD, GetLevelIndex);
        

            this._MaxValueFloatNormal = GLCsessionDateGetterGNV.SessionValuesindex().Skip(this._IndexSpan1).Take(this._IndexTakeAmount).Max();
                this._normalizedOutputValue = _MaxValueFloatNormal * 1.00F;
                Debug.Log(_IndexSpan1 + "_IndexValue1 in elseRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                Debug.Log(_IndexSpan2 + "_IndexValue2 in elseRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
            
            

        }

        public float CalculateNormalizedValue()
        {
            // DailySessions SD = new DailySessions();
            // int Area3 = SD.GetLevel();
            // Debug.Log(Area3 + "otherclass3");
            return this._normalizedOutputValue;


        }
    }


    public class GetXmlArray
    {

        int _MetricXMLGB_Int_DD = 0;
        List<Dictionary<string, int>> _ReturnLevels = new List<Dictionary<string, int>>();
        TextAsset _GetLevelIndex;
        public GetXmlArray(int MetricXMLGB_Int_DD, TextAsset GetLevelIndex)
        {
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._GetLevelIndex = GetLevelIndex;
            GetLevelClass GLCsessionDateGetterGXA = new GetLevelClass(_MetricXMLGB_Int_DD, GetLevelIndex);
            this._ReturnLevels = GLCsessionDateGetterGXA.LevelsMethod();
     

        }

        public List<Dictionary<string, int>> ReturnXmlArray()
        {
            // DailySessions SD = new DailySessions();
            // int Area3 = SD.GetLevel();
            // Debug.Log(Area3 + "otherclass3");
            return this._ReturnLevels;
            

        }
}
    public class GetXmlDateArray
    {

        int _MetricXMLGB_Int_DD = 0;
        List<DateTime> _ReturnDateArray = new List<DateTime>();
        TextAsset _GetLevelIndex;
        public GetXmlDateArray(int MetricXMLGB_Int_DD, TextAsset GetLevelIndex)
        {
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._GetLevelIndex = GetLevelIndex;
            GetLevelClass GLCsessionDateGetterRDA = new GetLevelClass(_MetricXMLGB_Int_DD, GetLevelIndex);
            this._ReturnDateArray = GLCsessionDateGetterRDA.SessionDatesindex();
    

        }

        public List<DateTime> ReturnXmlDateArray()
        {
            // DailySessions SD = new DailySessions();
            // int Area3 = SD.GetLevel();
            // Debug.Log(Area3 + "otherclass3");
            return this._ReturnDateArray;


        }
    }
}




