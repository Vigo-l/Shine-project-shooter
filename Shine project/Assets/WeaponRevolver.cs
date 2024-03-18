using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponRevolver : MonoBehaviour
{
    public int damage;

    public Camera cam;
    [SerializeField]
    private GameObject _BloodPrefab;

    public float fireRate;

    public int mag = 5; 

    public int ammo = 15;
    public int magammo = 15;

    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    public Animation animationGun;
    public AnimationClip reloadGun;
    public AnimationClip emptyReloadGun;

    public GameObject muzzle;


    public GameObject hitVFX;
    public GameObject playerHitVFX;
    public GameObject shotVFX;
    public GameObject smokeVFX;

    [Space]
    public AudioSource Revolver;
    public AudioClip Click;
    public AudioClip Fire;


    private float nextFire;
    [Header ("Recoil")]
    [Range(0, 2)]
    public float recoverPercent = 0.7f;
    [Space]
    public float recoilUp = 1f;
    public float recoilBack = 0f;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;

    private bool recoiling;
    public bool recovering;
    private bool shooting;

    private float recoilLenght;
    private float recoverLenght;

    private void Start()
    {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magammo;


        originalPosition = transform.localPosition;

        recoilLenght = 0;
        recoverLenght = 1 / fireRate * recoverPercent;
    }

    private void Update()
    {
        if (nextFire >0)
        {
            nextFire -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && nextFire <= 0 && ammo > 0 && animationGun.isPlaying == false && shooting == false)
        {
            nextFire = 1 / fireRate;

            ammo--;

            StartCoroutine(Shoot());
            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magammo;
        }

        if(Input.GetKeyDown(KeyCode.R) && animationGun.isPlaying == false && ammo != 15 && shooting == false)
        {
            Reload();
        }

        if(ammo <= 0 && mag > 0 && shooting == false)
        {
            EmptyReload();
        }

        if (recoiling)
        {
            Recoil();
        }
        if (recovering)
        {
            Recovering();
        }
    }

     void Reload()
    {
        if (mag > 0)
        {
            animationGun.Play(reloadGun.name);
            mag--;

            ammo = magammo;
        }
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magammo;

    }

    void EmptyReload()
    {
        if (animationGun.isPlaying == false)
        {
            animationGun.Play(emptyReloadGun.name);
        mag--;

        ammo = magammo;
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magammo;
        }
       
    }

    IEnumerator Shoot()
    {
        shooting = true; 
        Debug.Log("Click");
        yield return new WaitForSeconds(1);
        recoiling = true;
        recovering = false;
        Debug.Log("shot gun");
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        PhotonNetwork.Instantiate(shotVFX.name, muzzle.transform.position, muzzle.transform.rotation);
        PhotonNetwork.Instantiate(smokeVFX.name, muzzle.transform.position, muzzle.transform.rotation);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            if (hit.transform.gameObject.GetComponent<Health>()) 
            {
                GameObject bloodPrefabInstance = Instantiate(_BloodPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                bloodPrefabInstance.transform.parent = hit.transform;
                PhotonNetwork.Instantiate(playerHitVFX.name, hit.point, Quaternion.identity);
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
                Debug.Log("hit!");

            }
            else
            {
                PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            }
        }
        shooting = false;
    }


    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originalPosition.x, originalPosition.y + recoilUp, originalPosition.z - recoilBack);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLenght);

        if (transform.localPosition == finalPosition) 
        {
            recoiling = false;
            recovering = true;
        }
    }
    void Recovering()
    {
        Vector3 finalPosition = originalPosition;


        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLenght);

        if (transform.localPosition == finalPosition)
        {
            recovering = false;
            recoiling = false;
        }
    }
}
