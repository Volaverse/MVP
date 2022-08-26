using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using Photon.Pun;
using agora_gaming_rtc;
using agora_utilities;

public class AgoraController : MonoBehaviourPun
{
    int mode = -1;
    private static string appId = "58f8a2d679474f58a02a6a91642d35c4";
    public IRtcEngine mRtcEngine;
    public uint mRemotePeer;
    private bool audioMuted = false;
    private bool videoMuted = false;
    public GameObject videoPanel;
    public Button audioButton;
    public Button videoButton;
    public Button leaveButton;
    string[] tid;

    GameObject whiteboard_;

    public GameObject map_handler;

    void Start()
    {
        Debug.Log("Started baby!");
        tid = new string[120];
    }
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("E pressed");
            GameObject player = GameObject.Find("localPlayer");
            if (player == null)
            {
                Debug.LogError("local player not found!");
            }
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(player.transform.position + new Vector3(0, 1, 0), new Vector3(0, 1, 0), out hitInfo);
            if (hit)
            {
                //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.name == "Classic_Ceiling_01_snaps001")
                {
                    GameObject ceiling = hitInfo.transform.parent.gameObject;
                    GameObject room = ceiling.transform.parent.gameObject;
                    Debug.Log("Hit " + room.name);
                    int index = int.Parse(room.name.Substring(5, room.name.LastIndexOf('_') - 5));
                    bool isOwner = map_handler.GetComponent<Maphandler>().isOwner(index);
                    whiteboard_ = room.transform.Find("Whiteboard").gameObject;
                    if (whiteboard_ == null)
                    {
                        Debug.LogError("Whiteboard not found!");
                        return;
                    }
                    if (mRtcEngine == null)
                    {
                        LoadEngine();
                    }
                    if (isOwner)
                    {
                        Debug.Log("Joining as teacher");
                        joinChannel("channel123", 0);
                    }
                    else
                    {
                        Debug.Log("Joining as student");
                        joinChannel("channel123", 1);
                    }
                }
            }
        }
    }
    public void LoadEngine()
    {
        Debug.Log("Loading Engine");
        mRtcEngine = IRtcEngine.GetEngine(appId);
        //mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_LIVE_BROADCASTING);
        //mRtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        mRtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess2;
        mRtcEngine.OnUserJoined += OnUserJoined2;
        mRtcEngine.OnUserOffline += OnUserOffline2;
    }
    public void joinChannel(string channelName, int _mode)
    {
        mode = _mode;
        Debug.Log("Joining Channel: " + channelName);
        if (mRtcEngine == null)
        {
            Debug.Log("Engine needs to be initialized before joining a channel");
            return;
        }
        videoPanel.SetActive(true);
        if (mode == 0)
        {
            videoButton.interactable = true;
            mRtcEngine.EnableVideo();
            //mRtcEngine.EnableLocalVideo(false);
        }
        else if (mode == 1)
        {
            videoButton.interactable = true;
            //mRtcEngine.EnableLocalVideo(false);
            //mRtcEngine.MuteLocalVideoStream(true);
            mRtcEngine.EnableVideo();
        }
        else
        {
            Debug.LogError("Ajeeb mode");
        }
        mRtcEngine.JoinChannel(channelName, null, 0);
        //mRtcEngine.EnableLocalVideo(false);
        //mRtcEngine.EnableVideo();
    }

    public void leaveChannel()
    {
        Debug.Log("leaving channel.");
        if (mRtcEngine == null)
        {
            Debug.Log("Engine needs to be initialised before leaving channel");
            return;
        }
        mRtcEngine.LeaveChannel();
        videoPanel.SetActive(false);
    }

    public void unloadEngine()
    {
        Debug.Log("Unloading Agora Engine.");
        if (mRtcEngine != null)
        {
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
            VideoSurface videoSurface = whiteboard_.GetComponent<VideoSurface>();
            if (videoSurface != null)
            {
                Destroy(videoSurface);
                videoSurface = null;
            }
            RawImage rawImage = whiteboard_.GetComponent<RawImage>();
            if (rawImage != null)
            {
                Destroy(rawImage);
                rawImage = null;
            }
    }

    public void onLeaveButtonPressed()
    {
        Debug.Log("Leave Button pressed");
        if (mRtcEngine != null)
        {
            leaveChannel();
            unloadEngine();
        }
    }

    public void onAudioButtonPressed()
    {
        if (audioMuted)
        {
            audioMuted = false;
            mRtcEngine.EnableLocalAudio(true);
            mRtcEngine.MuteLocalAudioStream(false);
            audioButton.GetComponentInChildren<Text>().text = "Mute Audio";
        }
        else
        {
            audioMuted = true;
            mRtcEngine.EnableLocalAudio(false);
            mRtcEngine.MuteLocalAudioStream(true);
            audioButton.GetComponentInChildren<Text>().text = "Unmute Audio";
        }
    }

    public void onVideoButtonPressed()
    {
        if (videoMuted)
        {
            videoMuted = false;
            mRtcEngine.EnableLocalVideo(true);
            videoButton.GetComponentInChildren<Text>().text = "Mute Video";
        }
        else
        {
            videoMuted = true;
            mRtcEngine.EnableLocalVideo(false);
            videoButton.GetComponentInChildren<Text>().text = "Unmute Video";
        }
    }

    private void OnJoinChannelSuccess2(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Successfully joined channel: " + channelName + " with id: " + uid);
        
        if (mode == 0)
        {
            ExitGames.Client.Photon.Hashtable a = PhotonNetwork.CurrentRoom.CustomProperties;
            Debug.Log(a);
            a[whiteboard_.transform.parent.name] = uid.ToString();
            Debug.Log(a);
            PhotonNetwork.CurrentRoom.SetCustomProperties(a);
            whiteboard_.AddComponent<RawImage>();
            VideoSurface videoSurface = whiteboard_.AddComponent<VideoSurface>();
            if (videoSurface == null)
            {
                Debug.LogError("video surface not created!");
            }
            videoSurface.SetForUser(0);
            videoSurface.SetEnable(true);
            //mRtcEngine.EnableLocalVideo(false);
        }
        else if (mode == 1)
        {
            string _tid = (string) PhotonNetwork.CurrentRoom.CustomProperties[whiteboard_.transform.parent.name];
            Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties);
            uint tid = uint.Parse(_tid);
            whiteboard_.AddComponent<RawImage>();
            VideoSurface videoSurface = whiteboard_.AddComponent<VideoSurface>();
            if (videoSurface == null)
            {
                Debug.Log("video surface not created!");
            }
            Debug.Log("!!!!!!!!:" + tid);
            videoSurface.SetForUser(tid);
            videoSurface.SetEnable(true);
            //mRtcEngine.EnableLocalVideo(false);
        }
        else
        {
            Debug.Log("Mode not set but entered meet!!!");
        }
    }
    private void OnUserJoined2(uint uid, int elapsed)
    {
        Debug.Log("New user has joined channel with id: " + uid);
    }

    private void OnUserOffline2(uint uid, USER_OFFLINE_REASON reason)
    {
        Debug.Log("User with id: " + uid + " has left the channel");
    }
    void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        if (mRtcEngine != null)
        {
            mRtcEngine.LeaveChannel();
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
    }
}
