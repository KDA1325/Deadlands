using BackEnd;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public string token = "";
    private string tableName = "USER_DATA";

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

        //GetSaveFile(); //LoadSaveFile
    }

    // 세이브 파일을 0으로 밀어버림 
    public void InitSaveFile()
    {
        Debug.LogWarning("데이터를 생성합니다. : " + tableName);

        string path = $"{Application.persistentDataPath}/SaveFile.csv";

        StreamWriter sw = File.CreateText(path);

        for (int i = 0; i < Enum.GetNames(typeof(Define.SaveFile)).Length; ++i)
        {
            sw.WriteLine($"{i},0");
        }

        sw.Close();

        Param param = new Param()
        {
            { "ATTUNLOCK", 0 },
            { "DEFUNLOCK", 0 },
            { "UTILUNLOCK", 0 },
            { "GOLD", 0 },
            { "PLAYERATTDAMAGE", 0 },
            { "PLAYERATTSPEED", 0 },
            { "PLAYERATTRANGE", 0 },
            { "PLAYERCRIPERCENT", 0 },
            { "PLAYERCRIDAMAGE", 0 },
            { "MULTISHOTPERCENT", 0 },
            { "MULTISHOTCOUNT", 0 },
            { "DUALSHOTPERCENT", 0 },
            { "DUALSHOTCOUNT", 0 },
            { "PLAYERHP", 0 },
            { "PLAYERHPRECOVER", 0 },
            { "PLAYERVAMPIRPERCENT", 0 },
            { "PLAYERREFLECTPERCENT", 0 },
            { "PLAYERENVASIONPERCENT", 0 },
            { "PLAYERPUSHPERCENT", 0 },
            { "PLAYERRESURRECTIONPERCENT", 0 },
            { "COINOBTAINPERCENT", 0 },
            { "COINOBTAINRATIO", 0 },
            { "EXPOBTAINRATIO", 0 },
            { "PLAYERSPEEDUP", 0 },
            { "COININTERESTPERCENT", 0 },
            { "CLEARSTAGE", 0 },
            { "MAXSTAGE1", 0 },
            { "MAXSTAGE2", 0 },
            { "MAXSTAGE3", 0 },
            { "MAXSTAGE4", 0 },
            { "MAXSTAGE5", 0 },
            { "MAXSTAGE6", 0 }
        };

        Managers.Data.GameDataInsert("USER_DATA", param);

        GetSaveFile();
    }

    // 로그인 데이터 파일 생성 및 로드
    public string LoadLoginData()
    {
        string Path = $"{Application.persistentDataPath}/loginDataName.csv";
        List<string[]> loginData = new List<string[]>();

        // 저장 파일이 없을 경우, 저장 파일을 생성
        if (!File.Exists(Path + ".csv"))
        {
            try
            {
                File.Create(Path + ".csv");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error creating CSV file: " + e.Message);
            }
            Debug.Log("저장 파일을 생성했습니다.");
            return Define.nullToken;
        }

        loginData = ReadCSV(Path);

        if (loginData.Count != 0)
        {
            if (loginData[0][0].Contains("Google"))
            {
                Debug.Log("Save Data Include Google Token");
                LoadServerDataToSaveFile();
                return Define.googleToken;
            }
            else if (loginData[0][0].Contains("Apple"))
            {
                Debug.Log("Save Data Include Apple Token");
                LoadServerDataToSaveFile();
                return Define.appleToken;
            }
        }

        Debug.Log("Save Data is Null");
        return Define.nullToken;
    }

    // 세이브(유저 데이터) 파일 생성 및 로드(LoadSaveData+CreateCSVFile)
    public void GetSaveFile()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData(tableName, new Where());
        string Path = $"{Application.persistentDataPath}/SaveFile.csv";
        List<float> stats = new List<float>();
        StreamReader data = null;
        if (bro.IsSuccess())
        {
            try
            {
                Debug.Log("게임 정보 조회에 성공했습니다. : " + tableName + bro);

                data = new StreamReader(Path);
            }
            catch
            {
                InitSaveFile();

                data = new StreamReader(Path);
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
        }
        else
        {
            Debug.Log("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }

    // 서버 데이터로 세이브 파일 덮어쓰기
    public void LoadServerDataToSaveFile()
    {
        BackendReturnObject bro = Backend.GameData.GetMyData(tableName, new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("서버 데이터 조회에 성공했습니다. : " + tableName + bro);

            List<float> stats = new List<float>();

            var rows = bro.GetReturnValuetoJSON()["rows"];

            if (rows.Count > 0)
            {
                var row = rows[0];
                stats.Add(ParseFloat(row["ATTUNLOCK"].ToString(), "ATTUNLOCK"));
                stats.Add(ParseFloat(row["DEFUNLOCK"].ToString(), "DEFUNLOCK"));
                stats.Add(ParseFloat(row["UTILUNLOCK"].ToString(), "UTILUNLOCK"));
                stats.Add(ParseFloat(row["GOLD"].ToString(), "GOLD"));
                stats.Add(ParseFloat(row["PLAYERATTDAMAGE"].ToString(), "PLAYERATTDAMAGE"));
                stats.Add(ParseFloat(row["PLAYERATTSPEED"].ToString(), "PLAYERATTSPEED"));
                stats.Add(ParseFloat(row["PLAYERATTRANGE"].ToString(), "PLAYERATTRANGE"));
                stats.Add(ParseFloat(row["PLAYERCRIPERCENT"].ToString(), "PLAYERCRIPERCENT"));
                stats.Add(ParseFloat(row["PLAYERCRIDAMAGE"].ToString(), "PLAYERCRIDAMAGE"));
                stats.Add(ParseFloat(row["MULTISHOTPERCENT"].ToString(), "MULTISHOTPERCENT"));
                stats.Add(ParseFloat(row["MULTISHOTCOUNT"].ToString(), "MULTISHOTCOUNT"));
                stats.Add(ParseFloat(row["DUALSHOTPERCENT"].ToString(), "DUALSHOTPERCENT"));
                stats.Add(ParseFloat(row["DUALSHOTCOUNT"].ToString(), "DUALSHOTCOUNT"));
                stats.Add(ParseFloat(row["PLAYERHP"].ToString(), "PLAYERHP"));
                stats.Add(ParseFloat(row["PLAYERHPRECOVER"].ToString(), "PLAYERHPRECOVER"));
                stats.Add(ParseFloat(row["PLAYERVAMPIRPERCENT"].ToString(), "PLAYERVAMPIRPERCENT"));
                stats.Add(ParseFloat(row["PLAYERREFLECTPERCENT"].ToString(), "PLAYERREFLECTPERCENT"));
                stats.Add(ParseFloat(row["PLAYERENVASIONPERCENT"].ToString(), "PLAYERENVASIONPERCENT"));
                stats.Add(ParseFloat(row["PLAYERPUSHPERCENT"].ToString(), "PLAYERPUSHPERCENT"));
                stats.Add(ParseFloat(row["PLAYERRESURRECTIONPERCENT"].ToString(), "PLAYERRESURRECTIONPERCENT"));
                stats.Add(ParseFloat(row["COINOBTAINPERCENT"].ToString(), "COINOBTAINPERCENT"));
                stats.Add(ParseFloat(row["COINOBTAINRATIO"].ToString(), "COINOBTAINRATIO"));
                stats.Add(ParseFloat(row["EXPOBTAINRATIO"].ToString(), "EXPOBTAINRATIO"));
                stats.Add(ParseFloat(row["PLAYERSPEEDUP"].ToString(), "PLAYERSPEEDUP"));
                stats.Add(ParseFloat(row["COININTERESTPERCENT"].ToString(), "COININTERESTPERCENT"));
                stats.Add(ParseFloat(row["CLEARSTAGE"].ToString(), "CLEARSTAGE"));
                stats.Add(ParseFloat(row["MAXSTAGE1"].ToString(), "MAXSTAGE1"));
                stats.Add(ParseFloat(row["MAXSTAGE2"].ToString(), "MAXSTAGE2"));
                stats.Add(ParseFloat(row["MAXSTAGE3"].ToString(), "MAXSTAGE3"));
                stats.Add(ParseFloat(row["MAXSTAGE4"].ToString(), "MAXSTAGE4"));
                stats.Add(ParseFloat(row["MAXSTAGE5"].ToString(), "MAXSTAGE5"));
                stats.Add(ParseFloat(row["MAXSTAGE6"].ToString(), "MAXSTAGE6"));
            }

            _stat = stats;
            SetSaveFile();
        }
        else
        {
            Debug.Log("서버 데이터 조회에 실패했습니다. : " + bro);
        }
    }

    private float ParseFloat(string row, string key)
    {
        try
        {
            return float.Parse(row);
        }
        catch 
        {   
            // 서버 데이터가 NULL인 경우 기본값 0 사용
            return 0f;
        }
    }
    // 세이브 파일(유저 데이터) 저장
    public void SetSaveFile()
    {
        string Path = $"{Application.persistentDataPath}/SaveFile.csv";
        StreamWriter sw = null;
        try
        {
            sw = File.CreateText(Path);
        }
        catch
        {
            InitSaveFile();
            sw = File.CreateText(Path);
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
        PushData();
    }

    // 데이터 파일 로드(ReadCSV)
    // 게임 스탯 파일 자체를 가져옴
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
    }

    // 게임 스탯 파일을 가공해서 가져옴
    public float GetDateFileStat(Define.OutGameStat _type)
    {
        float baseStat = Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)_type][0];

        for (int i = 0; i < (int)Managers.Data.Stat[(int)_type]; ++i)
        {
            baseStat += Managers.Data.GetDataFile("OutGameFile/PlayerOutGameStat")[(int)_type][1];
        }

        return Mathf.Round(baseStat * 100f) / 100f;
    }

    public List<string[]> ReadCSV(string path)
    {
        path = $"{path}.csv";
        StreamReader csvFile = new StreamReader(path);

        if (csvFile == null)
            return null;

        List<string[]> data = new List<string[]>();
        string fileContent = csvFile.ReadToEnd();

        string[] lines = fileContent.Split(new char[] { '\n' });
        foreach (string line in lines)
        {
            string[] fields = line.Split(new char[] { ',' });

            List<string> component = new List<string>();
            foreach (string field in fields)
            {
                if (field == "\r" || field == "" || field == "\t" || field == " ") continue;

                component.Add(field);
            }

            if (component.Count <= 0) continue;

            data.Add(component.ToArray());
        }
        csvFile.Close();

        return data;
    }

    public void WriteLoginData()
    {
        string Path = $"{Application.persistentDataPath}/loginDataName.csv";

        using (StreamWriter sw = new StreamWriter(Path))
        {
            sw.Flush();
            sw.Write(token);
            sw.Close();
        }
    }

    // 게임정보 데이터 삽입
    public void GameDataInsert(string tableName, Param param)
    {
        var bro = Backend.GameData.Insert(tableName, param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 삽입에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    // 게임 정보 데이터 변경(업데이트)
    public void GameDataUpdate(string tableName, Param param)
    {
        var bro = Backend.GameData.Update(tableName, new Where(), param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 변경에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 변경에 실패했습니다. : " + bro);
        }
    }

    public void PushData()
    {
        Param param = new Param()
        {
            { "ATTUNLOCK", Managers.Data.Stat[(int)Define.SaveFile.AttUnlock] },
            { "DEFUNLOCK", Managers.Data.Stat[(int)Define.SaveFile.DefUnlock] },
            { "UTILUNLOCK", Managers.Data.Stat[(int)Define.SaveFile.UtilUnlock] },
            { "GOLD", Managers.Data.Stat[(int)Define.SaveFile.Gold] },
            { "PLAYERATTDAMAGE", Managers.Data.Stat[(int)Define.SaveFile.PlayerAttDamage] },
            { "PLAYERATTSPEED", Managers.Data.Stat[(int)Define.SaveFile.PlayerAttSpeed] },
            { "PLAYERATTRANGE", Managers.Data.Stat[(int)Define.SaveFile.PlayerAttRange] },
            { "PLAYERCRIPERCENT", Managers.Data.Stat[(int)Define.SaveFile.PlayerCriPercent] },
            { "PLAYERCRIDAMAGE", Managers.Data.Stat[(int)Define.SaveFile.PlayerCriDamage] },
            { "MULTISHOTPERCENT", Managers.Data.Stat[(int)Define.SaveFile.MultiShotPercent] },
            { "MULTISHOTCOUNT", Managers.Data.Stat[(int)Define.SaveFile.MultiShotCount] },
            { "DUALSHOTPERCENT", Managers.Data.Stat[(int)Define.SaveFile.DualShotPercent] },
            { "DUALSHOTCOUNT", Managers.Data.Stat[(int)Define.SaveFile.DualShotCount] },
            { "PLAYERHP", Managers.Data.Stat[(int)Define.SaveFile.PlayerHp] },
            { "PLAYERHPRECOVER", Managers.Data.Stat[(int)Define.SaveFile.PlayerHpRecover] },
            { "PLAYERVAMPIRPERCENT", Managers.Data.Stat[(int)Define.SaveFile.PlayerVampirPercent] },
            { "PLAYERREFLECTPERCENT", Managers.Data.Stat[(int)Define.SaveFile.PlayerReflectPercent] },
            { "PLAYERENVASIONPERCENT", Managers.Data.Stat[(int)Define.SaveFile.PlayerEnvasionPercent] },
            { "PLAYERPUSHPERCENT", Managers.Data.Stat[(int)Define.SaveFile.PlayerPushPercent] },
            { "PLAYERRESURRECTIONPERCENT", Managers.Data.Stat[(int)Define.SaveFile.PlayerResurrectionPercent] },
            { "COINOBTAINPERCENT", Managers.Data.Stat[(int)Define.SaveFile.CoinObtainPercent] },
            { "COINOBTAINRATIO", Managers.Data.Stat[(int)Define.SaveFile.CoinObtainRatio] },
            { "EXPOBTAINRATIO", Managers.Data.Stat[(int)Define.SaveFile.ExpObtainRatio] },
            { "PLAYERSPEEDUP", Managers.Data.Stat[(int)Define.SaveFile.PlaySpeedUp] },
            { "COININTERESTPERCENT", Managers.Data.Stat[(int)Define.SaveFile.CoinInterestPercent] },
            { "CLEARSTAGE", Managers.Data.Stat[(int)Define.SaveFile.ClearStage] },
            { "MAXSTAGE1", Managers.Data.Stat[(int)Define.SaveFile.MaxStage1] },
            { "MAXSTAGE2", Managers.Data.Stat[(int)Define.SaveFile.MaxStage2] },
            { "MAXSTAGE3", Managers.Data.Stat[(int)Define.SaveFile.MaxStage3] },
            { "MAXSTAGE4", Managers.Data.Stat[(int)Define.SaveFile.MaxStage4] },
            { "MAXSTAGE5", Managers.Data.Stat[(int)Define.SaveFile.MaxStage5] },
            { "MAXSTAGE6", Managers.Data.Stat[(int)Define.SaveFile.MaxStage6] }
        };

        GameDataUpdate("USER_DATA", param);
    }

    public void Clear()
    {
        SetSaveFile();
    }
}