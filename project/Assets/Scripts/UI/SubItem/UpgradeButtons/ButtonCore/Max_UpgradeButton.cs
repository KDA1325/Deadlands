using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max_UpgradeButton : Base_UpgradeButton
{
    protected int MaxLevel = 3;

    public bool isMax;

    enum GameObjects
    {
        LevelSortBox
    }

    GameObject _levelSort;

    public override void Init()
    {
        base.Init();
        
        Bind<GameObject>(typeof(GameObjects));
        _levelSort = Get<GameObject>((int)GameObjects.LevelSortBox);
    }

    private void OnEnable()
    {
        Transform[] _childs = _levelSort.GetComponentsInChildren<Transform>();
        for (int i = 1; i < _childs.Length; ++i)
        {
            Managers.Resource.Destroy(_childs[i].gameObject);
        }

        if (MaxLevel <= 3)
        {
            for (int i = 0; i < _level; ++i)
            {
                Managers.UI.MakeSubItem<LevelBox>(_levelSort.transform, $"InGameUpgradeButtons/LevelBox1");
            }
            for (int i = 0; i < MaxLevel - _level; ++i)
            {
                Managers.UI.MakeSubItem<LevelBox>(_levelSort.transform, $"InGameUpgradeButtons/LevelBox0");
            }
        }
        else
        {
            int lootLevel = _level / 3;
            int branchLevel = _level % 3;

            for (int i = 0; i < branchLevel; ++i)
            {
                Managers.UI.MakeSubItem<LevelBox>(_levelSort.transform, $"InGameUpgradeButtons/LevelBox{lootLevel + 1}");
            }
            for (int i = 0; i < 3 - branchLevel; ++i)
            {
                Managers.UI.MakeSubItem<LevelBox>(_levelSort.transform, $"InGameUpgradeButtons/LevelBox{lootLevel}");
            }
        }
    }

    public override void OnUpgrade()
    {
        base.OnUpgrade();

        if (_level == MaxLevel)
        {
            isMax = true;
        }
    }
}
