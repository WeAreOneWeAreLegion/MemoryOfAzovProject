﻿using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager> {

    public enum TypeOfTag { Player, Enemy, Wall, Door, DoorTrigger, Bell, HittableObject, FakeWall, CollectableObject }
    public enum ButtonRequest { A, B, X, Y, RB, LB, RT, LT }

    #region Public Variables
    [Header("\t--Game Designers Variables--")]
    [Tooltip("Muestra los fps, en caso contrario los esconde automaticamente")]
    public bool showFPS;

    [Header("Materials Variables")]
    [Tooltip("Distancia extra para no hacer invisible los objetos cuando estan a la misma distancia frontal que el personaje principal")]
    [Range(0,20)] public float transparencyOffsetForward = 1f;
    [Tooltip("Distancia extra para no hacer invisible los objetos cuando estan a la misma distancia lateral que el personaje principal")]
    [Range(0,20)] public float transparencyOffsetLateral = 1f;
    [Tooltip("La cantidad de transparencia a tener los objetos que la camara esconde")]
    [Range(0,1)] public float objectsHidenByCameraTransparency = 0.2f;
    [Tooltip("La cantidad de transparencia a tener los muros que la camara esconde")]
    [Range(0,1)] public float wallsHidenByCameraTransparency = 0f;

    [Header("\t    --Own Script Variables--")]
    [Header("Player Variables")]
    [Tooltip("Referencia del player")]
    public PlayerController player;

    [Header("Main Light")]
    [Tooltip("Referencia de la luz direccional")]
    public Light directionalLight;

    [Header("Rooms Variables")]
    [Tooltip("Referencia a todas las habitacions")]
    public List<GameObject> roomsList = new List<GameObject>();
    [Tooltip("Referencia a todas las puertas")]
    public List<ConectionScript> doorsList = new List<ConectionScript>();
    [Tooltip("Referencia a todas las puertas")]
    public List<FakeWallScript> fakeWallsList = new List<FakeWallScript>();

    [Header("HUD Variables")]
    //Main Canvas
    [Tooltip("Referencia al canvas principal")]
    public Canvas mainCanvas;
    //References
    [Tooltip("Referencia al hud del player")]
    public PlayerHUD playerHUD;
    [Tooltip("Referencia al hud del enemigo")]
    public GameObject enemyHUDPrefab;
    [Tooltip("Referencia al hud de pausa")]
    public GameObject pausePanel;
    //Componens
    [Tooltip("Referencia al texto de vida")]
    public Text hpText;
    [Tooltip("Referencia a la sombra del texto de vida")]
    public Text hpTextShadow;
    [Tooltip("Referencia al texto de fps")]
    public Text fpsText;

    [Header("Gems Panel")]
    [Tooltip("Referencia to gems panel")]
    public RectTransform gemsPanel;
    [Tooltip("Referencia a los huevos del hud")]
    public List<GameObject> diamondEggMask = new List<GameObject>();
    [Tooltip("Posicion en el cual el panel de gemas esta escondido")]
    public float gemsPanelYHidden = 100;
    [Tooltip("Posicion en el cual el panel de gemas esta mostradose")]
    public float gemsPanelYShown = 0;
    [Tooltip("Tiempo en el cual el panel de gemas tardara en aparecer del todo")]
    public float gemsPanelTime = 1;
    [Tooltip("Tiempo en el cual el panel de gemas tardara en mostrar gema")]
    public float timeBeforeAddingGem = 0.4f;
    [Tooltip("Tiempo en el cual el panel de gemas tardara en desaparecer tras mostrar gema")]
    public float timeShowingGemsPanel = 1f;

    [Header("Health Panel")]
    [Tooltip("Referencia to health panel")]
    public RectTransform healthPanel;
    [Tooltip("Posicion en el cual el panel de vida esta escondido")]
    public float healthPanelYHidden = 100;
    [Tooltip("Posicion en el cual el panel de vida esta mostradose")]
    public float healthPanelYShown = 0;
    [Tooltip("Tiempo en el cual el panel de vida tardara en aparecer del todo")]
    public float healthPanelTime = 1;
    [Tooltip("Tiempo en el cual el panel de vida tardara en mostrar gema")]
    public float timeBeforeChangingHealth = 0.4f;
    [Tooltip("Tiempo en el cual el panel de vida tardara en desaparecer tras mostrar gema")]
    public float timeShowingHealthPanel = 1f;

    [Header("\t--Pause Menu Variables--")]
    [Tooltip("Menu variables to know what to show depending on the menu I am currently at")]
    public GameObject pauseMenuGO;
    public GameObject restartConfirmationPanel;
    public GameObject settingsPanel;
    public GameObject menuConfirmationPanel;
    public GameObject quitConfirmationPanel;
    [Tooltip("Button variables to know which button to return to after coming back to the pause menu. Used for the controller navigation through the menu")]
    public GameObject resumeButton;
    public GameObject restartButton;
    public GameObject settingsButton;
    public GameObject menuButton;
    public GameObject quitButton;
    [Tooltip("Button variables to know which button to start from after pressing a button. Used for the controller navigation through the menu")]
    public GameObject yesButtonRestartConfirmationPanel;
    public GameObject yesButtonMenuConfirmationPanel;
    public GameObject yesButtonQuitConfirmationPanel;
    public GameObject controllerToggle;
    [Tooltip("Bool variable to know if a confirmation panel is visible or not")]
    public bool confirmationPanelOpen = false;
    [Tooltip("Bool variable to know if a the player is using a controller or a keyboard")]
    public bool controllerToggleIsChecked = true;

    public EventSystem myEventSystem;

    [Header("\t--Game Over Variables--")]
    [Tooltip("Game Over variables, seen when the player dies")]
    public GameObject gameOverPanel;
    public GameObject gameOverRestartButton;

    [Header("\t--Victory Variables--")]
    [Tooltip("Game Over variables, seen when the player dies")]
    public GameObject victoryPanel;
    public GameObject victoryOKButton;
    public Text ghostHuntedText;
    public Text eggsFoundText;
    public Text healthLostText;
    public Text timePlayedText;

    [Header("Tags List")]
    [Tooltip("0.Player, 1.Enemy, 2.Wall, 3.Door 4.DoorTrigger 5.Bell 6.HittableObjets 7.FakeWall")]
    public List<string> tagList = new List<string>();
    #endregion

    #region Private Variables
    private int healthModifier;
    private bool combateMode;
    private float deltaTime;
    private float gemsPanelTimer;
    private float healthPanelTimer;
    private bool isGamePaused;
    private bool showGemsPanel;
    private bool showHealthPanel;
    private bool addingGems;
    private bool addingHealth;
    private bool hasKey;

    //Persistance variables
    private int maxNumOfGems = 0;
    private int currentNumOfGems = 0;
    private int currentNumOfGhosts = 0;
    private float gameTimeStart;
    private int currentHealthLost = 0;

    private List<GameObject> allGems = new List<GameObject>();
    private List<EnemyHUD> enemyHUDList = new List<EnemyHUD>();
    private List<EnemyHUD> enemyHUDWaitingList = new List<EnemyHUD>();

    //Pause Panel Variables
    //private int actionIndex = 0; ------------ Provisional, don't erase
    #endregion

    private void Awake()
    {
        Time.timeScale = 1;
        gameTimeStart = Time.timeSinceLevelLoad;
    }

    private void Start()
    {
        pausePanel.SetActive(false);

        //Set-up HUD
        if (!showFPS)
            fpsText.gameObject.SetActive(false);
        else
            fpsText.gameObject.SetActive(true);

        Physics.IgnoreLayerCollision(0, 11, true);
    }

    private void Update()
    {
        if (showFPS)
            ShowFPS();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
            AddGem();

        if (Input.GetKeyDown(KeyCode.H))
            player.RecieveDamage(20);

        if (Input.GetKeyDown(KeyCode.J))
            player.IncreaseHealth(20);

        if (Input.GetKeyDown(KeyCode.T))
            player.GetGreenLight();

        if (Input.GetKeyDown(KeyCode.Y))
            player.GetRedLight();
#endif

        MovingPanels();

        if (isGamePaused)
            PauseActions();
        else if (InputsManager.Instance.GetStartButtonDown())
            PauseGame();

    }

    #region Door Methods
    public void UnblockPlayerDoors()
    {
        combateMode = false;

        player.CalmMode();

        Ray ray;
        RaycastHit hit;

        ray = new Ray(GetPlayer().position, Vector3.down);
        Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("FloorLayer"));

        foreach (ConectionScript d in doorsList)
        {
            if (d.IsDoorFromRoom(hit.transform.parent.gameObject))
            {
                d.UnblockDoor();
            }
        }

        foreach (FakeWallScript f in fakeWallsList)
        {
            if (f.IsDoorFromRoom(hit.transform.parent.gameObject))
            {
                f.UnblockDoor();
            }
        }
    }

    public void ShowAndHideDoors()
    {
        Ray ray;
        RaycastHit hit;

        ray = new Ray(GetPlayer().position, Vector3.down);
        Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("FloorLayer"));

        foreach (ConectionScript d in doorsList)
        {
            if (d.IsDoorFromRoom(hit.transform.parent.gameObject))
            {
                d.ShowVisualDoor();
            }
            else
            {
                d.HideVisualDoor();
            }
        }
    }

    public void ShowAllDoors()
    {
        foreach (ConectionScript d in doorsList)
        {
            d.ShowVisualDoor();
        }
    }

    public void BlockPlayerDoors()
    {
        combateMode = true;

        player.CombateMode();

        Ray ray;
        RaycastHit hit;

        ray = new Ray(GetPlayer().position, Vector3.down);
        Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("FloorLayer"));

        foreach (ConectionScript d in doorsList)
        {
            if (d.IsDoorFromRoom(hit.transform.parent.gameObject))
            {
                d.BlockDoor();
            }
        }
    }
    #endregion

    #region Light Methods
    public void SwitchMainLight()
    {
        directionalLight.enabled = !directionalLight.enabled;
        if (directionalLight.enabled)
            RenderSettings.ambientIntensity = 1;
        else
            RenderSettings.ambientIntensity = 0;
    }
    #endregion

    #region HUD Methods
    public void SetInitialHp(int currentHp)
    {
        ModifyHealthPanel(currentHp);
    }

    public void ModifyHp(int currentHp)
    {
        healthModifier = currentHp;
        addingHealth = true;
        ShowHealthPanel();
    }

    public void ActivePlayerHUD(ButtonRequest req)
    {
        playerHUD.ShowSpecificButton(req);
    }

    public void CreateEnemyHUD(Transform target, int initialHp)
    {
        if (enemyHUDWaitingList.Count == 0)
        {
            GameObject go = Instantiate(enemyHUDPrefab, mainCanvas.transform) as GameObject;
            EnemyHUD e = go.GetComponent<EnemyHUD>();
            e.SetUp(target, initialHp);
            enemyHUDList.Add(e);
        }
        else
        {
            EnemyHUD e = enemyHUDWaitingList[0];
            enemyHUDWaitingList.Remove(e);
            e.gameObject.SetActive(true);
            e.SetUp(target, initialHp);
            enemyHUDList.Add(e);
        }
    }

    public void ModifyEnemyHp(Transform target, int currentHp)
    {
        enemyHUDList.Find(x => x.GetTarget() == target).ModifyHp(currentHp);
    }

    public void DestroyEnemyHUD(Transform target)
    {
        enemyHUDList.ForEach(x =>
        {
            if (x.GetTarget() == target)
            {
                x.gameObject.SetActive(false);
                enemyHUDWaitingList.Add(x);
                enemyHUDList.Remove(x);
            }
        });
    }

    public void DisablePlayerHUD()
    {
        playerHUD.HideImage();
    }

    private void MovingPanels()
    {
        HealthPanel();
        GemsPanel();
    }

    private void HealthPanel()
    {
        if (showHealthPanel)
        {
            if (healthPanelTimer >= 1)
            {
                if (addingHealth)
                {
                    StartCoroutine(ModifyHealthCoroutine(healthModifier));
                    addingHealth = false;
                }

                healthPanelTimer = 1;
            }
            else
                healthPanelTimer += Time.unscaledDeltaTime / healthPanelTime;
        }
        else
        {
            if (healthPanelTimer <= 0)
                healthPanelTimer = 0;
            else
                healthPanelTimer -= Time.unscaledDeltaTime / healthPanelTime;
        }

        healthPanel.anchoredPosition = Vector2.up * Mathf.Lerp(healthPanelYHidden, healthPanelYShown, healthPanelTimer) + Vector2.right * healthPanel.anchoredPosition.x;
    }

    private void GemsPanel()
    {
        if (showGemsPanel)
        {
            if (gemsPanelTimer >= 1)
            {
                //Call 
                if (addingGems)
                {
                    StartCoroutine(AddNewGemGathered());
                    addingGems = false;
                }

                gemsPanelTimer = 1;
            }
            else
                gemsPanelTimer += Time.unscaledDeltaTime / gemsPanelTime;
        }
        else
        {
            if (gemsPanelTimer <= 0)
                gemsPanelTimer = 0;
            else
                gemsPanelTimer -= Time.unscaledDeltaTime / gemsPanelTime;
        }

        gemsPanel.anchoredPosition = Vector2.up * Mathf.Lerp(gemsPanelYHidden, gemsPanelYShown, gemsPanelTimer) + Vector2.right * gemsPanel.anchoredPosition.x;
    }

    private void AddGem()
    {
        addingGems = true;
        ShowGemsPanel();
    }

    private void ModifyHealthPanel(int currentHp)
    {
        hpText.text = currentHp.ToString();
        hpTextShadow.text = hpText.text;
    }

    private void VisualGemAdd()
    {
        currentNumOfGems++;

        foreach (GameObject e in diamondEggMask)
        {
            if (e.activeInHierarchy)
            {
                e.SetActive(false);
                return;
            }
        }
    }

    IEnumerator ModifyHealthCoroutine(int currentHp)
    {
        yield return new WaitForSecondsRealtime(timeBeforeChangingHealth);
        ModifyHealthPanel(currentHp);
        yield return new WaitForSecondsRealtime(timeShowingHealthPanel);
        HideHealthPanel();
        yield return null;
    }

    IEnumerator AddNewGemGathered()
    {
        yield return new WaitForSecondsRealtime(timeBeforeAddingGem);
        VisualGemAdd();
        yield return new WaitForSecondsRealtime(timeShowingGemsPanel);
        HideGemsPanel();
        yield return null;
    }

    public void ShowHealthPanel()
    {
        showHealthPanel = true;
    }

    public void HideHealthPanel()
    {
        if (player.GetCurrentHp() == player.initialHp)
            showHealthPanel = false;
    }

    public void ShowGemsPanel()
    {
        showGemsPanel = true;
    }

    public void HideGemsPanel()
    {
        showGemsPanel = false;
    }
    #endregion

    #region Persistance Modify Methods
    public void IncreaseMaxGems(GameObject spawner)
    {
        allGems.Add(spawner);
        maxNumOfGems++;
    }

    public void IncreaseNumOfGems()
    {
        currentNumOfGems++;
        AddGem();

        if (currentNumOfGems >= maxNumOfGems)
            CallPlayerVictory();
    }

    public void IncreseNumOfGhostsCaptured()
    {
        currentNumOfGhosts++;
    }

    public void IncreaseHealthLost(int healthLost)
    {
        currentHealthLost += healthLost;
    }
    #endregion

    #region Getter Methods
    public bool GetIsPlayerPanelActive()
    {
        return playerHUD.GetIsActive();
    }

    public bool GetIsInCombateMode()
    {
        return combateMode;
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public bool IsDirectLightActivated()
    {
        return directionalLight.enabled;
    }

    public string GetTagOfDesiredType(TypeOfTag t)
    {
        return tagList[(int)t];
    }

    public Transform GetPlayer()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        return player.transform;
    }

    public Vector2 GetCanvasResolution()
    {
        return mainCanvas.GetComponent<CanvasScaler>().referenceResolution;
    }
    #endregion

    //Setters

    #region Game Events Methods
    public void GetKey()
    {
        hasKey = true;
    }
    #endregion

    #region FPS Method
    private void ShowFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        //float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + ((int)fps).ToString();
    }
    #endregion

    #region Pause Methods
    private void PauseActions()
    {
        if (InputsManager.Instance.GetBackButtonInputDown() && !confirmationPanelOpen) //Input.GetKeyDown("joystick button 1")
            Resume();

        if (confirmationPanelOpen)
        {
            if (InputsManager.Instance.GetBackButtonInputDown())
            {
                if (restartConfirmationPanel.gameObject.activeSelf)
                    HideRestartConfirmationPanel();

                if (settingsPanel.gameObject.activeSelf)
                    HideSettingsPanel();

                if (menuConfirmationPanel.gameObject.activeSelf)
                    HideMenuConfirmationPanel();

                if (quitConfirmationPanel.gameObject.activeSelf)
                    HideQuitConfirmationPanel();
            }
        }
    }

    private void PauseGame()
    {
        if (!confirmationPanelOpen)
        {
            StartCoroutine(HighlightButton(resumeButton));

            ShowGemsPanel();
            ShowHealthPanel();

            Time.timeScale = 0;

            Debug.Log("Showing pause panel");
            pausePanel.SetActive(true);
            pauseMenuGO.SetActive(true);

            isGamePaused = !isGamePaused;
        }

    }

    public void Resume()
    {
        HideGemsPanel();
        HideHealthPanel();

        Debug.Log("Hide pause panel");
        isGamePaused = false;
        pausePanel.SetActive(false);
        pauseMenuGO.SetActive(false);
        Time.timeScale = 1;
    }

    #region Restart Button
    public void RestartScene()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ShowRestartConfirmationPanel()
    {
        StartCoroutine(HighlightButton(yesButtonRestartConfirmationPanel));
        confirmationPanelOpen = true;
        restartConfirmationPanel.SetActive(true);
        pauseMenuGO.SetActive(false);
    }

    public void HideRestartConfirmationPanel()
    {
        StartCoroutine(HighlightButton(restartButton));
        confirmationPanelOpen = false;
        restartConfirmationPanel.SetActive(false);
        pauseMenuGO.SetActive(true);
    }
    #endregion

    #region Menu Button
    public void LoadMenu()
    {
        isGamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void ShowMenuConfirmationPanel()
    {
        StartCoroutine(HighlightButton(yesButtonMenuConfirmationPanel));
        confirmationPanelOpen = true;
        menuConfirmationPanel.SetActive(true);
        pauseMenuGO.SetActive(false);
    }

    public void HideMenuConfirmationPanel()
    {
        StartCoroutine(HighlightButton(menuButton));
        confirmationPanelOpen = false;
        menuConfirmationPanel.SetActive(false);
        pauseMenuGO.SetActive(true);
    }
    #endregion

    #region Settings Button
    public void ShowSettingsPanel()
    {
        StartCoroutine(HighlightButton(controllerToggle));
        confirmationPanelOpen = true;
        settingsPanel.SetActive(true);
        pauseMenuGO.SetActive(false);
    }

    public void HideSettingsPanel()
    {
        StartCoroutine(HighlightButton(settingsButton));
        confirmationPanelOpen = false;
        settingsPanel.SetActive(false);
        pauseMenuGO.SetActive(true);
    }

    public void SetControllerToggle()
    {
        controllerToggleIsChecked = !controllerToggleIsChecked;

        if (controllerToggleIsChecked)
        {
            InputsManager.Instance.isControllerPlaying = true; //InputsManager.isControllerPlaying = true;
        }
        else
        {
            InputsManager.Instance.isControllerPlaying = false; //InputsManager.isControllerPlaying = false;
        }
    }
    #endregion

    #region Quit Button
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //si le damos al botón de Quit en Unity, parará de jugar
#else
        Application.Quit(); //si le damos Quit fuera de Unity, cerrará el programa
#endif
    }

    public void ShowQuitConfirmationPanel()
    {
        StartCoroutine(HighlightButton(yesButtonQuitConfirmationPanel));
        confirmationPanelOpen = true;
        quitConfirmationPanel.SetActive(true);
        pauseMenuGO.SetActive(false);
    }

    public void HideQuitConfirmationPanel()
    {
        StartCoroutine(HighlightButton(quitButton));
        confirmationPanelOpen = false;
        quitConfirmationPanel.SetActive(false);
        pauseMenuGO.SetActive(true);
    }
    #endregion

    IEnumerator HighlightButton(GameObject myButton)
    {
        myEventSystem.SetSelectedGameObject(null);
        yield return null;
        myEventSystem.SetSelectedGameObject(myButton);//myEventSystem.firstSelectedGameObject);
        yield return null;
    }
    #endregion

    #region Game State Methods
    public void CallPlayerDeath()
    {//Game Over
        gameOverPanel.SetActive(true);
        StartCoroutine(HighlightButton(gameOverRestartButton));
        /*Debug.Log("You have lost");
        Debug.Log("Quantity of ghost hunted: " + currentNumOfGhosts);
        Debug.Log("Quantity of diamond eggs found: " + currentNumOfGems);
        Debug.Log("Quantity of health lost: " + currentHealthLost);
        Debug.Log("Time played: " + (Time.timeSinceLevelLoad - gameTimeStart).ToString());*/
        Time.timeScale = 0;
    }

    public void CallPlayerVictory()
    {//Victory
        victoryPanel.SetActive(true);
        StartCoroutine(HighlightButton(victoryOKButton));
        ghostHuntedText.text = "Quantity of ghost hunted: " + currentNumOfGhosts;
        eggsFoundText.text = "Quantity of diamond eggs found: " + currentNumOfGems;
        healthLostText.text = "Quantity of health lost: " + currentHealthLost;
        timePlayedText.text = "Time played: " + (Time.timeSinceLevelLoad - gameTimeStart).ToString();
        /*Debug.Log("A winner is you");
        Debug.Log("Quantity of ghost hunted: "+currentNumOfGhosts);
        Debug.Log("Quantity of diamond eggs found: "+currentNumOfGems);
        Debug.Log("Quantity of health lost: "+currentHealthLost);
        Debug.Log("Time played: "+(Time.timeSinceLevelLoad - gameTimeStart).ToString());*/
        Time.timeScale = 0;
    }
    #endregion
}
