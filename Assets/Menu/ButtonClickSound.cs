using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public AudioSource buttonSound; // 音效源
    public Button myButton; // UI按鈕

    void Start()
    {
        // 確保按鈕和音效源已經分配
        if (myButton != null && buttonSound != null)
        {
            // 為按鈕添加點擊事件監聽器
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    // 按鈕點擊事件處理方法
    void OnButtonClick()
    {
        // 播放音效
        buttonSound.Play();

        // 添加其他功能，例如觸發開火
        // StartFire(true); // 假設您有這樣的方法
    }
}
