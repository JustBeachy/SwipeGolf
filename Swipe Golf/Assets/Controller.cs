﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;

public class Controller : MonoBehaviour
{
    public int swipes, cameraZoom;
    public static int Swipes, CameraZoom;
    public static bool Start=false;
    public static bool noSwipeZone = false;
    public Text swipesText;
    public bool[] HiddenCoins = new bool[180];
    public static Controller LoadIn = new Controller();
    string path;
    // Start is called before the first frame update
    void Awake()
    {
        path = Application.persistentDataPath + "/SaveFile.json";
        Swipes = swipes;
        CameraZoom = cameraZoom;
        Load();
        

        if(HiddenCoins[SceneManager.GetActiveScene().buildIndex]) //delete hidden coin if already found
        {
            GameObject.FindGameObjectWithTag("Coin").SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        swipesText.text = Swipes.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(bool foundCoin=false)
    {
        if (!HiddenCoins[SceneManager.GetActiveScene().buildIndex]) //if hidden coin wasnt already found
        {
            HiddenCoins[SceneManager.GetActiveScene().buildIndex] = foundCoin; //save hidden coin
        }
            
        
        
        Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Save()
    {
        Controller saveFile = gameObject.GetComponent<Controller>();

        string json = JsonUtility.ToJson(saveFile);
        print(json);
        File.WriteAllText(@path, json);
        //File.WriteAllText(@path, EncryptDecrypt(json, 1337)); //comment out for testing -encryption
    }

    public void Load()
    {
        if (File.Exists(@path))
        {
            string loadedString = File.ReadAllText(@path);
            //loadedString = EncryptDecrypt(loadedString, 1337); //comment out for testing -encryption
            JsonUtility.FromJsonOverwrite(loadedString, LoadIn);
            print(loadedString);
            HiddenCoins = LoadIn.HiddenCoins;
        }
    }

    public string EncryptDecrypt(string szPlainText, int szEncryptionKey) //encrypt save
    {
        StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
        StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
        char Textch;
        for (int iCount = 0; iCount < szPlainText.Length; iCount++)
        {
            Textch = szInputStringBuild[iCount];
            Textch = (char)(Textch ^ szEncryptionKey);
            szOutStringBuild.Append(Textch);
        }
        return szOutStringBuild.ToString();
    }
}
