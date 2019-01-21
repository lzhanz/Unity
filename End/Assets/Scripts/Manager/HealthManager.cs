using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    private Scrollbar _GScroll;

    private Scrollbar _RScroll;

    private Image _sp;


    public static HealthManager Instance
    {
        get { return instance; }
    }

    //public GameObject goHealthBar;


    //public Scrollbar forDel;


    private Vector3 positionVec3 = new Vector3();
    //public Text HpTxt;
    private int index = 1;//当前血条阶段
    public int Index
    {
        get
        {
            return this.index;
        }

        set
        {
            this.index = value;
        }
    }

    private void Awake()
    {
        instance = this;
        _GScroll = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        _RScroll = GameObject.Find("HealthBar").GetComponent<Scrollbar>();
        _sp = GameObject.Find("fore").GetComponent<Image>();
    }

    void Start()
    {
        //timer = 10;
        //this.positionVec3 = this.transform.localPosition;
    }
    void Update()
    {
        CastAnimation(_GScroll.gameObject);
    }
    /// <summary>
    /// 血条当前值显示
    /// </summary>
    /// <param name="curValue">当前血量</param>
    /// <param name="maxValue">最大血量</param>
    /// <param name="isBoss">是否是Bosss</param>
    public void SetHealthValueText(int current, int max, bool isTrubans)
    {
        /*if (isTrubans)
        {
            float hp = ((float)current / (float)max) * 100;
            HpTxt.text = hp.ToString("f") + "%";
        }
        else
        {
            HpTxt.text = current + "/" + max;
        }*/
        if (current <= max / 2 && current > max / 4)
        {
            _sp.DOColor(new Color32(255, 125, 0, 255), 0.1f);
        }
        if (current <= max / 4)
        {
            _sp.DOColor(new Color32(255, 0, 0, 255), 0.1f);
        }

    }

    /// <summary>
    /// 血条状态处理
    /// </summary>
    /// <param name="curValue">当前血量</param>
    /// <param name="maxValue">最大血量</param>
    /// <param name="isBoss">是否是Bosss</param>
    public void UpdateHealthBar(int curValue, int maxValue, bool isTurbans)
    {
        //Scrollbar progressBar = null;
        //Image _sp = null;

        // progressBar = this.goHealthBar.GetComponent<Scrollbar>();


        //_sp = this.transform.Find("HealthBar/SlidingArea/forDel").GetComponent<Image>();


        if (curValue >= maxValue)
        {
            curValue = maxValue;
            //progressBar.size = 1;
            _GScroll.size = 1;
            return;
        }
        /*if (maxValue <= 0 && isTurbans == false)
        {
            this.gameObject.SetActive(false);
        }*/
        if (Index == 0) return;
        int valueOfLine = maxValue / Index;
        if (valueOfLine <= 0)
            return;
        int index = curValue / valueOfLine;
        if (curValue % valueOfLine == 0)
            index--;
        float value = (curValue - index * valueOfLine) / (float)valueOfLine;
        if (curValue <= 0)
            value = 0;
        if (null != _GScroll)
        {
            _GScroll.size = value;
        }
    }

    //血条中间图片滑动处理
    void CastAnimation(GameObject father)
    {
        Scrollbar progressBar = null;
        if (null != father)
        {
            progressBar = father.GetComponent<Scrollbar>();
        }
        if (null == progressBar)
        {
            return;
        }
        if (_RScroll.size <= progressBar.size)
        {
            _RScroll.size = progressBar.size;
        }
        else
        {
            _RScroll.size -= Time.deltaTime * 0.13f;
        }
    }

    /*#region 测试代码
        private float timer;       //距离多长时间刷新列表
        private float useTime = 0;
        int xt = 100;
        void FixedUpdate()
        {
            if (timer > 0 && useTime < Time.time)
            {
                useTime = Time.time + 1;
                timer -= 1;
                xt =xt- 10;
                UpdateHealthBar(xt, 100, false);
                SetHealthValueText(xt, 100, false);
            }
        }
    #endregion*/

}