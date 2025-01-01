using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public AudioSource buttonSound; // ���ķ�
    public Button myButton; // UI���s

    void Start()
    {
        // �T�O���s�M���ķ��w�g���t
        if (myButton != null && buttonSound != null)
        {
            // �����s�K�[�I���ƥ��ť��
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    // ���s�I���ƥ�B�z��k
    void OnButtonClick()
    {
        // ���񭵮�
        buttonSound.Play();

        // �K�[��L�\��A�ҦpĲ�o�}��
        // StartFire(true); // ���]�z���o�˪���k
    }
}
