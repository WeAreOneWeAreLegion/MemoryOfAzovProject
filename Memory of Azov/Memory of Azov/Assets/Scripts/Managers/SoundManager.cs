using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager> {

    public enum SoundRequest { P_Knock, P_OpenDoor, P_ButtonPush, E_Cry }
    public enum SoundRequestAmbient { A_Rain, A_Thunder }
    public enum SoundRequestPlayer { P_Damaged, P_Death, P_Scared, P_ShakingHead, P_StepsMarble, P_StepsCarpet }
    public enum SoundRequestGhost { G_Attack, G_Boo, G_Damaged, G_Dead, G_Laugh, G_LaughALot, G_Stunned }
    public enum SoundRequestScenario { S_Button, S_DoorClose, S_DoorKnob, S_DoorOpen, S_Fireplace, S_HealthItemDropped,
        S_HealthItemFound, S_ItemFound, S_ItemParticles, S_LockedDoor, S_SpecialItemFound, S_UnlockDoor }
    public enum SoundRequestFlashlight { F_ChargingFlash, F_Noise, F_On, F_Off }
    public enum SoundRequestMenu { M_Movement, M_Sound }

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
    public string scenarioDoorCloseSound;
    FMOD.Studio.EventInstance soundEventScenarioDoorCloseSound;

    [FMODUnity.EventRef]
    public string scenarioDoorKnobSound;
    FMOD.Studio.EventInstance soundEventScenarioDoorKnobSound;

    [FMODUnity.EventRef]
    public string scenarioDoorOpenSound;
    FMOD.Studio.EventInstance soundEventScenarioDoorOpenSound;

    [FMODUnity.EventRef]
    public string scenarioFireplaceSound;
    FMOD.Studio.EventInstance soundEventScenarioFireplaceSound;

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

    [Header("Flashlight Sounds")]
    [FMODUnity.EventRef]
    public string flashlightChargingFlashSound;
    FMOD.Studio.EventInstance soundEventFlashlightChargingFlashSound;

    [FMODUnity.EventRef]
    public string flashlightNoiseSound;
    FMOD.Studio.EventInstance soundEventFlashlightNoiseSound;

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
    #endregion

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
        soundEventScenarioDoorCloseSound = FMODUnity.RuntimeManager.CreateInstance(scenarioDoorCloseSound);
        soundEventScenarioDoorKnobSound = FMODUnity.RuntimeManager.CreateInstance(scenarioDoorKnobSound);
        soundEventScenarioDoorOpenSound = FMODUnity.RuntimeManager.CreateInstance(scenarioDoorOpenSound);
        soundEventScenarioFireplaceSound = FMODUnity.RuntimeManager.CreateInstance(scenarioFireplaceSound);
        soundEventScenarioHealItemDroppedSound = FMODUnity.RuntimeManager.CreateInstance(scenarioHealItemDroppedSound);
        soundEventScenarioHealItemFoundSound = FMODUnity.RuntimeManager.CreateInstance(scenarioHealItemFoundSound);
        soundEventScenarioItemFoundSound = FMODUnity.RuntimeManager.CreateInstance(scenarioItemFoundSound);
        soundEventScenarioItemLightParticlesSound = FMODUnity.RuntimeManager.CreateInstance(scenarioItemLightParticlesSound);
        soundEventScenarioLockedDoorSound = FMODUnity.RuntimeManager.CreateInstance(scenarioLockedDoorSound);
        soundEventScenarioSpecialItemFoundSound = FMODUnity.RuntimeManager.CreateInstance(scenarioSpecialItemFoundSound);
        soundEventScenarioUnlockDoorSound = FMODUnity.RuntimeManager.CreateInstance(scenarioUnlockDoorSound);

        //Flashlight
        soundEventFlashlightChargingFlashSound = FMODUnity.RuntimeManager.CreateInstance(flashlightChargingFlashSound);
        soundEventFlashlightNoiseSound = FMODUnity.RuntimeManager.CreateInstance(flashlightNoiseSound);
        soundEventFlashlightOffSound = FMODUnity.RuntimeManager.CreateInstance(flashlightOffSound);
        soundEventFlashlightOnSound = FMODUnity.RuntimeManager.CreateInstance(flashlightOnSound);

        //Menu
        soundEventMenuHUDMovementSound = FMODUnity.RuntimeManager.CreateInstance(menuHUDMovementSound);
        soundEventMenuHUDSelectSound = FMODUnity.RuntimeManager.CreateInstance(menuHUDSelectSound);
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
        }
    }

    public void GhostSoundEnum(SoundRequestGhost srg, Transform target)
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
            case SoundRequestScenario.S_DoorClose:
                PlayScenarioDoorCloseSound();
                break;
            case SoundRequestScenario.S_DoorKnob:
                PlayScenarioDoorKnobSound();
                break;
            case SoundRequestScenario.S_DoorOpen:
                PlayScenarioDoorOpenSound();
                break;
            case SoundRequestScenario.S_Fireplace:
                PlayScenarioFireplaceSound();
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
            case SoundRequestScenario.S_LockedDoor:
                PlayScenarioLockedDoorSound();
                break;
            case SoundRequestScenario.S_SpecialItemFound:
                PlayScenarioSpecialItemFoundSound();
                break;
            case SoundRequestScenario.S_UnlockDoor:
                PlayScenarioUnlockDoorSound();
                break;
        }
    }

    public void FlashlightSoundEnum(SoundRequestFlashlight srf, Transform target)
    {
        switch (srf)
        {
            case SoundRequestFlashlight.F_ChargingFlash:
                PlayFlashlightChargingFlashSound();
                break;
            case SoundRequestFlashlight.F_Noise:
                PlayFlashlightNoiseSound();
                break;
            case SoundRequestFlashlight.F_Off:
                PlayFlashlightOffSound();
                break;
            case SoundRequestFlashlight.F_On:
                PlayFlashlightOnSound();
                break;
        }
    }

    public void MenuSoundEnum(SoundRequestMenu srm, Transform target)
    {
        switch (srm)
        {
            case SoundRequestMenu.M_Movement:
                PlayMenuHUDMovementSound();
                break;
            case SoundRequestMenu.M_Sound:
                PlayMenuHUDSelectSound();
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
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioDoorCloseSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioDoorKnobSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioDoorOpenSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioFireplaceSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioHealItemDroppedSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioHealItemFoundSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioItemFoundSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioItemLightParticlesSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioLockedDoorSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioSpecialItemFoundSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventScenarioUnlockDoorSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateFlashlightSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightChargingFlashSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightNoiseSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightOffSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventFlashlightOnSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void UpdateMenuSounds()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventMenuHUDMovementSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEventMenuHUDSelectSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
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

    public void PlayScenarioDoorCloseSound()
    {
        soundEventScenarioDoorCloseSound.start();
    }

    public void PlayScenarioDoorKnobSound()
    {
        soundEventScenarioDoorKnobSound.start();
    }

    public void PlayScenarioDoorOpenSound()
    {
        soundEventScenarioDoorOpenSound.start();
    }

    public void PlayScenarioFireplaceSound()
    {
        soundEventScenarioFireplaceSound.start();
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
    #endregion

    #region Flashlight Methods
    public void PlayFlashlightChargingFlashSound()
    {
        soundEventFlashlightChargingFlashSound.start();
    }

    public void PlayFlashlightNoiseSound()
    {
        soundEventFlashlightNoiseSound.start();
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
