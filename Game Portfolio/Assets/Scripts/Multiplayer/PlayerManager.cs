using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;
	GameObject controller;

	[SerializeField] GameObject ghostPrefab;
	GameObject ghostGO;

	[SerializeField] private float respawnDelay;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if (PV.IsMine)
		{
			CreateController();
		}
	}

	void CreateController()
	{
		Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
		controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
		controller.name = PV.Owner.NickName;
	}

	public void Die(Transform deathPos)
	{
		PhotonNetwork.Destroy(controller);

		ghostGO = Instantiate(ghostPrefab, deathPos.position, deathPos.rotation);

		StartCoroutine(RespawnDelay());
	}

	IEnumerator RespawnDelay()
    {
		yield return new WaitForSeconds(respawnDelay);

		Destroy(ghostGO);
		CreateController();
	}
}
