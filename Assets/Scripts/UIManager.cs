using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    //各種 Panel 以及 UI
    public GameObject mainMenuPanel;
    public GameObject pausePanel;
    public GameObject hud1PPanel;
    public GameObject hud2PPanel;
    public GameObject abilityChoosePanel;
    public TextMeshProUGUI timerText;
    public BuffManager buffManager;
    public List<Image> bufferSelectionImage;
    public List<TextMeshProUGUI> bufferSelectionTitle;
    public List<TextMeshProUGUI> bufferSelectionDescription;
    public List<Slider> powervalueSliders = new List<Slider>();
    public RectTransform selection1P;
    public RectTransform selection2P;

    //確認是否為單人遊戲
    private bool isSinglePlayerGame = true;

    //確認 1P/2P 以及所有玩家是否已經選完能力
    public bool is1PReadyToGetAbility = true;
    public bool is2PReadyToGetAbility = true;
    public bool isAllReadyToGetAbility = true;

    //遊戲是否開始
    private bool isTimerStart = false;

    //玩家當前選擇的能力
    public int currentAbility1PSelect = 0;
    public int currentAbility2PSelect = 1;

    //累積值
    private int powerValue = 0;
    public int powerThreshold = 100;

    //計時
    public float timerThrshold = 20f;
    private float timer = 0f;

    public Slider powerSlide;

    void Start()
    {
        ShowMainMenu();
    }


    //開始選單
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
        hud1PPanel.SetActive(false);
        hud2PPanel.SetActive(false);
        abilityChoosePanel.SetActive(false);
        Time.timeScale = 0f; // 停止遊戲
    }

    //開始 1P 遊戲
    public void Start1PGame()
    {
        mainMenuPanel.SetActive(false);
        hud1PPanel.SetActive(true);
        hud2PPanel.SetActive(false);
        powerSlide = powervalueSliders[0];
        powerSlide.value = powerValue;
        powerSlide.maxValue = powerThreshold;
        isSinglePlayerGame = true;
        isTimerStart = true;
        timer = timerThrshold;
        Time.timeScale = 1f;
    }

    //開始 2P 遊戲
    public void Start2PGame()
    {
        mainMenuPanel.SetActive(false);
        hud1PPanel.SetActive(false);
        hud2PPanel.SetActive(true);
        powerSlide = powervalueSliders[1];
        isSinglePlayerGame = false;
        isTimerStart = true;
        timer = timerThrshold;
        Time.timeScale = 1f;
    }

    //離開遊戲
    public void ExitGame()
    {
        Application.Quit();
    }

    //觸發遊戲結算畫面
    public void ToggleEnd()
    {
        isTimerStart = false;
        if (isSinglePlayerGame == true)
        {
            hud1PPanel.SetActive(false);
        }
        else
        {
            hud2PPanel.SetActive(false);
        }
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    //重啟遊戲
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        if (isSinglePlayerGame == true)
        {
            hud1PPanel.SetActive(true);
        }
        else
        {
            hud2PPanel.SetActive(true);
        }
        Time.timeScale = 1f;
        isTimerStart = true;
        timer = timerThrshold;
        DisplayTimerText();
    }

    //開啟能力選單畫面
    public void StartChoosingAbility()
    {
        List<BuffAttribute> bufferSelection = new List<BuffAttribute>();
        bufferSelection = buffManager.GetThreeRandomAttribute();
        for (int i = 0; i < bufferSelectionImage.Count; i++)
        {
            bufferSelectionImage[i].sprite = bufferSelection[i].icon;
            bufferSelectionTitle[i].text = bufferSelection[i].title;
            bufferSelectionDescription[i].text = $"Lift:{bufferSelection[i].attributes.weightLifting} <br> Speed:{bufferSelection[i].attributes.moveSpeed}";
        }

        if (isSinglePlayerGame == true)
        {
            hud1PPanel.SetActive(false);
        }
        else
        {
            hud2PPanel.SetActive(false);
        }
        selection1P.anchoredPosition = new Vector2(bufferSelectionImage[currentAbility1PSelect].GetComponent<RectTransform>().anchoredPosition.x, selection1P.anchoredPosition.y);
        selection2P.anchoredPosition = new Vector2(bufferSelectionImage[currentAbility2PSelect].GetComponent<RectTransform>().anchoredPosition.x, selection2P.anchoredPosition.y);
        abilityChoosePanel.SetActive(true);
        is1PReadyToGetAbility = false;
        is2PReadyToGetAbility = false;
        isAllReadyToGetAbility = false;
        Time.timeScale = 0f;
    }

    //關閉能力選單畫面
    public void StopChoosingAbility()
    {
        if (isSinglePlayerGame == true)
        {
            hud1PPanel.SetActive(true);
        }
        else
        {
            hud2PPanel.SetActive(true);
        }
        abilityChoosePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    //累積值操作
    public void PowerValueOperation(int value)
    {
        powerValue = powerValue + value;
        if (powerValue >= powerThreshold)
        {
            StartChoosingAbility();
            powerValue = 0;

        }
        powerSlide.value = powerValue;
    }

    //計時器顯示
    void DisplayTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void FixedUpdate()
    {
        //計時
        if (timer > 0 && isTimerStart == true)
        {
            timer = timer - Time.fixedDeltaTime * Time.timeScale;
            DisplayTimerText();
        }
        else if (isTimerStart == true)
        {
            ToggleEnd();
        }
    }
    void Update()
    {

        //能力選單選擇邏輯
        if (isAllReadyToGetAbility == false)
        {
            if (isSinglePlayerGame == true)
            {
                is2PReadyToGetAbility = true;
                currentAbility2PSelect = 3;
                selection2P.gameObject.SetActive(false);
            }

            if (Keyboard.current[Key.A].wasPressedThisFrame && is1PReadyToGetAbility == false)
            {
                currentAbility1PSelect = currentAbility1PSelect - 1;
                if (currentAbility1PSelect < 0)
                {
                    currentAbility1PSelect = 2;
                }
                if (currentAbility1PSelect == currentAbility2PSelect)
                {
                    currentAbility1PSelect = currentAbility1PSelect - 1;
                }
                if (currentAbility1PSelect < 0)
                {
                    currentAbility1PSelect = 2;
                }
                selection1P.anchoredPosition = new Vector2(bufferSelectionImage[currentAbility1PSelect].GetComponent<RectTransform>().anchoredPosition.x, selection1P.anchoredPosition.y);
            }

            if (Keyboard.current[Key.D].wasPressedThisFrame && is1PReadyToGetAbility == false)
            {
                currentAbility1PSelect = currentAbility1PSelect + 1;
                if (currentAbility1PSelect > 2)
                {
                    currentAbility1PSelect = 0;
                }
                if (currentAbility1PSelect == currentAbility2PSelect)
                {
                    currentAbility1PSelect = currentAbility1PSelect + 1;
                }
                if (currentAbility1PSelect > 2)
                {
                    currentAbility1PSelect = 0;
                }
                selection1P.anchoredPosition = new Vector2(bufferSelectionImage[currentAbility1PSelect].GetComponent<RectTransform>().anchoredPosition.x, selection1P.anchoredPosition.y);
            }

            if (Keyboard.current[Key.R].wasPressedThisFrame)
            {
                is1PReadyToGetAbility = !is1PReadyToGetAbility;
            }

            if (Keyboard.current[Key.J].wasPressedThisFrame && is2PReadyToGetAbility == false)
            {
                currentAbility2PSelect = currentAbility2PSelect - 1;
                if (currentAbility2PSelect < 0)
                {
                    currentAbility2PSelect = 2;
                }
                if (currentAbility2PSelect == currentAbility1PSelect)
                {
                    currentAbility2PSelect = currentAbility2PSelect - 1;
                }
                if (currentAbility2PSelect < 0)
                {
                    currentAbility2PSelect = 2;
                }
                selection2P.anchoredPosition = new Vector2(bufferSelectionImage[currentAbility2PSelect].GetComponent<RectTransform>().anchoredPosition.x, selection2P.anchoredPosition.y);
            }

            if (Keyboard.current[Key.L].wasPressedThisFrame && is2PReadyToGetAbility == false)
            {
                currentAbility2PSelect = currentAbility2PSelect + 1;
                if (currentAbility2PSelect > 2)
                {
                    currentAbility2PSelect = 0;
                }
                if (currentAbility2PSelect == currentAbility1PSelect)
                {
                    currentAbility2PSelect = currentAbility2PSelect + 1;
                }
                if (currentAbility2PSelect > 2)
                {
                    currentAbility2PSelect = 0;
                }
                selection2P.anchoredPosition = new Vector2(bufferSelectionImage[currentAbility2PSelect].GetComponent<RectTransform>().anchoredPosition.x, selection2P.anchoredPosition.y);
            }

            if (Keyboard.current[Key.P].wasPressedThisFrame)
            {
                is2PReadyToGetAbility = !is2PReadyToGetAbility;
            }

            if (is1PReadyToGetAbility == true && is2PReadyToGetAbility == true)
            {
                isAllReadyToGetAbility = true;
                StopChoosingAbility();

                if (is1PReadyToGetAbility)
                {
                    if (currentAbility1PSelect < 3 && currentAbility1PSelect >= 0)
                        GameState.Instance.OnP1BuffGet(bufferSelectionTitle[currentAbility1PSelect].text.Trim());
                }

                if (is2PReadyToGetAbility)
                {
                    if (currentAbility2PSelect < 3 && currentAbility2PSelect >= 0)
                        GameState.Instance.OnP2BuffGet(bufferSelectionTitle[currentAbility2PSelect].text.Trim());
                }
            }

        }
    }
}
