using System;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class XmlInterface : DropdownScript
{



    // Use this for initialization
    void Start()
    {

        //  GameObject ParentalAGameObject = new GameObject();

        //  ParentalAGameObject.name = "ParentalAGameObject";

        //  Debug.Log("Today is " + GBinputStartDate.ToString("MMMM dd, yyyy") + ".FULLxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        //  Debug.Log("Today is " + GBinputStartDate.Month.ToString() + ".MONTHxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        //  Debug.Log("Today is " + GBinputStartDate.Day.ToString() + ".DATxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");


        // MakeBackDropLineHor Lino2 = new MakeBackDropLineHor(0.1F, 0.805F, 0.805F, GBinputStartDate, GBinputEndDate);
        // GraphBuilderASB Lino3 = new GraphBuilderASB(0.9F, 0.9F, pointPrefab, inputDate2, datespanJan2);
        // GraphBuilderASB Lino4 = new GraphBuilderASB(0.8F, 0.8F, pointPrefab, GBinputStartDate, GBinputEndDate, dateLabelPrefab, SlatBGGameO);




    }

    // Update is called once per frame
    void Update()
    {





    }



    public class MakeBackDropLineHor
    {

        float _BackDropYValueFloat;
        GameObject _GameObjectBackDrop = new GameObject();
        GameObject _pointPrefabBackDrop;
        float _radiusXBackDrop;
        float _radiusYBackDrop;
        int _numPointsBackDrop;
        DateTime _startDate;
        DateTime _endDate;
        int _IndexOfStartDateGB;
        int _IndexOfEndDateGB;
        TextAsset _MetricXMLGB;
        int _MetricXMLGB_Int_DD;
        public MakeBackDropLineHor(int MetricXMLGB_Int_DD, TextAsset MetricXMLGB, float BackDropYValueFloat, float RadiusXBackDrop, float RadiusYBackdrop, DateTime startDate, DateTime endDate)
        {
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            this._MetricXMLGB = MetricXMLGB;
            this._BackDropYValueFloat = BackDropYValueFloat;
            this._radiusXBackDrop = RadiusXBackDrop;
            this._radiusYBackDrop = RadiusYBackdrop;

            //   this._pointPrefabBackDrop = pointPrefabBackDrop;

            this._startDate = startDate;
            this._endDate = endDate;


            //CALCULATING THE INDEX START DATE AND END DATE AND NORMALIZED VALUE FROM THAT RANGE

            DailySessions.GetStartDate GBStartDate = new DailySessions.GetStartDate(_startDate, _MetricXMLGB_Int_DD, _MetricXMLGB);
            this._IndexOfStartDateGB = GBStartDate.CalculateStart();

            DailySessions.GetEndDate GBEndDate = new DailySessions.GetEndDate(_endDate, _MetricXMLGB_Int_DD, _MetricXMLGB);
            this._IndexOfEndDateGB = GBEndDate.CalculateEnd();

            //Number of Points might need change this if one value is greater than the other
            if (this._IndexOfStartDateGB < this._IndexOfEndDateGB)
            {
                this._numPointsBackDrop = this._IndexOfEndDateGB - this._IndexOfStartDateGB;
            }
            else
            {
                this._numPointsBackDrop = this._IndexOfStartDateGB - this._IndexOfEndDateGB;
            }


            LineRenderer lineRendererBackDrop = this._GameObjectBackDrop.AddComponent<LineRenderer>();
            lineRendererBackDrop.material = new Material(Shader.Find("Particles/Additive"));
            lineRendererBackDrop.startColor = Color.blue;
            lineRendererBackDrop.endColor = Color.blue;
            lineRendererBackDrop.startWidth = 0.0015F;
            lineRendererBackDrop.endWidth = 0.0015F;
            lineRendererBackDrop.positionCount = this._numPointsBackDrop;
            //lineRendererBackDrop.loop = true;



            Vector3[] pointsBackDrop = new Vector3[this._numPointsBackDrop];
            //multiply 'i' by '1.0f' to ensure the result is a fraction

            int i = 0;
            while (i < _numPointsBackDrop)

            {

                float pointNumBackDrop = (i * 1.0f) / _numPointsBackDrop;


                float angleBackDrop = pointNumBackDrop * Mathf.PI;
                float angleBackDropReversed = angleBackDrop * -1;


                pointsBackDrop[i] = new Vector3(Mathf.Sin(angleBackDrop) * _radiusXBackDrop, _BackDropYValueFloat, Mathf.Cos(angleBackDrop) * _radiusYBackDrop);
                // Instantiate(_pointPrefabBackDrop, pointsBackDrop[i], Quaternion.identity);
                // Instantiate(datePrefabBackDrop, pointsBackDrop[i], Quaternion.identity);


                i++;

            }
            lineRendererBackDrop.SetPositions(pointsBackDrop);
            _GameObjectBackDrop.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
        }
    }


    public class GraphBuilderASB
    {

        float _YValueFloat;
        GameObject _GameObjectGraphBuilder = new GameObject();

        GameObject _pointPrefab;
        GameObject _dateLabelPrefab;
        float _radiusX;
        float _radiusY;
        float _radiusXSlatBG;
        float _radiusYSlatBG;
        float SlatGapFloat = 0.016F;

        float _radiusXSlatBG2;
        float _radiusYSlatBG2;
        float SlatGapFloat2 = 0.03F;
        int _numPoints;

        DateTime _startDate;
        DateTime _endDate;
        int _IndexOfStartDateGB;
        int _IndexOfEndDateGB;
        float _normalizedValue;

        List<Dictionary<string, int>> _XMLListReturner;
        List<DateTime> _XMLDateReturner;

        int _intListReturned;
        DateTime _dateListReturned;


        LineTextureMode textureMode = LineTextureMode.Stretch;
        Color32 _ElectricBlueTransA;
        Color32 _GraphLineColor;
        Material _slatMaterial_Shade1;
        Material _slatMaterial_Shade2;
        Material _slatMaterial_Shade3;
        int _IndexOfRange1 = 0;
        int _IndexOfRange2 = 0;
        int _MetricGraphState;
        //FOR XML METRIC SELECT PASS VARIABLE
        TextAsset _MetricXMLGB;
        int _MetricXMLGB_Int_DD;
        float _SlatHeightGB1 = 0.1F;
        float _SlatHeightGB2 = 0.18F;
        public GraphBuilderASB(int MetricGraphState123, int MetricXMLGB_Int_DD, TextAsset MetricFromDropDownList, float RadiusX, float RadiusY, GameObject pointPrefab, DateTime startDate, DateTime endDate, GameObject dateLabelPrefab, GameObject SlatBGGameO, Material SlatMaterial_Shade1, Material SlatMaterial_Shade2, Material SlatMaterial_Shade3, Color32 ElectricBlueTransA, Color32 GraphLineColor)
        {

            this._MetricGraphState = MetricGraphState123;
            Debug.Log(MetricGraphState123 + "MGS MGS MGS MGS MGS MGS MG S MGS MGS");
            this._MetricXMLGB_Int_DD = MetricXMLGB_Int_DD;
            //FOR XML METRIC SELECT PASS VARIABLE
            this._MetricXMLGB = MetricFromDropDownList;
            string parentXmlNodeLabel = metricsDropDownListParentNodeLabel[_MetricXMLGB_Int_DD];
            string childXmlNodeLabel = metricsDropDownListChildNodeLabel[_MetricXMLGB_Int_DD];
            //
            this._radiusX = RadiusX;
            this._radiusY = RadiusY;
            this._radiusXSlatBG = this._radiusX + SlatGapFloat;
            this._radiusYSlatBG = this._radiusY + SlatGapFloat;
            this._radiusXSlatBG2 = this._radiusX + SlatGapFloat2;
            this._radiusYSlatBG2 = this._radiusY + SlatGapFloat2;
            this._dateLabelPrefab = dateLabelPrefab;
            this._pointPrefab = pointPrefab;
            this._startDate = startDate;
            this._endDate = endDate;
            this._normalizedValue = 0.0F;
            this._slatMaterial_Shade1 = SlatMaterial_Shade1;
            this._slatMaterial_Shade2 = SlatMaterial_Shade2;
            this._slatMaterial_Shade3 = SlatMaterial_Shade3;
            this._ElectricBlueTransA = ElectricBlueTransA;
            this._GraphLineColor = GraphLineColor;
            //CALCULATING THE INDEX START DATE AND END DATE AND NORMALIZED VALUE FROM THAT RANGE

            DailySessions.GetStartDate GBStartDate = new DailySessions.GetStartDate(_startDate, _MetricXMLGB_Int_DD, _MetricXMLGB);
            this._IndexOfStartDateGB = GBStartDate.CalculateStart();
            Debug.Log(this._IndexOfStartDateGB + "Index of where _startDate start date is -ArraySphereBackup");

            DailySessions.GetEndDate GBEndDate = new DailySessions.GetEndDate(_endDate, _MetricXMLGB_Int_DD, _MetricXMLGB);
            this._IndexOfEndDateGB = GBEndDate.CalculateEnd();
            Debug.Log(this._IndexOfEndDateGB + "Index of Where End Date is - ASB-GraphBuilder");

            //Number of Points might need change this if one value is greater than the other
            if (this._IndexOfStartDateGB < this._IndexOfEndDateGB)
            {
                this._numPoints = this._IndexOfEndDateGB - this._IndexOfStartDateGB;
                this._IndexOfRange1 = this._IndexOfStartDateGB;
                this._IndexOfRange2 = this._IndexOfEndDateGB;
            }
            else
            {
                this._numPoints = this._IndexOfStartDateGB - this._IndexOfEndDateGB;
                this._IndexOfRange2 = this._IndexOfStartDateGB;
                this._IndexOfRange1 = this._IndexOfEndDateGB;
            }
            //CONSTRUCTOR NAME(s) DailySessions.GetNormalizedValue GBNormalizedValueConstructor ARE NOT INDEPENDENT PRIVATE VARIABLES MAY CAUSE AN ISSUE WITH MULTI INVOKATION

            DailySessions.GetNormalizedValue GBNormalizedValueConstructor = new DailySessions.GetNormalizedValue(this._IndexOfRange1, this._IndexOfRange2, _MetricXMLGB_Int_DD, _MetricXMLGB);

            this._normalizedValue = GBNormalizedValueConstructor.CalculateNormalizedValue();
            Debug.Log(_normalizedValue + "Normalized value based on input - ASB-GraphBuilder NORMALIZER-NORMALIZER-NORMALIZER-NORMALIZER-NORMALIZER");

            DailySessions.GetXmlArray CXml = new DailySessions.GetXmlArray(_MetricXMLGB_Int_DD, _MetricXMLGB);
            _XMLListReturner = CXml.ReturnXmlArray();


            DailySessions.GetXmlDateArray DateXml = new DailySessions.GetXmlDateArray(_MetricXMLGB_Int_DD, _MetricXMLGB);
            _XMLDateReturner = DateXml.ReturnXmlDateArray();



            Material newMat = Resources.Load("ParticleNew", typeof(Material)) as Material;
            LineRenderer lineRenderer = this._GameObjectGraphBuilder.AddComponent<LineRenderer>();
            // lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.material = newMat;
            lineRenderer.textureMode = textureMode;
            lineRenderer.GetComponent<Renderer>().material.SetColor("_Color", _GraphLineColor);
            lineRenderer.GetComponent<Renderer>().material.SetColor("_EmissionColor", _GraphLineColor);

            lineRenderer.startColor = _GraphLineColor;
            lineRenderer.endColor = _GraphLineColor;
            lineRenderer.startWidth = 0.0025F;
            lineRenderer.endWidth = 0.0025F;
            lineRenderer.numCornerVertices = 5;
            lineRenderer.positionCount = _numPoints;


            Vector3[] points = new Vector3[_numPoints];
            Vector3[] Slatpoints = new Vector3[_numPoints];
            Vector3[] Slatpoints2 = new Vector3[_numPoints];
            //multiply 'i' by '1.0f' to ensure the result is a fraction


            int j = 0;
            while (j < _numPoints)

            {
                float pointNum = (j * 1.0f) / _numPoints;
                float angle = pointNum * Mathf.PI;
                Slatpoints[j] = new Vector3(Mathf.Sin(angle) * _radiusXSlatBG, 0.0F, Mathf.Cos(angle) * _radiusYSlatBG);
                Slatpoints2[j] = new Vector3(Mathf.Sin(angle) * _radiusXSlatBG2, 0.0F, Mathf.Cos(angle) * _radiusYSlatBG2);
                j++;
            }

            float _slatWidthIn = Vector3.Distance(Slatpoints[1], Slatpoints[2]);
            float _slatWidthIn2 = Vector3.Distance(Slatpoints2[1], Slatpoints2[2]);
            Debug.Log(_slatWidthIn + "SLAT SLAT SLATSLAT SLAT SLATSLAT SLAT SLATSLAT SLAT SLATSLAT SLAT SLATSLAT SLAT SLATSLAT SLAT SLATSLAT SLAT SLAT");




            int i = 0;
            while (i < _numPoints)

            {


                float pointNum = (i * 1.0f) / _numPoints;

                _XMLListReturner[_IndexOfStartDateGB + i].TryGetValue(childXmlNodeLabel, out _intListReturned);
                //DateInputter For PointPrefab CoRoutine
                _dateListReturned = _XMLDateReturner[_IndexOfStartDateGB + i];

                float smallY = _intListReturned;
                float smallYConverted = (smallY / _normalizedValue) * 0.4F;
                // Debug.Log(smallYConverted);
                //angle along the unit circle for placing points
                float angle = pointNum * Mathf.PI;
                points[i] = new Vector3(Mathf.Sin(angle) * _radiusX, smallYConverted / 4, Mathf.Cos(angle) * _radiusY);
                GameObject _Billy = Instantiate(_pointPrefab, points[i], Quaternion.identity);

                _Billy.transform.GetChild(0).transform.GetComponent<Renderer>().material.SetColor("_Color", _GraphLineColor);
                string textToDisplay = _intListReturned.ToString();
                Text ImbuePointWithDataTextValue = _Billy.transform.GetChild(1).GetChild(0).transform.GetComponent<Text>();
                ImbuePointWithDataTextValue.text = textToDisplay;
                ImbuePointWithDataTextValue.color = new Color(255f, 255f, 255f, 0.0f);
                //  _Billy.gameObject.GetComponent<Renderer>().material = newMat;
                // _Billy.gameObject.GetComponent<Material>().SetColor("_Color", _GraphLineColor);
                if (_MetricGraphState == 0)
                {
                    _Billy.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    //-----------------------DATE LABEL SCALE FACTOR FOR WHEN A NUMBER OF POINTS WILL NOT FIT TOGETHER
                    DateLabeller DATELABELLERYEAR = new DateLabeller(_numPoints, _dateListReturned, dateLabelPrefab, points[i].x, points[i].z);

                    if (_dateListReturned.Day == 1) { 
                    VerticalLineGenerator VLG2 = new VerticalLineGenerator(points[i].x, points[i].z, 0.1F, Color.blue);
                }
                    if (i == 0 || i == _numPoints - 1)
                    {
                     //  GraphBGSlats SLATGENERATOR1 = new GraphBGSlats(_SlatHeightGB1, SlatBGGameO, Slatpoints[i].x + _slatWidthIn2 / 2, Slatpoints[i].z, (_slatWidthIn) / 2, _dateListReturned, _slatMaterial_Shade1, _slatMaterial_Shade2);
                        GraphBGSlats SLATGENERATOR2 = new GraphBGSlats(_SlatHeightGB2, SlatBGGameO, Slatpoints2[i].x, Slatpoints2[i].z, _slatWidthIn2 + 0.034F, _dateListReturned, _slatMaterial_Shade3, _slatMaterial_Shade3);
                        VerticalLineGenerator VLG2 = new VerticalLineGenerator(points[i].x, points[i].z, 0.1F, Color.blue);

                    }
                    else
                    {
                     //   GraphBGSlats SLATGENERATOR3 = new GraphBGSlats(_SlatHeightGB1, SlatBGGameO, Slatpoints[i].x, Slatpoints[i].z, _slatWidthIn, _dateListReturned, _slatMaterial_Shade1, _slatMaterial_Shade2);
                        GraphBGSlats SLATGENERATOR4 = new GraphBGSlats(_SlatHeightGB2, SlatBGGameO, Slatpoints2[i].x, Slatpoints2[i].z, _slatWidthIn2, _dateListReturned, _slatMaterial_Shade3, _slatMaterial_Shade3);

                    }
                    VerticalLineGenerator VLG1 = new VerticalLineGenerator(points[i].x, points[i].z, points[i].y, ElectricBlueTransA);
                   // VerticalLineGenerator VLG2 = new VerticalLineGenerator(points[i].x, points[i].z, 0.1F, ElectricBlueTransA);
                }


                if (_MetricGraphState > 0) { _Billy.transform.SetParent(GameObject.Find("ParentalMetricAGameObject").transform); }
               // VerticalLineGenerator VLG2 = new VerticalLineGenerator(points[i].x, points[i].z, points[i].y, ElectricBlueTransA);
                //Set the text box's text element to the current textToDisplay:
                //string textToDisplay = tempTextBox.text;


                //Vertical Line Generator based on point, class is below


                i++;

                Debug.Log(i);
              
            }
            lineRenderer.SetPositions(points);
            if (_MetricGraphState == 0) { _GameObjectGraphBuilder.transform.SetParent(GameObject.Find("ParentalAGameObject").transform); }
            if (_MetricGraphState > 0) { _GameObjectGraphBuilder.transform.SetParent(GameObject.Find("ParentalMetricAGameObject").transform); }
           
        }

        public class DateLabeller
        {
            DateTime _dateLabellerInput;
            Vector3 _VectorVDateLabelLine;
            Vector3 _VectorVDateLabelLineMonth;

            private string _DateLabelDaytextToDisplay;
            private string _Date14textToDisplay;
            private string _Date14textToDisplayMonth;
            private string _DateLabelMonthtextToDisplay;
            float _Point_X_BottomOfDateLine;
            float _Point_Z_BottomOfDateLine;
            int _numPointsLabller;

            public DateLabeller(int Numpoints, DateTime DateLabellerInput, GameObject dateLabelPrefab, float Point_X_BottomOfDateLine, float Point_Z_BottomOfDateLine)
            {
                this._dateLabellerInput = DateLabellerInput;

                this._numPointsLabller = Numpoints;
                GameObject _dateLabelPrefab = dateLabelPrefab;
                this._Point_X_BottomOfDateLine = Point_X_BottomOfDateLine;
                this._Point_Z_BottomOfDateLine = Point_Z_BottomOfDateLine;
             
                _VectorVDateLabelLine = new Vector3(this._Point_X_BottomOfDateLine, -0.015F, this._Point_Z_BottomOfDateLine);
                _VectorVDateLabelLineMonth = new Vector3(this._Point_X_BottomOfDateLine -0.003F, -0.033F, this._Point_Z_BottomOfDateLine);



                if (_dateLabellerInput.Day == 1)
                {
                   
                    GameObject _DateLabelledMonth = Instantiate(_dateLabelPrefab, _VectorVDateLabelLineMonth, Quaternion.identity);
                 
                    this._DateLabelMonthtextToDisplay = _dateLabellerInput.ToString("MMMM");

                    this._Date14textToDisplayMonth = _DateLabelMonthtextToDisplay;

                    Text ImbuePointWithDateLabel14ValueMonth = _DateLabelledMonth.transform.GetChild(0).transform.GetComponent<Text>();
                  
                    ImbuePointWithDateLabel14ValueMonth.text = this._Date14textToDisplayMonth;
                    ImbuePointWithDateLabel14ValueMonth.fontSize = 34;
                    ImbuePointWithDateLabel14ValueMonth.alignment = TextAnchor.LowerLeft;
                   
                    ImbuePointWithDateLabel14ValueMonth.color = new Color(255f, 255f, 255f, 1.0f);
                  
                    _DateLabelledMonth.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);

                }
                if (_dateLabellerInput.Day == 15)
                {

                    GameObject _DateLabelledMonth = Instantiate(_dateLabelPrefab, _VectorVDateLabelLineMonth, Quaternion.identity);

                    this._DateLabelMonthtextToDisplay = _dateLabellerInput.ToString("MMMM yyyy");

                    this._Date14textToDisplayMonth = _DateLabelMonthtextToDisplay;

                    Text ImbuePointWithDateLabel14ValueMonth = _DateLabelledMonth.transform.GetChild(0).transform.GetComponent<Text>();

                    ImbuePointWithDateLabel14ValueMonth.text = this._Date14textToDisplayMonth;
                    ImbuePointWithDateLabel14ValueMonth.fontSize = 34;
                    ImbuePointWithDateLabel14ValueMonth.alignment = TextAnchor.LowerCenter;

                    ImbuePointWithDateLabel14ValueMonth.color = new Color(255f, 255f, 255f, 1.0f);

                    _DateLabelledMonth.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);

                }


                if (_numPointsLabller <= 182)
                    {
                        GameObject _DateLabelled = Instantiate(_dateLabelPrefab, _VectorVDateLabelLine, Quaternion.identity);
                        this._DateLabelDaytextToDisplay = _dateLabellerInput.Day.ToString();
                        Text ImbuePointWithDateLabelValue = _DateLabelled.transform.GetChild(0).transform.GetComponent<Text>();
                        ImbuePointWithDateLabelValue.text = this._DateLabelDaytextToDisplay;
                        ImbuePointWithDateLabelValue.color = new Color(255f, 255f, 255f, 1.0f);
                        _DateLabelled.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    }
                    if (_numPointsLabller > 182 && _numPointsLabller <= 365 && _dateLabellerInput.Day % 2 == 0)
                    {
                        GameObject _DateLabelled = Instantiate(_dateLabelPrefab, _VectorVDateLabelLine, Quaternion.identity);
                        this._DateLabelDaytextToDisplay = _dateLabellerInput.Day.ToString();
                        Text ImbuePointWithDateLabelValue = _DateLabelled.transform.GetChild(0).transform.GetComponent<Text>();
                        ImbuePointWithDateLabelValue.text = this._DateLabelDaytextToDisplay;
                        ImbuePointWithDateLabelValue.color = new Color(255f, 255f, 255f, 1.0f);
                        _DateLabelled.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    }

                    if (_numPointsLabller > 365 && _numPointsLabller <= 547 && _dateLabellerInput.Day % 3 == 0)
                    {
                        GameObject _DateLabelled = Instantiate(_dateLabelPrefab, _VectorVDateLabelLine, Quaternion.identity);
                        this._DateLabelDaytextToDisplay = _dateLabellerInput.Day.ToString();
                        Text ImbuePointWithDateLabelValue = _DateLabelled.transform.GetChild(0).transform.GetComponent<Text>();
                        ImbuePointWithDateLabelValue.text = this._DateLabelDaytextToDisplay;
                        ImbuePointWithDateLabelValue.color = new Color(255f, 255f, 255f, 1.0f);
                        _DateLabelled.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    }
                    if (_numPointsLabller > 547 && _numPointsLabller <= 729 && _dateLabellerInput.Day % 4 == 0)
                    {
                        GameObject _DateLabelled = Instantiate(_dateLabelPrefab, _VectorVDateLabelLine, Quaternion.identity);
                        this._DateLabelDaytextToDisplay = _dateLabellerInput.Day.ToString();
                        Text ImbuePointWithDateLabelValue = _DateLabelled.transform.GetChild(0).transform.GetComponent<Text>();
                        ImbuePointWithDateLabelValue.text = this._DateLabelDaytextToDisplay;
                        ImbuePointWithDateLabelValue.color = new Color(255f, 255f, 255f, 1.0f);
                        _DateLabelled.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    }
                    if (_numPointsLabller > 729 && _numPointsLabller <= 911 && _dateLabellerInput.Day % 5 == 0)
                    {
                        GameObject _DateLabelled = Instantiate(_dateLabelPrefab, _VectorVDateLabelLine, Quaternion.identity);
                        this._DateLabelDaytextToDisplay = _dateLabellerInput.Day.ToString();
                        Text ImbuePointWithDateLabelValue = _DateLabelled.transform.GetChild(0).transform.GetComponent<Text>();
                        ImbuePointWithDateLabelValue.text = this._DateLabelDaytextToDisplay;
                        ImbuePointWithDateLabelValue.color = new Color(255f, 255f, 255f, 1.0f);
                        _DateLabelled.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    }
                    if (_numPointsLabller > 911 && _numPointsLabller <= 1095 && _dateLabellerInput.Day % 6 == 0)
                    {
                        GameObject _DateLabelled = Instantiate(_dateLabelPrefab, _VectorVDateLabelLine, Quaternion.identity);
                        this._DateLabelDaytextToDisplay = _dateLabellerInput.Day.ToString();
                        Text ImbuePointWithDateLabelValue = _DateLabelled.transform.GetChild(0).transform.GetComponent<Text>();
                        ImbuePointWithDateLabelValue.text = this._DateLabelDaytextToDisplay;
                        ImbuePointWithDateLabelValue.color = new Color(255f, 255f, 255f, 1.0f);
                        _DateLabelled.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);
                    }

                

            }

        }
        public class VerticalLineGenerator
        {
            GameObject _GameObjectVerticalLine = new GameObject();
            Vector3[] _VectorVLine = new Vector3[2];

            float _Point_X_BottomOfVLine;
            float _Point_Z_BottomOfVLine;

            float _HeightOfLine = 0.1F;

            Color32 _ElectricBlueTransA;


            public VerticalLineGenerator(float Point_X_BottomOfVLine, float Point_Z_BottomOfVLine, float Point_Y_HeightOfVLine, Color32 ElectricBlueTransA)

            {
                this._Point_X_BottomOfVLine = Point_X_BottomOfVLine;
                this._Point_Z_BottomOfVLine = Point_Z_BottomOfVLine;
                this._HeightOfLine = Point_Y_HeightOfVLine;
                this._ElectricBlueTransA = ElectricBlueTransA;
                LineRenderer _lineRendererVerticalLine = this._GameObjectVerticalLine.AddComponent<LineRenderer>();
                _lineRendererVerticalLine.material = new Material(Shader.Find("Particles/Additive"));
                _lineRendererVerticalLine.startColor = _ElectricBlueTransA;
                _lineRendererVerticalLine.endColor = _ElectricBlueTransA;
                _lineRendererVerticalLine.startWidth = 0.0008F;
                _lineRendererVerticalLine.endWidth = 0.0008F;
                _lineRendererVerticalLine.sortingLayerName = "Foreground";

                _VectorVLine[0] = new Vector3(this._Point_X_BottomOfVLine, -0.005F, this._Point_Z_BottomOfVLine);
                _VectorVLine[1] = new Vector3(this._Point_X_BottomOfVLine, _HeightOfLine, this._Point_Z_BottomOfVLine);
                // _VectorVLine[0] = new Vector3(0.0F, 0.0F, 0.0F);
                // _VectorVLine[1] = new Vector3(1.0F, 0.1F, 0.0F);

                _lineRendererVerticalLine.SetPositions(_VectorVLine);
               // if (_MetricGraphState == 1) { _GameObjectVerticalLine.transform.SetParent(GameObject.Find("ParentalAGameObject").transform); }
                _GameObjectVerticalLine.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);

            }
        }

        public class GraphBGSlats
        {
            float _slatWidth;
            float _slatHeigth;
            GameObject _slatCloneGameObject;
            Vector3 _slatVector;
            float _point_Z_SlatLine;
            float _point_X_SlatLine;
            int SlatDateColorChecker;
            Material _SlatMeth1;
            Material _SlatMeth2;

            public GraphBGSlats(float SlatHeight, GameObject SlatBGGameO, float Point_X_SlatLine, float Point_Z_SlatLine, float _slatWidthIn, DateTime SlatDateColorChecker, Material _SlatMethMaterial_Shade1, Material _SlatMethMaterial_Shade2)
            {
                this._slatHeigth = SlatHeight;
                this._point_X_SlatLine = Point_X_SlatLine;
                this._point_Z_SlatLine = Point_Z_SlatLine;
                this._slatWidth = _slatWidthIn;
                this.SlatDateColorChecker = SlatDateColorChecker.Month;
                this._SlatMeth1 = _SlatMethMaterial_Shade1;
                this._SlatMeth2 = _SlatMethMaterial_Shade2;
                _slatVector = new Vector3(this._point_X_SlatLine, 0.048F, this._point_Z_SlatLine);

                GameObject _slatCloneGameObject = Instantiate(SlatBGGameO, _slatVector, Quaternion.identity);
                if (this.SlatDateColorChecker % 2 == 0)

                {
                    // is even change material
                    _slatCloneGameObject.gameObject.GetComponent<Renderer>().material = _SlatMeth1;
                }

                else
                {
                    //Is Odd change material
                    _slatCloneGameObject.gameObject.GetComponent<Renderer>().material = _SlatMeth2;
                }




                _slatCloneGameObject.transform.localScale = new Vector3(_slatWidth, _slatHeigth, 0.006F);

                _slatCloneGameObject.transform.LookAt(Vector3.zero);
                float _YSlatLookAtValue = _slatCloneGameObject.transform.eulerAngles.y;
                _slatCloneGameObject.transform.rotation = Quaternion.Euler(0, _YSlatLookAtValue, 0);
                _slatCloneGameObject.transform.SetParent(GameObject.Find("ParentalAGameObject").transform);

            }
        }
    }
}
