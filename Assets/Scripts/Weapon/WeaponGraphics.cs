using UnityEngine;

public class WeaponGraphics : MonoBehaviour
{
    [Header("Information")]
    public string weaponName;
    public Sprite weaponIcon;
    
    
    [Header("Internal References")]
    public ParticleSystem muzzleFlash;
    public GameObject weaponGFX;
    
    
    [Header("Crosshair")]
    public CrosshairData crosshairDataDefault; //一般情况下的准心
    public CrosshairData crosshairDataHit; //击中后的准心
    

    public void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }


    
    
}
