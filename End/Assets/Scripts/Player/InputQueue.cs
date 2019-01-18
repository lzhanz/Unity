using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class InputQueue// : MonoBehaviour
{

    public float Max_time = 0.26f;
    public int skillIndex;
    public int count;
    [HideInInspector] public float interval = -1;

    string temp;

    [HideInInspector] public List<string> instructsList = new List<string>();

    public InputQueue()
    {
        Start();
    }


    public void Start()
    {
        instructsList.Add("LeftArrowRightArrowX");
        instructsList.Add("RightArrowLeftArrowX");
        instructsList.Add("DownArrowRightArrowX");
        instructsList.Add("DownArrowLeftArrowX");
    }


    public int GetIndex()
    {
        if (Input.anyKeyDown)//检测到按键
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))//遍历所有按键的enum值.
            {
                if (Input.GetKeyDown(keyCode))  //enum值对应的按键是否按下。
                {
                    temp += keyCode.ToString();
                    count = match(1);
                    if (count > -2)
                        return count;
                    interval = 0;
                }
            }
        }
        if (interval >= 0)
            interval += Time.deltaTime;

        if (interval > Max_time)
        {
            interval = -1;
            skillIndex= match();
            return skillIndex;
        }
        skillIndex = -1;
        return skillIndex; ;
    }

    int match(int b = 0)
    {
        int index;
        try
        {
            index = instructsList.FindIndex(a => a == temp.ToString());
            if (b == 1 && index == -1)
            {
                index = instructsList.FindIndex(a => cutout(a, temp.ToString()));
                if (index != -1)
                    return -2;
            }
            temp = "";
            return index;
        }
        catch
        {
            return -1;
        }

    }

    bool cutout(string a, string b)
    {
        //Debug.Log(a);
        //Debug.Log(b);
        for (int i = 0; i < b.Length; ++i)
        {
            if (a[i] != b[i])
                return false;
        }
        return true;
    }
}



