/*****************************************

    文件：DataClass.cs
    作者：张程瑞
    邮箱：296529530@qq.com
    日期：2022/7/24 21:59:28
    功能：用来保存和操作数据的类

******************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class DataClass
{
    private static int sideLength = 4;


    private static Data[,] _datas = new Data[sideLength, sideLength];
    private static GameObject[,] _gameObgDatas = new GameObject[sideLength, sideLength];

    /// <summary>
    /// 数据更改的事件
    /// </summary>
    public static event DataChange OnDataChange = null;

    public delegate void DataChange(Data[,] data);

    /// <summary>
    /// 获取游戏对象
    /// </summary>
    /// <param name="i">行</param>
    /// <param name="j">列</param>
    /// <returns></returns>
    public static GameObject ToObtainGameObg(int i, int j)
    {
        return _gameObgDatas[i, j];
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public static void InitializeData()
    {
        OnDataChange += RandomlyGenerat;
        OnDataChange += RefreshInterface;
        GameObject gCom = new GameObject();

        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                _datas[i, j] = new Data();

                _gameObgDatas[i, j] = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Square"));
                _gameObgDatas[i, j].transform.position = new Vector3(j * 2.5f, -i * 2.5f, 0);
                _gameObgDatas[i, j].transform.SetParent(gCom.transform);
            }
        }

        int x1 = Random.Range(0, sideLength);
        int y1 = Random.Range(0, sideLength);
        int x2 = Random.Range(0, sideLength);
        int y2 = Random.Range(0, sideLength);
        while (x1 == x2 && y1 == y2)
        {
            x2 = Random.Range(0, sideLength);
            y2 = Random.Range(0, sideLength);
        }

        _datas[x1, y1].value = Random.Range(1, 3) == 1 ? 2 : 4;
        _datas[x2, y2].value = Random.Range(1, 3) == 1 ? 2 : 4;

        if (OnDataChange != null) OnDataChange(_datas);
    }

    /// <summary>
    /// 按下 下方向键
    /// </summary>
    public static void PressDown()
    {
        Data[,] lowData = new Data[sideLength, sideLength];
        //将_datas数组中的数据复制到lowData数组中
        for (int i = 0; i < _datas.GetLength(0); i++)
        {
            for (int j = 0; j < _datas.GetLength(1); j++)
            {
                lowData[i, j] = new Data();
                lowData[i, j].value = _datas[i, j].value;
            }
        }

        for (int i = _datas.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = _datas.GetLength(1) - 1; j >= 0; j--)
            {
                if (_datas[i, j].value != 0)
                {
                    int x = i;
                    int y = j;
                    while (x < _datas.GetLength(0) - 1)
                    {
                        x++;
                        if (_datas[x, y].value == 0)
                        {
                            _datas[x, y].value = _datas[x - 1, y].value;
                            _datas[x - 1, y].value = 0;
                        }
                        else if (_datas[x, y].value == _datas[x - 1, y].value)
                        {
                            _datas[x, y].value *= 2;
                            _datas[x - 1, y].value = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        //判断Data与lowData是否相等
        bool isEqual = true;
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                if (lowData[i, j].value != _datas[i, j].value)
                {
                    isEqual = false;
                    break;
                }
            }
        }

        if (!isEqual)
        {
            if (OnDataChange != null) OnDataChange(_datas);
        }
    }

    /// <summary>
    /// 按下 上方向键
    /// </summary>
    public static void PressUp()
    {
        Data[,] lowData = new Data[sideLength, sideLength];
        //将_datas数组中的数据复制到lowData数组中
        for (int i = 0; i < _datas.GetLength(0); i++)
        {
            for (int j = 0; j < _datas.GetLength(1); j++)
            {
                lowData[i, j] = new Data();
                lowData[i, j].value = _datas[i, j].value;
            }
        }

        for (int i = 0; i < _datas.GetLength(0); i++)
        {
            for (int j = 0; j < _datas.GetLength(1); j++)
            {
                if (_datas[i, j].value != 0)
                {
                    int x = i;
                    int y = j;
                    while (x > 0)
                    {
                        x--;
                        if (_datas[x, y].value == 0)
                        {
                            _datas[x, y].value = _datas[x + 1, y].value;
                            _datas[x + 1, y].value = 0;
                        }
                        else if (_datas[x, y].value == _datas[x + 1, y].value)
                        {
                            _datas[x, y].value *= 2;
                            _datas[x + 1, y].value = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        //判断Data与lowData是否相等
        bool isEqual = true;
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                if (lowData[i, j].value != _datas[i, j].value)
                {
                    isEqual = false;
                    break;
                }
            }
        }

        if (!isEqual)
        {
            if (OnDataChange != null) OnDataChange(_datas);
        }
    }

    /// <summary>
    /// 按下 左方向键
    /// </summary>
    public static void PressLeft()
    {
        Data[,] lowData = new Data[sideLength, sideLength];
        //将_datas数组中的数据复制到lowData数组中
        for (int i = 0; i < _datas.GetLength(0); i++)
        {
            for (int j = 0; j < _datas.GetLength(1); j++)
            {
                lowData[i, j] = new Data();
                lowData[i, j].value = _datas[i, j].value;
            }
        }

        for (int i = 0; i < _datas.GetLength(0); i++)
        {
            for (int j = 0; j < _datas.GetLength(1); j++)
            {
                if (_datas[i, j].value != 0)
                {
                    int x = i;
                    int y = j;
                    while (y > 0)
                    {
                        y--;
                        if (_datas[x, y].value == 0)
                        {
                            _datas[x, y].value = _datas[x, y + 1].value;
                            _datas[x, y + 1].value = 0;
                        }
                        else if (_datas[x, y].value == _datas[x, y + 1].value)
                        {
                            _datas[x, y].value *= 2;
                            _datas[x, y + 1].value = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        //判断Data与lowData是否相等
        bool isEqual = true;
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                if (lowData[i, j].value != _datas[i, j].value)
                {
                    isEqual = false;
                    break;
                }
            }
        }

        if (!isEqual)
        {
            if (OnDataChange != null) OnDataChange(_datas);
        }
    }

    /// <summary>
    /// 按下 右方向键
    /// </summary>
    public static void PressRight()
    {
        Data[,] lowData = new Data[sideLength, sideLength];
        //将_datas数组中的数据复制到lowData数组中
        for (int i = 0; i < _datas.GetLength(0); i++)
        {
            for (int j = 0; j < _datas.GetLength(1); j++)
            {
                lowData[i, j] = new Data();
                lowData[i, j].value = _datas[i, j].value;
            }
        }

        for (int i = _datas.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = _datas.GetLength(1) - 1; j >= 0; j--)
            {
                if (_datas[i, j].value != 0)
                {
                    int x = i;
                    int y = j;
                    while (y < _datas.GetLength(1) - 1)
                    {
                        y++;
                        if (_datas[x, y].value == 0)
                        {
                            _datas[x, y].value = _datas[x, y - 1].value;
                            _datas[x, y - 1].value = 0;
                        }
                        else if (_datas[x, y].value == _datas[x, y - 1].value)
                        {
                            _datas[x, y].value *= 2;
                            _datas[x, y - 1].value = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        //判断Data与lowData是否相等
        bool isEqual = true;
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                if (lowData[i, j].value != _datas[i, j].value)
                {
                    isEqual = false;
                    break;
                }
            }
        }

        if (!isEqual)
        {
            if (OnDataChange != null) OnDataChange(_datas);
        }
    }

    /// <summary>
    /// 随机生成
    /// </summary>
    /// <param name="data"></param>
    private static void RandomlyGenerat(Data[,] data)
    {
        //判断Data中的数据全部为为非空
        bool isAllNotNull = true;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLongLength(1); j++)
            {
                if (data[i, j].value == 0)
                {
                    isAllNotNull = false;
                    break;
                }
            }
        }

        if (!isAllNotNull)
        {
            //随机在Data中数据为0的位置生成一个2或者4
            int x = Random.Range(0, sideLength);
            int y = Random.Range(0, sideLength);
            while (data[x, y] != null && data[x, y].value != 0)
            {
                x = Random.Range(0, sideLength);
                y = Random.Range(0, sideLength);
            }

            data[x, y].value = Random.Range(0, 2) == 0 ? 2 : 4;
        }
        else
        {
            //判断数据的上下左右是否有相同的数据 若没有则输出游戏结束
        }
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private static void RefreshInterface(Data[,] data)
    {
        //遍历行数
        for (int i = 0; i < data.GetLength(0); i++)
        {
            //遍历列数
            for (int j = 0; j < data.GetLength(1); j++)
            {
                var g = DataClass.ToObtainGameObg(i, j);

                g.transform.Find("Canvas").Find("ValueText").GetComponent<TextMeshProUGUI>().text =
                    data[i, j].value.ToString();
            }
        }
    }

    public class Data
    {
        /// <summary>
        /// 当前方块的值
        /// </summary>
        public int value = 0;
    }
}