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

    [SerializeField] private List<Level> levels;
    public int currentLevel = 1;

    public void ChangeLevel()
    {
        levels[currentLevel].Hat.SetActive(false);
        levels[currentLevel].Head.SetActive(false);
        levels[currentLevel].Body.SetActive(false);

        currentLevel++;

        levels[currentLevel].Hat.SetActive(true);
        levels[currentLevel].Head.SetActive(true);
        levels[currentLevel].Body.SetActive(true);
    }
}
