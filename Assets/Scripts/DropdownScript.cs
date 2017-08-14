using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Specialized;

public class DropdownScript : MonoBehaviour
{
    //===============================================
    //  FROM DAILYSESSIONS VARIABLES  
   // public Text PublicPercentLoader;
    // public Material MetricMaterial_1_Green;
    // public Material MetricMaterial_2_Purple;
    // public Material MetricMaterial_3_Orange;
    public static float MainRadius = 0.75F;
    public static float HorLineYValueBottom = -0.005F;
    public static float HorLineYValueTop = 0.1F;
    Color32 ElectricBlueTransA = new Color32(0, 0, 255, 255);
    Color32 ParticleGreen = new Color32(72, 232, 170, 255);
    Color32 ParticleGreen2 = new Color32(0, 233, 143, 255);

    //public Color32 ElectricBlueTransA = new Color32(255, 0, 0, 255);
    //public Color32 ParticleGreen = new Color32(255, 0, 0, 255);
    //public Color32 ParticleGreen2 = new Color32(255, 0, 0, 255);

    public Material SlatMaterial_Shade1;
    public Material SlatMaterial_Shade2;
    public Material SlatMaterial_Shade3;

    public static DateTime inputDate2;
    public static DateTime datespanJan2;
    //  public GameObject pointPrefabBackDrop;
    // public GameObject pointPrefabCS;
    public GameObject pointPrefab;
    public GameObject pointPrefabCapsule;
    public GameObject pointPrefabCube;
    public GameObject dateLabelPrefab;
    public GameObject SlatBGGameO;


    public DateTime GBinputStartDate = new DateTime(YearA, MonthA, DayA);
    public DateTime GBinputEndDate = new DateTime(YearA2, MonthA2, DayA2);
    public DateTime GBinputStartDateConvert = new DateTime(2015, 1, 13);
    public DateTime GBinputEndDateConvert = new DateTime(2015, 3, 19);
    public DateTime GBinputStartDate2 = new DateTime(2015, 1, 13);
    public DateTime GBinputEndDate2 = new DateTime(2015, 3, 19);
    //-----------------------------------------------
    //if GBinpitStartDate is later than End Date Then

    List<string> months_DD_A1 = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
    List<string> years_DD_A1 = new List<string>() { "2015", "2016", "2017" };

    public static List<string> metricsDropDownList = new List<string>() { "Metric 1", "User Sessions", "New Users", "Bounce Rate" };
    public static List<string> metricsDropDownListCompared = new List<string>() { "Metric 2", "User Sessions", "New Users", "Bounce Rate" };

    public static List<string> metricsDropDownListParentNodeLabel = new List<string>();
    public static List<string> metricsDropDownListChildNodeLabel = new List<string>();


    //DropDown Range 1 on left of menu
    public static int DayA = 1;
    public static int MonthA = 1;
    public static int YearA = 2015;

    public int ns_MonthA = 1;

    public int ns_YearAt = 2015;

    //DropDown Range 2 on right of menu

    public static int DayA2 = 1;
    public static int MonthA2 = 2;
    public static int YearA2 = 2015;
    public int ns_MonthB1 = 2;
    public int ns_YearB2 = 2015;

    public GameObject ParentalAGameObject;
    public GameObject ParentalMetricAGameObject;


    public Dropdown dropdown;
    public Dropdown dropdownA2;

    public Dropdown dropdownB;
    public Dropdown dropdownB2;

    public Dropdown dropdownMetrics;
    public Dropdown dropdownMetricsCompared;
    //public int Metric1FromDropDownList = 0;
    public int MetricGraphState123 = 0;
    public int BMetricGraphState123 = 0;
    public TextAsset GameAsset;
    public TextAsset GameAssetb;
    public TextAsset GameAssetc;

    public int Metric1FromDropDownList = 0;
    public TextAsset _GameAssetMetric1Local = new TextAsset();

