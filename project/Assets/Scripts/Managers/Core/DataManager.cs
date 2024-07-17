using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

public class DataManager
{
    List<float> _stat;
    Dictionary<string, Dictionary<int, List<float>>> _info;

    public List<float> Stat
    {
        get
        {
            return _stat;
        }
        set
        {
            _stat = value;
        }
    }

    public void Init()
    {
        _info = new Dictionary<string, Dictionary<int, List<float>>>();

        GetSaveFile();
    }


    public void InitSaveFile()
    {
        string path = $"{Application.persistentDataPath}/SaveFile.csv";

        StreamWriter sw = File.CreateText(path);

        for (int i = 0; i < Enum.GetNames(typeof(Define.SaveFile)).Length; ++i)
        {
            sw.WriteLine($"{i},0");
        }

        sw.Close();

        GetSaveFile();
    } // 세이브 파일을 0으로 밀어버림
    public void GetSaveFile()
    {
        string csvPath = $"{Application.persistentDataPath}/SaveFile.csv";
        List<float> stats = new List<float>();
        StreamReader data = null;
        try
        {
            data = new StreamReader(csvPath);
        }
        catch
        {
            InitSaveFile();
            data = new StreamReader(csvPath);
        }

        string[] texts = data.ReadToEnd().Split(new char[] { '\n' });

        int i = 0;
        for (int j = 0; j < texts.Length; ++j)
        {
            if (texts[j] == "")
                continue;

            string[] level = texts[j].Split(new char[] { ',' });

            stats.Add(float.Parse(level[1]));

            ++i;
        }

        for (; i < (int)Define.SaveFile.End; ++i)
        {
            stats.Add(0);
        }

        _stat = stats;
        data.Close();
    } // 세이브 파일을 로드함
    public void SetSaveFile()
    {
        string csvPath = $"{Application.persistentDataPath}/SaveFile.csv";
        StreamWriter sw = null;
        try
        {
            sw = File.CreateText(csvPath);
        }
        catch
        {
            InitSaveFile();
            sw = File.CreateText(csvPath);
        }

        if (_stat.Count < (int)Define.SaveFile.End)
        {
            InitSaveFile();
        }

        for (int j = 0; j < _stat.Count; ++j)
        {
            string _level = $"{j}";

            _level += $",{_stat[j]}";

            sw.Write($"\n{_level}");
        }
        sw.Close();
    } // 세이브 파일을 저장함


    public Dictionary<int, List<float>> GetDataFile(string path)
    {
        Dictionary<int, List<float>> dict = null;

        if (_info.TryGetValue(path, out dict) == false)
        {
            TextAsset data = Managers.Resource.Load<TextAsset>($"Data/{path}");
            dict = new Dictionary<int, List<float>>();
            string[] texts = data.text.Split(new char[] { '\n' });

            for (int i = 1; i < texts.Length; i++)
            {
                if (texts[i] == "")
                    continue;

                string[] _level = texts[i].Split(new char[] { ',' });
                List<float> _stat = new List<float>();

                for (int j = 1; j < _level.Length; ++j)
                {
                    if (_level[j] == "" || _level[j] == "\r")
                        continue;

                    _stat.Add(float.Parse(_level[j]));
                }

                if (_stat.Count <= 0)
                    continue;

                dict.Add(int.Parse(_level[0]), _stat);
            }

            _info.Add(path, dict);
        }

        return dict;
    } // 게임 스탯 파일 자체를 가져옴
    public float GetDateFileStat(Define.OutGameStat _type)
    {
        float baseStat = Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)_type][0];

        for (int i = 0; i < (int)Managers.Data.Stat[(int)_type]; ++i)
        {
            baseStat += Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)_type][1];
        }

        return Mathf.Round(baseStat * 100f) / 100f;
    } // 게임 스탯 파일을 가공해서 가져옴 


    public void Clear()
    {
        SetSaveFile();
    }
}