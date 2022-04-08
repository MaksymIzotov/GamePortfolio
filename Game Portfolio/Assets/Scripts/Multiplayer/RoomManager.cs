using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    #region Singleton Init
    public static RoomManager Instance;

	void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
    }
	#endregion

	public enum Gamemode
	{
		DEATHMATCH = 0,
		BR = 1
	}

	public Gamemode mode;
	private PhotonView pv;

    private void Start()
    {
		pv = GetComponent<PhotonView>();
    }

    public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (scene.buildIndex == 1) // We're in the game scene
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
		}
	}

	public void SyncGamemode_RPC()
	{
		pv.RPC("SyncGamemode", RpcTarget.All, Launcher.Instance.gamemodeDropdown.value);
	}

	[PunRPC]
	private void SyncGamemode(int value)
	{
		Launcher.Instance.gamemodeDropdown.value = value;
	}
}
