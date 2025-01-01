using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public GameObject[] pages;
    public int currentPageIndex = 0;

    public Button[] next;
    public Button[] back;

    public Button storageNext;
    public Button[] storages;

    public DatabaseManager database;

    public TextMeshProUGUI text;
    public TextMeshProUGUI status;

    public int currentStorage = -1;

    void Start()
    {
        UpdatePage();  // 初始化顯示第一頁
        
        foreach (Button token in next)
        {
            token.onClick.AddListener(NextPage);
        }

        foreach (Button token in back)
        {
            token.onClick.AddListener(PreviousPage);
        }

        for (int i = 0; i < storages.Length; i++)
        {
            int index = i; // 重要：捕獲當前迴圈的索引值
            storages[i].onClick.AddListener(() => ChooseThisStorage(index));
        }

        storageNext.enabled = false;

    }

    private void Update()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);  // 只顯示當前頁面
        }

        if(currentStorage >= 0 && currentStorage <= 2)
        {
            status.text = database.PlayerInfoOutPut();
        }

    }

    private void ChooseThisStorage(int index)
    {
        Debug.Log($"U choose the storage{index}");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(storages[index].gameObject);
        database.ReadSpecificData(index+1);
        text.text = database.PlayerInfoOutPut();
        storageNext.enabled = true;
    }



    private void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            UpdatePage();
        }
        else
        {
            SceneManager.LoadScene("Play"); // 載入遊戲場景
        }
    }

    // 切換到上一頁
    private void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePage();
        }
        else
        {
            SceneManager.LoadScene("MainMenu"); // 載入遊戲場景
        }
    }

    // 更新頁面顯示
    private void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);  // 只顯示當前頁面
        }
        currentStorage = 0;
        storageNext.enabled = false;
        text.text = "Select Your Storage";
    }
}