    void Start()
    {
        populateList();
        addMetricForXMLSyncList();
        CheckDateLateness();
        
        Debug.Log(ns_YearAt + "Year A START in A first NS YEAR NS YEAR NS YEAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");


    }
    public void addMetricForXMLSyncList()
    {

        metricsDropDownListParentNodeLabel.Add("xsessions");
        metricsDropDownListChildNodeLabel.Add("xnumber-sessions");

        metricsDropDownListParentNodeLabel.Add("xsessions");
        metricsDropDownListChildNodeLabel.Add("xnumber-sessions");

        metricsDropDownListParentNodeLabel.Add("xsessions");
        metricsDropDownListChildNodeLabel.Add("xnumber-sessions");

        metricsDropDownListParentNodeLabel.Add("xsessions");
        metricsDropDownListChildNodeLabel.Add("xnumber-sessions");
    }

    public void Dropdown_2MetricIndexChanged(int Metric_DropDownIndex)
    {
        dropdownMetrics.value = Metric_DropDownIndex;
        removeParentGameO();
        removeParentMetricGameO();
        MetricGraphState123 = Metric_DropDownIndex;

        if (Metric_DropDownIndex == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else
        {
            ParentalAGameObject = new GameObject();
            ParentalAGameObject.name = "ParentalAGameObject";
            GBinputStartDateConvert = new DateTime(ns_YearAt, ns_MonthA, DayA);
            GBinputEndDateConvert = new DateTime(ns_YearB2, ns_MonthB1, DayA);
        

            CheckDateLateness();

            Metric1FromDropDownList = Metric_DropDownIndex;
            GetTextAsset GetMetric = new GetTextAsset(MetricGraphState123, GameAsset, GameAssetb, GameAssetc);
            _GameAssetMetric1Local = GetMetric.ReturnTextAsset();


            XmlInterface.MakeBackDropLineHor Lino = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, HorLineYValueTop, MainRadius, MainRadius, GBinputStartDateConvert, GBinputEndDateConvert);

            XmlInterface.MakeBackDropLineHor Lino2 = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, HorLineYValueBottom, MainRadius, MainRadius, GBinputStartDateConvert, GBinputEndDateConvert);
            XmlInterface.GraphBuilderASB MetricLino4 = new XmlInterface.GraphBuilderASB(0, MetricGraphState123, _GameAssetMetric1Local, MainRadius, MainRadius, pointPrefab, GBinputStartDateConvert, GBinputEndDateConvert, dateLabelPrefab, SlatBGGameO, SlatMaterial_Shade1, SlatMaterial_Shade2, SlatMaterial_Shade3, ParticleGreen2, ParticleGreen);
            if (BMetricGraphState123 != 0)
            {
                BMetricBuilder();
            }
        }


    }
    public void Dropdown_2MetricIndexChanged_Compared(int Metric_DropDownIndexB)
    {
        dropdownMetricsCompared.value = Metric_DropDownIndexB;
        removeParentMetricGameO();
        BMetricGraphState123 = Metric_DropDownIndexB;

        if (Metric_DropDownIndexB == 0 || MetricGraphState123 == 0)
        {
            removeParentMetricGameO();

        }
        else
        {
            BMetricBuilder();
        }


    }

   
    public void DateConstructor()
    {

        ParentalAGameObject = new GameObject();
        ParentalAGameObject.name = "ParentalAGameObject";

        GBinputStartDate = new DateTime(ns_YearAt, ns_MonthA, DayA);
        GBinputEndDate = new DateTime(ns_YearB2, ns_MonthB1, DayA);

        CheckDateLateness();
        GetTextAsset GetMetric = new GetTextAsset(MetricGraphState123, GameAsset, GameAssetb, GameAssetc);
        _GameAssetMetric1Local = GetMetric.ReturnTextAsset();
        XmlInterface.MakeBackDropLineHor Lino = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, HorLineYValueTop, MainRadius, MainRadius, GBinputStartDateConvert, GBinputEndDateConvert);

        XmlInterface.MakeBackDropLineHor Lino2 = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, HorLineYValueBottom, MainRadius, MainRadius, GBinputStartDateConvert, GBinputEndDateConvert);
        XmlInterface.GraphBuilderASB MetricLino4 = new XmlInterface.GraphBuilderASB(0, MetricGraphState123, _GameAssetMetric1Local, MainRadius, MainRadius, pointPrefab, GBinputStartDateConvert, GBinputEndDateConvert, dateLabelPrefab, SlatBGGameO, SlatMaterial_Shade1, SlatMaterial_Shade2, SlatMaterial_Shade3, ParticleGreen2, ParticleGreen);
        if (BMetricGraphState123 != 0)
        {
            BMetricBuilder();
        }
    }
    public void BMetricBuilder()
    {
        ParentalMetricAGameObject = new GameObject();
        ParentalMetricAGameObject.name = "ParentalMetricAGameObject";
        GBinputStartDateConvert = new DateTime(ns_YearAt, ns_MonthA, DayA);
        GBinputEndDateConvert = new DateTime(ns_YearB2, ns_MonthB1, DayA);

        CheckDateLateness();

        GetTextAsset GetMetric = new GetTextAsset(BMetricGraphState123, GameAsset, GameAssetb, GameAssetc);
        _GameAssetMetric1Local = GetMetric.ReturnTextAsset();

       // XmlInterface.MakeBackDropLineHor Lino = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, -0.005F, 0.805F, 0.805F, GBinputStartDateConvert, GBinputEndDateConvert);
        XmlInterface.GraphBuilderASB MetricLino4 = new XmlInterface.GraphBuilderASB(BMetricGraphState123, BMetricGraphState123, _GameAssetMetric1Local, 0.745F, 0.745F, pointPrefab, GBinputStartDateConvert, GBinputEndDateConvert, dateLabelPrefab, SlatBGGameO, SlatMaterial_Shade1, SlatMaterial_Shade2, SlatMaterial_Shade3, ElectricBlueTransA, ElectricBlueTransA);
    }

    public void CheckDateLateness()
    {


        if (GBinputStartDate.Date < GBinputEndDate.Date)
        {
            GBinputStartDateConvert = GBinputStartDate;
            GBinputEndDateConvert = GBinputEndDate;
        }
        else
        {
            GBinputStartDateConvert = GBinputEndDate;
            GBinputEndDateConvert = GBinputStartDate;
        }
    }
    public void Dropdown_IndexChanged(int DropDownIndex)
    {
        dropdown.value = DropDownIndex;

        removeParentGameO();
        removeParentMetricGameO();
        ns_MonthA = DropDownIndex + 1;
        GBinputStartDate = new DateTime(ns_YearAt, ns_MonthA, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
           
           
        }
        else
        {
            DateConstructor();

        }
    }
    public void Dropdown_IndexChangedA1(int DropDownIndexA1)
    {
        dropdownA2.value = DropDownIndexA1;
        removeParentGameO();
        removeParentMetricGameO();
        ns_YearAt = DropDownIndexA1 + 2015;
        GBinputStartDate = new DateTime(ns_YearAt, ns_MonthA, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
           
   
        }
        else

        {
            DateConstructor();
           
        }
    }

    public void Dropdown_IndexChangedB1(int DropDownIndex)
    {
        dropdownB.value = DropDownIndex;
        removeParentGameO();
        removeParentMetricGameO();
        ns_MonthB1 = DropDownIndex + 1;
        GBinputEndDate = new DateTime(ns_YearB2, ns_MonthB1, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
       
        }
        else
        {
            DateConstructor();
        }
    }
 
    public void Dropdown_IndexChangedB2(int DropDownIndexA1)
    {
        dropdownB2.value = DropDownIndexA1;
        removeParentGameO();
        removeParentMetricGameO();
        ns_YearB2 = DropDownIndexA1 + 2015;
        GBinputEndDate = new DateTime(ns_YearB2, ns_MonthB1, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else
        {
            DateConstructor();
        }
    }


    public void removeParentGameO()
    {
        DestroyImmediate(ParentalAGameObject);
            }

    public void removeParentMetricGameO()
    {
        DestroyImmediate(ParentalMetricAGameObject);
            }


    void populateList()
    {
        dropdown.AddOptions(months_DD_A1);
        dropdownA2.AddOptions(years_DD_A1);
        dropdownB.AddOptions(months_DD_A1);
        dropdownB2.AddOptions(years_DD_A1);

        dropdownMetrics.AddOptions(metricsDropDownList);
        dropdownMetricsCompared.AddOptions(metricsDropDownListCompared);
    }


    public class GetTextAsset
    {
        int _Metric1DD_input;
        TextAsset _ReturnTextAsset;

        public GetTextAsset(int Metric1DD_Input, TextAsset Metric1, TextAsset Metric2, TextAsset Metric3)
        {

            this._Metric1DD_input = Metric1DD_Input;
            if (this._Metric1DD_input == 0)
            {
                _ReturnTextAsset = Metric1;
                    }
            if (this._Metric1DD_input == 1)
            {
                _ReturnTextAsset = Metric1;
                    }
            if (this._Metric1DD_input == 2)
            {
                _ReturnTextAsset = Metric2;
                    }
            if (this._Metric1DD_input == 3)
            {
                _ReturnTextAsset = Metric3;
                    }
        }

        public TextAsset ReturnTextAsset()
        {
            return this._ReturnTextAsset;
        }
    }

    //-------------------------------------------------STARTDATE VOICE COMMANDS---------------
    public void VoiceStartDate2015(int VoiceStartIndex)
    {

        removeParentGameO();
        removeParentMetricGameO();

        int VoiceYearIndex = 2015;

        dropdown.value = VoiceStartIndex;
        dropdownA2.value = 0;

        ns_MonthA = VoiceStartIndex + 1;
        ns_YearAt = VoiceYearIndex;

        GBinputStartDate = new DateTime(ns_YearAt, ns_MonthA, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else

        {
            removeParentGameO();
            removeParentMetricGameO();
            DateConstructor();

        }
    }
    public void VoiceStartDate2016(int VoiceStartIndex)
    {

        removeParentGameO();
        removeParentMetricGameO();

        int VoiceYearIndex = 2016;

        dropdown.value = VoiceStartIndex;
        dropdownA2.value = 1;

        ns_MonthA = VoiceStartIndex + 1;
        ns_YearAt = VoiceYearIndex;

        GBinputStartDate = new DateTime(ns_YearAt, ns_MonthA, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else

        {
            removeParentGameO();
            removeParentMetricGameO();
            DateConstructor();

        }
    }
    public void VoiceStartDate2017(int VoiceStartIndex)
    {

        removeParentGameO();
        removeParentMetricGameO();

        int VoiceYearIndex = 2017;

        dropdown.value = VoiceStartIndex;
        dropdownA2.value = 2;

        ns_MonthA = VoiceStartIndex + 1;
        ns_YearAt = VoiceYearIndex;

        GBinputStartDate = new DateTime(ns_YearAt, ns_MonthA, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else

        {
            removeParentGameO();
            removeParentMetricGameO();
            DateConstructor();

        }
    }

    //-------------------------------------------------ENDDATE VOICE COMMANDS---------------
    public void VoiceEndDate2015(int VoiceStartIndex)
    {

        removeParentGameO();
        removeParentMetricGameO();

        int VoiceYearIndex = 2015;

        dropdownB.value = VoiceStartIndex;
        dropdownB2.value = 0;

        ns_MonthB1 = VoiceStartIndex + 1;
        ns_YearB2 = VoiceYearIndex;
        GBinputEndDate = new DateTime(ns_YearB2, ns_MonthB1, DayA);
      

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else

        {
            removeParentGameO();
            removeParentMetricGameO();
            DateConstructor();

        }
    }
    public void VoiceEndDate2016(int VoiceStartIndex)
    {

        removeParentGameO();
        removeParentMetricGameO();

        int VoiceYearIndex = 2016;

        dropdownB.value = VoiceStartIndex;
        dropdownB2.value = 1;

        ns_MonthB1 = VoiceStartIndex + 1;
        ns_YearB2 = VoiceYearIndex;
        GBinputEndDate = new DateTime(ns_YearB2, ns_MonthB1, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else

        {
            removeParentGameO();
            removeParentMetricGameO();
            DateConstructor();

        }
    }
    public void VoiceEndDate2017(int VoiceStartIndex)
    {

        removeParentGameO();
        removeParentMetricGameO();

        int VoiceYearIndex = 2017;

        dropdownB.value = VoiceStartIndex;
        dropdownB2.value = 2;

        ns_MonthB1 = VoiceStartIndex + 1;
        ns_YearB2 = VoiceYearIndex;
        GBinputEndDate = new DateTime(ns_YearB2, ns_MonthB1, DayA);

        if (MetricGraphState123 == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else

        {
            removeParentGameO();
            removeParentMetricGameO();
            DateConstructor();

        }
    }
    //-------------------------------------------------METRICS 1 and 2 VOICE COMMANDS---------------
    public void VoiceMetric1(int VoiceMetric1Index)
    {
        dropdownMetrics.value = VoiceMetric1Index;
        removeParentGameO();
        removeParentMetricGameO();
        MetricGraphState123 = VoiceMetric1Index;

        if (VoiceMetric1Index == 0)
        {
            removeParentGameO();
            removeParentMetricGameO();
        }
        else
        {
            ParentalAGameObject = new GameObject();
            ParentalAGameObject.name = "ParentalAGameObject";
            GBinputStartDateConvert = new DateTime(ns_YearAt, ns_MonthA, DayA);
            GBinputEndDateConvert = new DateTime(ns_YearB2, ns_MonthB1, DayA);


            CheckDateLateness();

            Metric1FromDropDownList = VoiceMetric1Index;
            GetTextAsset GetMetric = new GetTextAsset(MetricGraphState123, GameAsset, GameAssetb, GameAssetc);
            _GameAssetMetric1Local = GetMetric.ReturnTextAsset();


            XmlInterface.MakeBackDropLineHor Lino = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, HorLineYValueTop, MainRadius, MainRadius, GBinputStartDateConvert, GBinputEndDateConvert);

            XmlInterface.MakeBackDropLineHor Lino2 = new XmlInterface.MakeBackDropLineHor(MetricGraphState123, _GameAssetMetric1Local, HorLineYValueBottom, MainRadius, MainRadius, GBinputStartDateConvert, GBinputEndDateConvert);
            XmlInterface.GraphBuilderASB MetricLino4 = new XmlInterface.GraphBuilderASB(0, MetricGraphState123, _GameAssetMetric1Local, MainRadius, MainRadius, pointPrefab, GBinputStartDateConvert, GBinputEndDateConvert, dateLabelPrefab, SlatBGGameO, SlatMaterial_Shade1, SlatMaterial_Shade2, SlatMaterial_Shade3, ParticleGreen, ParticleGreen);
            if (BMetricGraphState123 != 0)
            {
                BMetricBuilder();
            }
        }


    }


    public void VoiceMetric2(int VoiceMetric1Index)
    {
        dropdownMetricsCompared.value = VoiceMetric1Index;
        removeParentMetricGameO();
        BMetricGraphState123 = VoiceMetric1Index;

        if (VoiceMetric1Index == 0 || MetricGraphState123 == 0)
        {
            removeParentMetricGameO();

        }
        else
        {
            BMetricBuilder();
        }


    }

    void Update()
    {
      //Metric 1 and 2
       // Dropdown_2MetricIndexChanged(1);
     //   Dropdown_2MetricIndexChanged_Compared(2);
        //Date Month and Year StartDate
      //  Dropdown_IndexChanged(2);
     //   Dropdown_IndexChangedA1(1);
        //Date Month and Year EndDate
    //    Dropdown_IndexChangedB1(4);
    //    Dropdown_IndexChangedB2(1);

    }
}





