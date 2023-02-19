using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStats : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public EmployeeLevel levelData;
        public GameObject Hat;
        public GameObject Head;
        public GameObject Body;
    }

    public List<Level> levels;
    public int CurrentLevelNumber = 0;
    public Level CurrentLevel;

    private void Start()
    {
        CurrentLevel = levels[CurrentLevelNumber];

        CurrentLevel.Hat.SetActive(true);
        CurrentLevel.Head.SetActive(true);
        CurrentLevel.Body.SetActive(true);
    }

    public void ChangeLevel()
    {
        CurrentLevel.Hat.SetActive(false);
        CurrentLevel.Head.SetActive(false);
        CurrentLevel.Body.SetActive(false);

        CurrentLevelNumber++;
        CurrentLevel = levels[CurrentLevelNumber];

        CurrentLevel.Hat.SetActive(true);
        CurrentLevel.Head.SetActive(true);
        CurrentLevel.Body.SetActive(true);
    }
}
