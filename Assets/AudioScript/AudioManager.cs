using UnityEngine;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

	public static AudioEventSystem.Dispatcher BGM_ES = new AudioEventSystem.Dispatcher();
	public static AudioEventSystem.Dispatcher SFX_ES = new AudioEventSystem.Dispatcher();
	public static AudioEventSystem.Dispatcher BTN_ES = new AudioEventSystem.Dispatcher();
    
    public AudioSourceSetting[] BGM_ASSets;
    public AudioSourceSetting[] SFX_ASSets;
    public AudioSourceSetting[] BTN_ASSets;

    public GameObject EmptyAudioSource;

    public List<GameObject> AudioObjInScene;

    [Range(0, 1)]
    public float BGM_FixValue = 0.5f;
    [Range(0, 1)]
    public float SFX_FixValue = 0.5f;

    float isfadeOutTime = 0.0f;

    void Awake()
    {
        inst = this;
    }
    // Use this for initialization
    void Start()
    {
        SFXRegister();
        BTNRegister();
    }

    public void DelAudioObj(GameObject _getObj)
    {
        AudioObjInScene.Remove(_getObj);
    }


    void SFXRegister()
    {
        for (int i = 0; i < SFX_ASSets.Length; ++i)
            SFXRegister(i);
    }
    void SFXRegister(int i)
    {
        SFX_ES.On(SFX_ASSets[i].name, delegate ()
        {
            GameObject Audio_Obj = Instantiate(EmptyAudioSource);
            PlayAudioBySetting(Audio_Obj, SFX_ASSets[i]);
            Audio_Obj.GetComponent<AudioSource>().Play();
            AudioObjInScene.Add(Audio_Obj);
        });
    }
    void BTNRegister()
    {
        for (int i = 0; i < BTN_ASSets.Length; ++i)
            BTNRegister(i);
    }
    void BTNRegister(int i)
    {
        BTN_ES.On(BTN_ASSets[i].name, delegate ()
        {
            GameObject Audio_Obj = Instantiate(EmptyAudioSource);
            PlayAudioBySetting(Audio_Obj, BTN_ASSets[i]);
            Audio_Obj.GetComponent<AudioSource>().Play();
            AudioObjInScene.Add(Audio_Obj);
        });
    }

    /// <summary>
    /// 音樂音效播放(依設定)
    /// </summary>
    /// <param name="objAudioSource"></param>
    /// <param name="_set"></param>
    /// <returns></returns>
    void PlayAudioBySetting(GameObject objAudioSource, AudioSourceSetting _set)
    {
        AudioSource audioInst = objAudioSource.GetComponent<AudioSource>();

        int clipLength = _set._Clip.AudioClip.Length;

        //是否隨機選擇音樂
        if (_set._Clip.isRandom)
        {
            //亂數
            //System.Random rdm;
            //rdm = new System.Random(Guid.NewGuid().GetHashCode());
            int rdm = UnityEngine.Random.Range(0, _set._Clip.AudioClip.Length);
            audioInst.clip = _set._Clip.AudioClip[rdm];
            //音樂結束刪除依長度
            audioInst.GetComponent<DestroyObj>().DestroyTime = _set._Clip.AudioClip[rdm].length;
        }
        else
        {
            audioInst.clip = _set._Clip.AudioClip[0];
            //音樂結束刪除依長度
            audioInst.GetComponent<DestroyObj>().DestroyTime = _set._Clip.AudioClip[0].length;
        }

        audioInst.playOnAwake = _set.PlayOnAwake;
        audioInst.loop = _set.Loop;
        audioInst.priority = _set.Priority;
        audioInst.volume = _set.Volume * SFX_FixValue;
        audioInst.pitch = _set._PitchSet.initPitch;
    }

    //FadeOut 
    void isFadeOut(AudioSource _get)
    {

        //現在音量
        float _vol;
        _vol = _get.volume;//isfadeOutTime

        //FadeOut 速率
        float lessRate;
        lessRate = _vol / isfadeOutTime * Time.deltaTime;

        //減低音量
        while (_vol > 0.01f)
        {
            _vol -= lessRate;
            if (_vol < 0)
                _vol = 0.0f;
            _get.volume = _vol;
        }
    }

    //設置全部音效音量
    public void SetVol_InSceneObj()
    {
        foreach (GameObject i in AudioObjInScene)
        {
            i.GetComponent<AudioSource>().volume *= SFX_FixValue;
        }
    }
    public void Control_BGM_Player()
    {
        //BGM_AS.volume = BGM_ReelVol * BGM_value;
    }

    public void deletAllSoundEffect() {
        foreach(GameObject i in AudioObjInScene)
        {
            Destroy(i);
        }
    }

    public class BGM_NAME
    {
        public const string BGM_Normal = "Normal";
        public const string BGM_FG = "FreeGame";
        public const string BGM_BG = "Bonus";
        public const string BGM_JP = "JackpotGame";
        public const string BGM_COUNTBOX = "BGM_COUNTBOX";
        public const string BGM_5CHOOSE1 = "BGM_5CHOOSE1";
    }
    public class BTN_NAME
    {
        public const string BTN_AUTO = "AUTO";
        public const string BTN_AUTO_PULLOUT = "AUTO_PULLOUT";
        public const string BTN_AUTO_SELECT = "AUTO_SELECT";
        public const string BTN_BET = "BET";
        public const string BTN_GOLD = "GOLD";
        public const string BTN_BUY = "Buy";
        public const string BTN_HIGHLOW_CHANGE = "HighLow_Change";
        public const string BTN_JP = "UI_JP";
        public const string BTN_Setup = "UI_Setup";
        public const string BTN_Spin = "UI_Spin";
        public const string BTN_Info_OPEN = "UI_Info_OPEN";
        public const string BTN_Info_CLOSE = "UI_Info_CLOSE";
        public const string BTN_DENOM = "BTN_DENOM";
        public const string BTN_STOP = "BTN_STOP";
        public const string BTN_FREESPINCLICK = "BTN_FREESPINCLICK";
        public const string BTN_LOCALIZATION = "BTN_LOCALIZATION";

    }
    public class VX_NAME
    {
        public const string SFX_STOPROLL = "STOPROLL";
        public const string SFX_LISTENSTART = "SFX_LISTENSTART";
        public const string SFX_LISTENEND = "SFX_LISTENEND";
        public const string SFX_ROUND = "SFX_ROUND";//"創建"FG的Round提示音效
        public const string SFX_CAT_SHOWUP = "SFX_CAT_SHOWUP";//0006 夢遊 長條百搭 提示
        public const string SFX_PAY_WILD_BIG = "SFX_PAY_WILD_BIG";//0006 夢遊 長條百搭 開啟
        public const string SFX_COUNT_PLUS = "SFX_COUNT_PLUS";//0006 夢遊 增加次數
        public const string SFX_COUNT_COLLECTION = "SFX_COUNT_COLLECTION";//0006 夢遊 結算次數
        public const string SFX_FOGPASS = "SFX_FOGPASS";
        public const string SFX_MONEYFLY = "SFX_MONEYFLY";
        public const string SFX_5_CHOOSE_1_INTRO = "SFX_5_CHOOSE_1_INTRO";
        public const string SFX_LOWPAY = "SFX_LOWPAY";
        public const string SFX_MIDPAY = "SFX_MIDPAY";
        public const string SFX_HIGHPAY = "SFX_HIGHPAY";
        public const string SFX_PAY_FG = "SFX_PAY_FG";
        public const string SFX_WILD = "SFX_WILD";
        public const string SFX_COIN_PASS = "SFX_COIN_PASS";
        public const string SFX_GUNSHOT = "SFX_GUNSHOT";

        //Bonus相關 (12~13)
        public const string SFX_PAY_BONUS = "SFX_PAY_BONUS";
        public const string SFX_BONUS_INTRO = "SFX_BONUS_INTRO";

        public const string SFX_SMALL_COUNT = "SFX_SMALL_COUNT"; //小結算
        public const string SFX_SMALL_COUNT_ONCE = "SFX_SMALL_COUNT_ONCE"; //小結算_單回

        //Bonus相關 (16~36)
        public const string SFX_Bonus_Win = "SFX_Bonus_Win";
        public const string SFX_Bonus_Fail = "SFX_Bonus_Fail";
        public const string SFX_Bonus_Show_All_Answer = "SFX_Bonus_Show_All_Answer";
        public const string SFX_Bonus_ScensePass = "SFX_Bonus_ScensePass";
        public const string SFX_Bonus_Scorpion = "SFX_Bonus_Scorpion";
        public const string SFX_Bonus_Bottle = "SFX_Bonus_Bottle";
        public const string SFX_Bonus_Bottle_shiny = "Bonus_Bottle_shiny";
        public const string SFX_Bonus_Scarabs = "SFX_Bonus_Scarabs";
        public const string SFX_Bonus_Scarabs_Success = "SFX_Bonus_Scarabs_Success";
        public const string SFX_Bonus_Scarabs_Fail = "SFX_Bonus_Scarabs_Fail";
        public const string SFX_Bonus_Torch = "SFX_Bonus_Torch";
        public const string SFX_Bonus_Troch_Success = "SFX_Bonus_Troch_Success";
        public const string SFX_Bonus_Troch_Fail = "SFX_Bonus_Troch_Fail";
        public const string SFX_Bonus_Torch_Scene_Success = "SFX_Bonus_Torch_Scene_Success";
        public const string SFX_Bonus_Torch_Scene_Fail = "SFX_Bonus_Torch_Scene_Fail";
        public const string SFX_Bonus_Coffin = "SFX_Bonus_Coffin";
        public const string SFX_Bonus_Coffin_Success = "SFX_Bonus_Coffin_Success";
        public const string SFX_Bonus_Coffin_Fail = "SFX_Bonus_Coffin_Fail";
        public const string SFX_Bonus_Complete = "SFX_Bonus_Complete";

        public const string SFX_Bonus_Intro_Click = "SFX_Bonus_Intro_Click";
        public const string SFX_Bonus_Scorpion_Success = "SFX_Bonus_Scorpion_Success";

        public const string SFX_5_CHOOSE_1_STOP = "SFX_5_CHOOSE_1_STOP";
        public const string SFX_5_CHOOSE_1_ROLLING = "SFX_5_CHOOSE_1_ROLLING";
        public const string SFX_5_CHOOSE_1_ROLLING_SLOW = "SFX_5_CHOOSE_1_ROLLING_SLOW";
    }


    [Serializable]
    public class AudioSourceSetting
    {
        public string name;
        //public AudioClip _AudioSource;
        public ClipSet _Clip = new ClipSet();
        public bool Mute = false;

        public bool PlayOnAwake = false;
        public bool Loop = false;
        [Range(0, 256)]
        public int Priority = 128;
        [Range(0.0f, 1.0f)]
        public float Volume = 1.0f;

        public PitchSet _PitchSet = new PitchSet();

        [Serializable]
        public class PitchSet
        {
            public bool isRandom = false;
            [Range(0.0f, 3.0f)]
            public float initPitch = 1.0f;
            [Range(1.0f, 3.0f)]
            public float max = 1.5f;
            [Range(0.0f, 1.0f)]
            public float min = 0.5f;
        }
        [Serializable]
        public class ClipSet
        {
            public bool isRandom = false;
            public AudioClip[] AudioClip;
            public float fadeInTime;
            public float fadeOutTime;
        }
    }
}
