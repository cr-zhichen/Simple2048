/*****************************************

    文件：GameControl.cs
    作者：张程瑞
    邮箱：296529530@qq.com
    日期：2022/7/24 22:21:50
    功能：游戏控制器

******************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private void Start()
    {
        //初始化游戏数据
        DataClass.InitializeData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            DataClass.PressUp();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DataClass.PressLeft();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            DataClass.PressDown();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            DataClass.PressRight();
        }
    }
}