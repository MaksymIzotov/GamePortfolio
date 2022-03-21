using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    private Vector3 currentRot;
    private Vector3 targetRot;

    public float snapiness;
    public float returnSpeed;

    void Update()
    {
        RecoilUpdate();
    }

    private void RecoilUpdate()
    {
        targetRot = Vector3.Lerp(targetRot, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRot = Vector3.Slerp(currentRot, targetRot, snapiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRot);
    }

    public void AddRecoil(float recoilX, float recoilY, float recoilZ) => targetRot += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
}
