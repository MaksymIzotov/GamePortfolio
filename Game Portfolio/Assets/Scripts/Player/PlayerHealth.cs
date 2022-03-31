using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPun
{
	PhotonView PV;
	PlayerManager playerManager;


	private int currentHealth;
    private void Start()
    {
		PV = GetComponent<PhotonView>();

		currentHealth = 100;
		UIUpdater.Instance.UpdateHealthText(currentHealth);
		playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
	}


    public void TakeDamage(int damage)
	{
		PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
	}

	[PunRPC]
	void RPC_TakeDamage(int damage)
	{
		if (!PV.IsMine)
			return;

		currentHealth -= damage;

		UIUpdater.Instance.UpdateHealthText(currentHealth);

		if (currentHealth <= 0)
			Die();
	}

	void Die()
	{
		playerManager.Die(transform);
	}
}
