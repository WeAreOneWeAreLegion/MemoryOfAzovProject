using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager> {

    public enum SoundRequest { P_Knock, P_OpenDoor, P_ButtonPush, E_Cry }
    public enum SoundRequestAmbient { A_Rain, A_RainOff, A_Thunder }
    public enum SoundRequestPlayer { P_Damaged, P_Death, P_Scared, P_ShakingHead, P_StepsMarble, P_StepsCarpet, P_LowHealth }
    public enum SoundRequestGhost { G_Attack, G_Boo, G_Damaged, G_Dead, G_Laugh, G_LaughALot, G_Stunned }
    public enum SoundRequestScenario { S_Button, S_LightbulbSpawns, S_LightbulbFalls, S_DoorOpen, S_Fireplace, S_FireplaceOff, S_FurnitureShaking, S_HealthItemDropped,
        S_HealthItemFound, S_ItemFound, S_ItemParticles, S_ItemParticlesOff, S_LockedDoor, S_SpecialItemFound, S_UnlockDoor, S_Lever, S_Library, S_PictureFalls }
    public enum SoundRequestFlashlight { F_ChargingFlash, F_ChargedFlash, F_ChargedFlashOff, F_Flash, F_On, F_Off }
    public enum SoundRequestMenu { Me_Movement, Me_Select, Me_SelectBack }
    public enum SoundRequestMusic { Mu_HouseOn, MU_CombatOn, MU_GameOverOn, MU_VictoryOn, MU_HouseOff, MU_CombatOff, MU_GameOverOff, MU_VictoryOff }

    #region Public Variables
    [Header("\t    Own Script Variables")]
    [Header("Player Sounds")]
    [Tooltip("Lista de sonidos de golpe")]
    public List<AudioClip> knockSounds;
    [Tooltip("Lista de sonidos de abrir puerta")]
    public List<AudioClip> openDoorSounds;
    [Tooltip("Lista de sonidos de pulsar boton")]
    public List<AudioClip> buttonPushSounds;

    [Header("Enemy Sounds")]
    [Tooltip("Lista de sonidos de enemigos gritando")]
    public List<AudioClip> enemyCrySound;

    /*[FMODUnity.EventRef] //we are looking in FMOD for a sound
    public string selectSound; //to pick our sound from the inspector
    FMOD.Studio.EventInstance soundEvent;*/

    [Header("Ambient Sounds")]
    [FMODUnity.EventRef] //we are looking in FMOD for a sound
    public string ambientRainSound; //to pick our sound from the inspector
    FMOD.Studio.EventInstance soundEventAmbientRainSound;

    [FMODUnity.EventRef] //we are looking in FMOD for a sound
    public string ambientThunderSound; //to pick our sound from the inspector
    FMOD.Studio.EventInstance soundEventAmbientThunderSound;

    [Header("Character Sounds")]
    [FMODUnity.EventRef]
    public string characterDamagedSound;
    FMOD.Studio.EventInstance soundEventCharacterDamagedSound;

    [FMODUnity.EventRef]
    public string characterDeathSound;
    FMOD.Studio.EventInstance soundEventCharacterDeathSound;

    [FMODUnity.EventRef]
    public string characterScaredSound;
    FMOD.Studio.EventInstance soundEventCharacterScaredSound;

    [FMODUnity.EventRef]
    public string characterShakingHeadSound;
    FMOD.Studio.EventInstance soundEventCharacterShakingHeadSound;

    [FMODUnity.EventRef]
    public string characterStepsCarpetSound;
    FMOD.Studio.EventInstance soundEventCharacterStepsCarpetSound;

    [FMODUnity.EventRef]
    public string characterStepsMarbleSound;
    FMOD.Studio.EventInstance soundEventCharacterStepsMarbleSound;

    [FMODUnity.EventRef]
    public string characterLowHealthSound;
    FMOD.Studio.EventInstance soundEventCharacterLowHealthSound;

    [Header("Enemy Sounds")]
    [FMODUnity.EventRef]
    public string ghostAttackSound;
    FMOD.Studio.EventInstance soundEventGhostAttackSound;

    [FMODUnity.EventRef]
    public string ghostBooSound;
    FMOD.Studio.EventInstance soundEventGhostBooSound;

    [FMODUnity.EventRef]
    public string ghostDamagedSound;
    FMOD.Studio.EventInstance soundEventGhostDamagedSound;

    [FMODUnity.EventRef]
    public string ghostDeadSound;
    FMOD.Studio.EventInstance soundEventGhostDeadSound;

    [FMODUnity.EventRef]
    public string ghostLaughSound;
    FMOD.Studio.EventInstance soundEventGhostLaughSound;

    [FMODUnity.EventRef]
    public string ghostLaughALotSound;
    FMOD.Studio.EventInstance soundEventGhostLaughALotSound;

    [FMODUnity.EventRef]
    public string ghostStunnedSound;
    FMOD.Studio.EventInstance soundEventGhostStunnedSound;

    [Header("Scenario Sounds")]
    [FMODUnity.EventRef]
    public string scenarioButtonSound;
    FMOD.Studio.EventInstance soundEventScenarioButtonSound;

    [FMODUnity.EventRef]
    public string scenarioLightbulbSpawnsSound;
    FMOD.Studio.EventInstance soundEventScenarioLightbulbSpawnsSound;

    [FMODUnity.EventRef]
    public string scenarioLightbulbFallsSound;
    FMOD.Studio.EventInstance soundEventScenarioLightbulbFallsSound;

    [FMODUnity.EventRef]
    public string scenarioDoorOpenSound;
    FMOD.Studio.EventInstance soundEventScenarioDoorOpenSound;

    [FMODUnity.EventRef]
    public string scenarioFireplaceSound;
    FMOD.Studio.EventInstance soundEventScenarioFireplaceSound;

    [FMODUnity.EventRef]
    public string scenarioFurnitureShakingSound;
    FMOD.Studio.EventInstance soundEventScenarioFurnitureShakingSound;

    [FMODUnity.EventRef]
    public string scenarioHealItemDroppedSound;
    FMOD.Studio.EventInstance soundEventScenarioHealItemDroppedSound;

    [FMODUnity.EventRef]
    public string scenarioHealItemFoundSound;
    FMOD.Studio.EventInstance soundEventScenarioHealItemFoundSound;

    [FMODUnity.EventRef]
    public string scenarioItemFoundSound;
    FMOD.Studio.EventInstance soundEventScenarioItemFoundSound;

    [FMODUnity.EventRef]
    public string scenarioItemLightParticlesSound;
    FMOD.Studio.EventInstance soundEventScenarioItemLightParticlesSound;

    [FMODUnity.EventRef]
    public string scenarioLockedDoorSound;
    FMOD.Studio.EventInstance soundEventScenarioLockedDoorSound;

    [FMODUnity.EventRef]
    public string scenarioSpecialItemFoundSound;
    FMOD.Studio.EventInstance soundEventScenarioSpecialItemFoundSound;

    [FMODUnity.EventRef]
    public string scenarioUnlockDoorSound;
    FMOD.Studio.EventInstance soundEventScenarioUnlockDoorSound;

    [FMODUnity.EventRef]
    public string scenarioLeverSound;
    FMOD.Studio.EventInstance soundEventScenarioLeverSound;

    [FMODUnity.EventRef]
    public string scenarioLibrarySound;
    FMOD.Studio.EventInstance soundEventScenarioLibrarySound;

    [FMODUnity.EventRef]
    public string scenarioPictureFallsSound;
    FMOD.Studio.EventInstance soundEventScenarioPictureFallsSound;

    [Header("Flashlight Sounds")]
    [FMODUnity.EventRef]
    public string flashlightChargingFlashSound;
    FMOD.Studio.EventInstance soundEventFlashlightChargingFlashSound;

    [FMODUnity.EventRef]
    public string flashlightChargedFlashSound;
    FMOD.Studio.EventInstance soundEventFlashlightChargedFlashSound;

    [FMODUnity.EventRef]
    public string flashlightFlashSound;
    FMOD.Studio.EventInstance soundEventFlashlightFlashSound;

    [FMODUnity.EventRef]
    public string flashlightOffSound;
    FMOD.Studio.EventInstance soundEventFlashlightOffSound;

    [FMODUnity.EventRef]
    public string flashlightOnSound;
    FMOD.Studio.EventInstance soundEventFlashlightOnSound;

    [Header("Menu Sounds")]
    [FMODUnity.EventRef]
    public string menuHUDMovementSound;
    FMOD.Studio.EventInstance soundEventMenuHUDMovementSound;

    [FMODUnity.EventRef]
    public string menuHUDSelectSound;
    FMOD.Studio.EventInstance soundEventMenuHUDSelectSound;

    [FMODUnity.EventRef]
    public string menuHUDSelectBackSound;
    FMOD.Studio.EventInstance soundEventMenuHUDSelectBackSound;

    [Header("Music")]
    [FMODUnity.EventRef]
    public string houseMusic;
    FMOD.Studio.EventInstance soundEventHouseMusic;

    [FMODUnity.EventRef]
    public string combatMusic;
    FMOD.Studio.EventInstance soundEventCombatMusic;

    [FMODUnity.EventRef]
    public string gameOverMusic;
    FMOD.Studio.EventInstance soundEventGameOverMusic;

    [FMODUnity.EventRef]
    public string victoryMusic;
    FMOD.Studio.EventInstance soundEventVictoryMusic;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound); //to tie the sounds together
        //Ambient
        soundEventAmbientRainSound = FMODUnity.RuntimeManager.CreateInstance(ambientRainSound);
        soundEventAmbientThunderSound = FMODUnity.RuntimeManager.CreateInstance(ambientThunderSound);

        //Character
        soundEventCharacterDamagedSound = FMODUnity.RuntimeManager.CreateInstance(characterDamagedSound);
        soundEventCharacterDeathSound = FMODUnity.RuntimeManager.CreateInstance(characterDeathSound);
        soundEventCharacterScaredSound = FMODUnity.RuntimeManager.CreateInstance(characterScaredSound);
        soundEventCharacterShakingHeadSound = FMODUnity.RuntimeManager.CreateInstance(characterShakingHeadSound);
        soundEventCharacterStepsCarpetSound = FMODUnity.RuntimeManager.CreateInstance(characterStepsCarpetSound);
        soundEventCharacterStepsMarbleSound = FMODUnity.RuntimeManager.CreateInstance(characterStepsMarbleSound);
        soundEventCharacterLowHealthSound = FMODUnity.RuntimeManager.CreateInstance(characterLowHealthSound);

        //Ghost
        soundEventGhostAttackSound = FMODUnity.RuntimeManager.CreateInstance(ghostAttackSound);
        soundEventGhostBooSound = FMODUnity.RuntimeManager.CreateInstance(ghostBooSound);
        soundEventGhostDamagedSound = FMODUnity.RuntimeManager.CreateInstance(ghostDamagedSound);
        soundEventGhostDeadSound = FMODUnity.RuntimeManager.CreateInstance(ghostDeadSound);
        soundEventGhostLaughSound = FMODUnity.RuntimeManager.CreateInstance(ghostLaughSound);
        soundEventGhostLaughALotSound = FMODUnity.RuntimeManager.CreateInstance(ghostLaughALotSound);
        soundEventGhostStunnedSound = FMODUnity.RuntimeManager.CreateInstance(ghostStunnedSound);

        //Scenario
        soundEventScenarioButtonSound = FMODUnity.RuntimeManager.CreateInstance(scenarioButtonSound);
        soundEventScenarioLightbulbSpawnsSound = FMODUnity.RuntimeManager.CreateInstance(scenarioLightbulbSpawnsSound);
        soundEventScenarioLightbulbFallsSound = FMODUnity.RuntimeManager.CreateInstance(scenarioLightbulbFallsSound);
        soundEventScenarioDoorOpenSound = FMODUnity.RuntimeManager.CreateInstance(scenarioDoorOpenSound);
        soundEventScenarioFireplaceSound = FMODUnity.RuntimeManager.CreateInstance(scenarioFireplaceSound);
        soundEventScenarioFurnitureShakingSound = FMODUnity.RuntimeManager.CreateInstance(scenarioFurnitureShakingSound);
        soundEventScenarioHealItemDroppedSound = FMODUnity.RuntimeManager.CreateInstance(scenarioHealItemDroppedSound);
        soundEventScenarioHealItemFoundSound = FMODUnity.RuntimeManager.CreateInstance(scenarioHealItemFoundSound);
        soundEventScenarioItemFoundSound = FMODUnity.RuntimeManager.CreateInstance(scenarioItemFoundSound);
        soundEventScenarioItemLightParticlesSound = FMODUnity.RuntimeManager.CreateInstance(scenarioItemLightParticlesSound);
        soundEventScenarioLockedDoorSound = FMODUnity.RuntimeManager.CreateInstance(scenarioLockedDoorSound);
        soundEventScenarioSpecialItemFoundSound = FMODUnity.RuntimeManager.CreateInstance(scenarioSpecialItemFoundSound);
        soundEventScenarioUnlockDoorSound = FMODUnity.RuntimeManager.CreateInstance(scenarioUnlockDoorSound);
        soundEventScenarioLeverSound = FMODUnity.RuntimeManager.CreateInstance(scenarioLeverSound);
        soundEventScenarioLibrarySound = FMODUnity.RuntimeManager.CreateInstance(scenarioLibrarySound);
        soundEventScenarioPictureFallsSound = FMODUnity.RuntimeManager.CreateInstance(scenarioPictureFallsSound);

        //Flashlight
        soundEventFlashlightChargingFlashSound = FMODUnity.RuntimeManager.CreateInstance(flashlightChargingFlashSound);
        soundEventFlashlightChargedFlashSound = FMODUnity.RuntimeManager.CreateInstance(flashlightChargedFlashSound);
        soundEventFlashlightFlashSound = FMODUnity.RuntimeManager.CreateInstance(flashlightFlashSound);
        soundEventFlashlightOffSound = FMODUnity.RuntimeManager.CreateInstance(flashlightOffSound);
        soundEventFlashlightOnSound = FMODUnity.RuntimeManager.CreateInstance(flashlightOnSound);

        //Menu
        soundEventMenuHUDMovementSound = FMODUnity.RuntimeManager.CreateInstance(menuHUDMovementSound);
        soundEventMenuHUDSelectSound = FMODUnity.RuntimeManager.CreateInstance(menuHUDSelectSound);
        soundEventMenuHUDSelectBackSound = FMODUnity.RuntimeManager.CreateInstance(menuHUDSelectBackSound);

        //Music
        soundEventHouseMusic = FMODUnity.RuntimeManager.CreateInstance(houseMusic);
        soundEventCombatMusic = FMODUnity.RuntimeManager.CreateInstance(combatMusic);
        soundEventGameOverMusic = FMODUnity.RuntimeManager.CreateInstance(gameOverMusic);
        soundEventVictoryMusic = FMODUnity.RuntimeManager.CreateInstance(victoryMusic);

        if (SceneManager.GetActiveScene().name == "Main")
        PlayHouseMusic();
    }

    void Update()
    {
        UpdateAmbientSounds();
        UpdateCharacterSounds();
        UpdateGhostSounds();
        UpdateScenarioSounds();
        UpdateFlashlightSounds();
        UpdateMenuSounds();
    }

    #region Sound Getters Methods
    public void AmbientSoundEnum(SoundRequestAmbient sra, Transform target)
    {
        switch (sra)
        {
            case SoundRequestAmbient.A_Rain:
                PlayAmbientRainSound();
                break;
            case SoundRequestAmbient.A_RainOff:
                StopAmbientRainSound();
                break;
            case SoundRequestAmbient.A_Thunder:
                PlayAmbientThunderSound();
                break;
        }
    }

    public void PlayerSoundEnum(SoundRequestPlayer srp)
    {
        switch (srp)
        {
            case SoundRequestPlayer.P_Damaged:
                PlayCharacterDamagedSound();
                break;
            case SoundRequestPlayer.P_Death:
                PlayCharacterDeathSound();
                break;
            case SoundRequestPlayer.P_Scared:
                PlayCharacterScaredSound();
                break;
            case SoundRequestPlayer.P_ShakingHead:
                PlayCharacterShakingHeadSound();
                break;
            case SoundRequestPlayer.P_StepsMarble:
                PlayCharacterStepsMarbleSound();
                break;
            case SoundRequestPlayer.P_StepsCarpet:
                PlayCharacterStepsCarpetSound();
                break;
            case SoundRequestPlayer.P_LowHealth:
                PlayCharacterLowHealthSound();
                break;
        }
    }

    public void GhostSoundEnum(SoundRequestGhost srg)
    {
        switch (srg)
        {
            case SoundRequestGhost.G_Attack:
                PlayGhostAttackSound();
                break;
            case SoundRequestGhost.G_Boo:
                PlayGhostBooSound();
                break;
            case SoundRequestGhost.G_Damaged:
                PlayGhostDamagedSound();
                break;
            case SoundRequestGhost.G_Dead:
                PlayGhostDeadSound();
                break;
            case SoundRequestGhost.G_Laugh:
                PlayGhostLaughSound();
                break;
            case SoundRequestGhost.G_LaughALot:
                PlayGhostLaughALotSound();
                break;
            case SoundRequestGhost.G_Stunned:
                PlayGhostStunnedSound();
                break;
        }
    }

    public void ScenarioSoundEnum(SoundRequestScenario srs, Transform target)
    {
        switch (srs)
        {
            case SoundRequestScenario.S_Button:
                PlayScenarioButtonSound();
                break;
            case SoundRequestScenario.S_LightbulbFalls:
                PlayScenarioLightbulbSpawnsSound();
                break;
            case SoundRequestScenario.S_LightbulbSpawns:
                PlayScenarioLightbulbFallsSound();
                break;
            case SoundRequestScenario.S_DoorOpen:
                PlayScenarioDoorOpenSound();
                break;
            case SoundRequestScenario.S_Fireplace:
                PlayScenarioFireplaceSound();
                break;
            case SoundRequestScenario.S_FireplaceOff:
                StopScenarioFireplaceSound();
                break;
            case SoundRequestScenario.S_FurnitureShaking:
                PlayScenarioFurnitureShakingSound();
                break;
            case SoundRequestScenario.S_HealthItemDropped:
                PlayScenarioHealItemDroppedSound();
                break;
            case SoundRequestScenario.S_HealthItemFound:
                PlayScenarioHealItemFoundSound();
                break;
            case SoundRequestScenario.S_ItemFound:
                PlayScenarioItemFoundSound();
                break;
            case SoundRequestScenario.S_ItemParticles:
                PlayScenarioItemLightParticlesSound();
                break;
            case SoundRequestScenario.S_ItemParticlesOff:
                StopScenarioItemLightParticlesSound();
                break;
            case SoundRequestScenario.S_LockedDoor:
                PlayScenarioLockedDoorSound();
                break;
            case SoundRequestScenario.S_SpecialItemFound:
                PlayScenarioSpecialItemFoundSound();
                break;
            case SoundRequestScenario.S_UnlockDoor:
                PlayScenarioUnlockDoorSound();
                break;
            case SoundRequestScenario.S_Lever:
                PlayScenarioLeverSound();
                break;
            case SoundRequestScenario.S_Library:
                PlayScenarioLibrarySound();
                break;
            case SoundRequestScenario.S_PictureFalls:
                PlayScenarioPictureFallsSound();
                break;
        }
    }

    public void FlashlightSoundEnum(SoundRequestFlashlight srf)
    {
        switch (srf)
        {
            case SoundRequestFlashlight.F_ChargingFlash:
                PlayFlashlightChargingFlashSound();
                break;
            case SoundRequestFlashlight.F_ChargedFlash:
                PlayFlashlightChargedFlashSound();
                break;
            case SoundRequestFlashlight.F_ChargedFlashOff:
                StopFlashlightChargedFlashSound();
                break;
            case SoundRequestFlashlight.F_Flash:
                PlayFlashlightFlashSound();
                break;
            case SoundRequestFlashlight.F_Off:
                PlayFlashlightOffSound();
                break;
            case SoundRequestFlashlight.F_On:
                PlayFlashlightOnSound();
                break;
        }
    }

    public void MenuSoundEnum(SoundRequestMenu srme)
    {
        switch (srme)
        {
            case SoundRequestMenu.Me_Movement:
                PlayMenuHUDMovementSound();
                break;
            case SoundRequestMenu.Me_Select:
                PlayMenuHUDSelectSound();
                break;
            case SoundRequestMenu.Me_SelectBack:
                PlayMenuHUDSelectBackSound();
                break;
        }
    }

    public void MusicSoundEnum(SoundRequestMusic srmu)
    {
        switch (srmu)
        {
            case SoundRequestMusic.Mu_HouseOn:
                PlayHouseMusic();
                break;
            case SoundRequestMusic.MU_CombatOn:
                PlayCombatMusic();
                break;
            case SoundRequestMusic.MU_GameOverOn:
                PlayGameOverMusic();
                break;
            case SoundRequestMusic.MU_VictoryOn:
                PlayVictoryMusic();
                break;
            case SoundRequestMusic.MU_HouseOff:
                StopHouseMusic();
                break;
            case SoundRequestMusic.MU_CombatOff:
                StopCombatMusic();
                break;
            case SoundRequestMusic.MU_GameOverOff:
                StopGameOverMusic();
                break;
            case SoundRequestMusic.MU_VictoryOff:
                StopVictoryMusic();
                break;
        }
    }
    #endregion

    #region Update Sound Methods
    void UpdateAmbientSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventAmbientRainSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventAmbientThunderSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateCharacterSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterDamagedSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterDeathSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterScaredSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterShakingHeadSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterStepsCarpetSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterStepsMarbleSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventCharacterLowHealthSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateGhostSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostAttackSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostBooSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostDamagedSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostDeadSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostLaughSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostLaughALotSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventGhostStunnedSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateScenarioSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioButtonSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioLightbulbSpawnsSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioLightbulbFallsSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioDoorOpenSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioFireplaceSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioHealItemDroppedSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioHealItemFoundSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioItemFoundSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioItemLightParticlesSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioLockedDoorSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioSpecialItemFoundSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioUnlockDoorSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioLeverSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioLibrarySound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioPictureFallsSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateFlashlightSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightChargingFlashSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightChargedFlashSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightFlashSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightOffSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightOnSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateMenuSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventMenuHUDMovementSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventMenuHUDSelectSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventMenuHUDSelectBackSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }
    #endregion

    /*public void PlaySound(string selectSound)
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound);
        soundEvent.start();
    }

    public void PlaySound()
    {
        soundEventCharacterDamagedSound.start();
    }*/

    #region Play Sound Methods
    #region Ambient Methods
    public void PlayAmbientRainSound()
    {
        soundEventAmbientRainSound.start();
    }

    public void PlayAmbientThunderSound()
    {
        soundEventAmbientThunderSound.start();
    }
    #endregion

    #region Character Methods
    public void PlayCharacterDamagedSound()
    {
        soundEventCharacterDamagedSound.start();
    }

    public void PlayCharacterDeathSound()
    {
        soundEventCharacterDeathSound.start();
    }

    public void PlayCharacterScaredSound()
    {
        soundEventCharacterScaredSound.start();
    }

    public void PlayCharacterShakingHeadSound()
    {
        soundEventCharacterShakingHeadSound.start();
    }

    public void PlayCharacterStepsCarpetSound()
    {
        soundEventCharacterStepsCarpetSound.start();
    }

    public void PlayCharacterStepsMarbleSound()
    {
        soundEventCharacterStepsMarbleSound.start();
    }

    public void PlayCharacterLowHealthSound()
    {
        soundEventCharacterLowHealthSound.start();
    }
    #endregion

    #region Ghost Methods
    public void PlayGhostAttackSound()
    {
        soundEventGhostAttackSound.start();
    }

    public void PlayGhostBooSound()
    {
        soundEventGhostBooSound.start();
    }

    public void PlayGhostDamagedSound()
    {
        soundEventGhostDamagedSound.start();
    }

    public void PlayGhostDeadSound()
    {
        soundEventGhostDeadSound.start();
    }

    public void PlayGhostLaughSound()
    {
        soundEventGhostLaughSound.start();
    }

    public void PlayGhostLaughALotSound()
    {
        soundEventGhostLaughALotSound.start();
    }

    public void PlayGhostStunnedSound()
    {
        soundEventGhostStunnedSound.start();
    }
    #endregion

    #region Scenario Methods
    public void PlayScenarioButtonSound()
    {
        soundEventScenarioButtonSound.start();
    }

    public void PlayScenarioLightbulbSpawnsSound()
    {
        soundEventScenarioLightbulbSpawnsSound.start();
    }

    public void PlayScenarioLightbulbFallsSound()
    {
        soundEventScenarioLightbulbFallsSound.start();
    }

    public void PlayScenarioDoorOpenSound()
    {
        soundEventScenarioDoorOpenSound.start();
    }

    public void PlayScenarioFireplaceSound()
    {
        soundEventScenarioFireplaceSound.start();
    }

    public void PlayScenarioFurnitureShakingSound()
    {
        soundEventScenarioFurnitureShakingSound.start();
    }

    public void PlayScenarioHealItemDroppedSound()
    {
        soundEventScenarioHealItemDroppedSound.start();
    }

    public void PlayScenarioHealItemFoundSound()
    {
        soundEventScenarioHealItemFoundSound.start();
    }

    public void PlayScenarioItemFoundSound()
    {
        soundEventScenarioItemFoundSound.start();
    }

    public void PlayScenarioItemLightParticlesSound()
    {
        soundEventScenarioItemLightParticlesSound.start();
    }

    public void PlayScenarioLockedDoorSound()
    {
        soundEventScenarioLockedDoorSound.start();
    }

    public void PlayScenarioSpecialItemFoundSound()
    {
        soundEventScenarioSpecialItemFoundSound.start();
    }

    public void PlayScenarioUnlockDoorSound()
    {
        soundEventScenarioUnlockDoorSound.start();
    }

    public void PlayScenarioLeverSound()
    {
        soundEventScenarioLeverSound.start();
    }

    public void PlayScenarioLibrarySound()
    {
        soundEventScenarioLibrarySound.start();
    }

    public void PlayScenarioPictureFallsSound()
    {
        soundEventScenarioPictureFallsSound.start();
    }
    #endregion

    #region Flashlight Methods
    public void PlayFlashlightChargingFlashSound()
    {
        soundEventFlashlightChargingFlashSound.start();
    }

    public void PlayFlashlightChargedFlashSound()
    {
        soundEventFlashlightChargedFlashSound.start();
    }

    public void PlayFlashlightFlashSound()
    {
        soundEventFlashlightFlashSound.start();
    }

    public void PlayFlashlightOffSound()
    {
        soundEventFlashlightOffSound.start();
    }

    public void PlayFlashlightOnSound()
    {
        soundEventFlashlightOnSound.start();
    }
    #endregion

    #region Menu Methods
    public void PlayMenuHUDMovementSound()
    {
        soundEventMenuHUDMovementSound.start();
    }

    public void PlayMenuHUDSelectSound()
    {
        soundEventMenuHUDSelectSound.start();
    }

    public void PlayMenuHUDSelectBackSound()
    {
        soundEventMenuHUDSelectBackSound.start();
    }
    #endregion

    #region Music Methods
    public void PlayHouseMusic()
    {
        soundEventHouseMusic.start();
    }

    public void PlayCombatMusic()
    {
        soundEventCombatMusic.start();
    }

    public void PlayGameOverMusic()
    {
        soundEventGameOverMusic.start();
    }

    public void PlayVictoryMusic()
    {
        soundEventVictoryMusic.start();
    }
    #endregion

    /*public void PlayThisSound(string selectedSound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(selectedSound, GetComponent<Transform>().position);
    }

    public void PlayScaredSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Character_Scared", GetComponent<Transform>().position);
        //typical playOneShot. We play an event (3D) at the position of the object that has the script; It plays the sound once and that's it

        //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Character_Damaged", this.gameObject);
        //this needs the location of a gameObject. It checks every frame the location of the GO
    }*/
    #endregion

    #region Stop Music Methods

    public void StopAmbientRainSound()
    {
        
        //soundEventHouseMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventAmbientRainSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopScenarioFireplaceSound()
    {
        //soundEventHouseMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventScenarioFireplaceSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopScenarioItemLightParticlesSound()
    {
        //soundEventHouseMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventScenarioItemLightParticlesSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopFlashlightChargedFlashSound()
    {
        //soundEventHouseMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventFlashlightChargedFlashSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopHouseMusic()
    {
        //soundEventHouseMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventHouseMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopCombatMusic()
    {
        //soundEventCombatMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventCombatMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopGameOverMusic()
    {
        //soundEventCombatMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventCombatMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopVictoryMusic()
    {
        //soundEventCombatMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        soundEventVictoryMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    #endregion

    #region Pause Sound Methods
    public void PauseAllSoundsButMusic()
    {
        //Ambient
        soundEventAmbientRainSound.setPaused(true);
        soundEventAmbientThunderSound.setPaused(true);

        //Character
        soundEventCharacterDamagedSound.setPaused(true);
        soundEventCharacterDeathSound.setPaused(true);
        soundEventCharacterScaredSound.setPaused(true);
        soundEventCharacterShakingHeadSound.setPaused(true);
        soundEventCharacterStepsCarpetSound.setPaused(true);
        soundEventCharacterStepsMarbleSound.setPaused(true);
        soundEventCharacterLowHealthSound.setPaused(true);

        //Ghosts
        soundEventGhostAttackSound.setPaused(true);
        soundEventGhostBooSound.setPaused(true);
        soundEventGhostDamagedSound.setPaused(true);
        soundEventGhostDeadSound.setPaused(true);
        soundEventGhostLaughSound.setPaused(true);
        soundEventGhostLaughALotSound.setPaused(true);
        soundEventGhostStunnedSound.setPaused(true);

        //Scenario
        soundEventScenarioButtonSound.setPaused(true);
        soundEventScenarioLightbulbSpawnsSound.setPaused(true);
        soundEventScenarioLightbulbFallsSound.setPaused(true);
        soundEventScenarioDoorOpenSound.setPaused(true);
        soundEventScenarioFireplaceSound.setPaused(true);
        soundEventScenarioFurnitureShakingSound.setPaused(true);
        soundEventScenarioHealItemDroppedSound.setPaused(true);
        soundEventScenarioHealItemFoundSound.setPaused(true);
        soundEventScenarioItemFoundSound.setPaused(true);
        soundEventScenarioItemLightParticlesSound.setPaused(true);
        soundEventScenarioLockedDoorSound.setPaused(true);
        soundEventScenarioSpecialItemFoundSound.setPaused(true);
        soundEventScenarioUnlockDoorSound.setPaused(true);
        soundEventScenarioLeverSound.setPaused(true);
        soundEventScenarioLibrarySound.setPaused(true);
        soundEventScenarioPictureFallsSound.setPaused(true);

        //Flashlight
        soundEventFlashlightChargingFlashSound.setPaused(true);
        soundEventFlashlightChargedFlashSound.setPaused(true);
        soundEventFlashlightFlashSound.setPaused(true);
        soundEventFlashlightOffSound.setPaused(true);
        soundEventFlashlightOnSound.setPaused(true);
    }

    public void ResumeAllSoundsButMusic()
    {
        //Ambient
        soundEventAmbientRainSound.setPaused(false);
        soundEventAmbientThunderSound.setPaused(false);

        //Character
        soundEventCharacterDamagedSound.setPaused(false);
        soundEventCharacterDeathSound.setPaused(false);
        soundEventCharacterScaredSound.setPaused(false);
        soundEventCharacterShakingHeadSound.setPaused(false);
        soundEventCharacterStepsCarpetSound.setPaused(false);
        soundEventCharacterStepsMarbleSound.setPaused(false);
        soundEventCharacterLowHealthSound.setPaused(false);

        //Ghosts
        soundEventGhostAttackSound.setPaused(false);
        soundEventGhostBooSound.setPaused(false);
        soundEventGhostDamagedSound.setPaused(false);
        soundEventGhostDeadSound.setPaused(false);
        soundEventGhostLaughSound.setPaused(false);
        soundEventGhostLaughALotSound.setPaused(false);
        soundEventGhostStunnedSound.setPaused(false);

        //Scenario
        soundEventScenarioButtonSound.setPaused(false);
        soundEventScenarioLightbulbSpawnsSound.setPaused(false);
        soundEventScenarioLightbulbFallsSound.setPaused(false);
        soundEventScenarioDoorOpenSound.setPaused(false);
        soundEventScenarioFireplaceSound.setPaused(false);
        soundEventScenarioFurnitureShakingSound.setPaused(false);
        soundEventScenarioHealItemDroppedSound.setPaused(false);
        soundEventScenarioHealItemFoundSound.setPaused(false);
        soundEventScenarioItemFoundSound.setPaused(false);
        soundEventScenarioItemLightParticlesSound.setPaused(false);
        soundEventScenarioLockedDoorSound.setPaused(false);
        soundEventScenarioSpecialItemFoundSound.setPaused(false);
        soundEventScenarioUnlockDoorSound.setPaused(false);
        soundEventScenarioLeverSound.setPaused(false);
        soundEventScenarioLibrarySound.setPaused(false);
        soundEventScenarioPictureFallsSound.setPaused(false);

        //Flashlight
        soundEventFlashlightChargingFlashSound.setPaused(false);
        soundEventFlashlightChargedFlashSound.setPaused(false);
        soundEventFlashlightFlashSound.setPaused(false);
        soundEventFlashlightOffSound.setPaused(false);
        soundEventFlashlightOnSound.setPaused(false);
    }

    public void PauseAllSoundsAndMusic()
    {
        FMODUnity.RuntimeManager.PauseAllEvents(true);
    }

    /*public void PauseAllSoundsAndMusic2()
    {
        FMODUnity.RuntimeManager.PauseAllEvents(true);
        soundEventHouseMusic.setPaused(false);
    }*/

    public void ResumeAllSoundsAndMusic()
    {
        FMODUnity.RuntimeManager.PauseAllEvents(false);
    }
    #endregion

    #region Former Sound Getters Methods
    public AudioClip GetSoundByRequest(SoundRequest sr)
    {
        switch (sr)
        {
            case SoundRequest.P_Knock:
                return GetPlayerKnockSound();
            case SoundRequest.P_OpenDoor:
                return GetPlayerOpenDoorSound();
            case SoundRequest.P_ButtonPush:
                return GetPlayerButtonPushSound();
            case SoundRequest.E_Cry:
                return GetEnemyCrySound();
        }

        return null;
    }

    //Player Sounds
    private AudioClip GetPlayerKnockSound()
    {
        return knockSounds[Random.Range(0, knockSounds.Count)];
    }

    private AudioClip GetPlayerOpenDoorSound()
    {
        return openDoorSounds[Random.Range(0, openDoorSounds.Count)];
    }

    private AudioClip GetPlayerButtonPushSound()
    {
        return buttonPushSounds[Random.Range(0, buttonPushSounds.Count)];
    }

    //Enemy Sounds
    private AudioClip GetEnemyCrySound()
    {
        return enemyCrySound[Random.Range(0, enemyCrySound.Count)];
    }
    
    //Object Sounds
    #endregion

}
