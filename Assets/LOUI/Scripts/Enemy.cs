using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static LevelManager levelMan;

    public EnemyType enemyType;
    public ShotType color;
    public Transform tf;
    public Rigidbody rb;
    public MeshRenderer rend;
    public Collider col;
    public Material mat;
    public Material hurtMat;

    public int hp;
    int maxHp;

    bool canTakeDamage;

    [SerializeField] Material[] blueMats;
    [SerializeField] Material[] redMats;

    public AudioSource audioSource;
    public AudioClip[] killClips;

    private void Start()
    {
        if (levelMan == null)
            levelMan = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        //if(playerTf == null)
        //    playerTf = GameObject.FindGameObjectWithTag("Player").transform;

        maxHp = hp;
        canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12 && canTakeDamage) // Layer 12 is PlayerBullet
        {
            ShotType shotColor = other.GetComponent<Projectile>().shotType;

            if (enemyType == EnemyType.Rock || color == shotColor)
            {
                StartCoroutine(TakeDamage(1));
            }
            else if(enemyType == EnemyType.Color && color != shotColor)
            {
                StartCoroutine(TakeDamage(3));
            }
        }
        else if(other.gameObject.layer == 14)
        {
            Kill(0);
        }
    }

    public IEnumerator TakeDamage(int dmg)
    {
        //Debug.Log("Enemy Take Damage");

        canTakeDamage = false;
        
        //enemy hurt sound

        rend.material = hurtMat;

        yield return new WaitForSeconds(0.1f);

        if (enemyType == EnemyType.Color)
            SetColor(color);
        else
            rend.material = mat;

        hp -= dmg;

        if(hp <= 0)
            Kill(dmg > 1 ? 1 : 0);

        canTakeDamage = true;

    }

    public void SetColor(ShotType color)
    {
        this.color = color;

        switch (color)
        {
            case ShotType.Blue:
                rend.materials = blueMats;
                break;
            case ShotType.Red:
                rend.materials = redMats;
                break;
        }
    }

    public void Activate(Vector3 spawnPoint, Vector3 eulerRot, Vector3 initVelocity)
    {
        hp = maxHp;
        tf.position = spawnPoint;
        tf.rotation = Quaternion.Euler(eulerRot);
        rb.isKinematic = false;
        rb.velocity = initVelocity;
        col.enabled = true;
        rend.enabled = true;
    }

    public void Activate(Vector3 spawnPoint, Vector3 eulerRot, Vector3 initVelocity, ShotType color)
    {
        hp = maxHp;
        SetColor(color);

        tf.position = spawnPoint;
        tf.rotation = Quaternion.Euler(eulerRot);
        rb.isKinematic = false;
        rb.velocity = initVelocity;
        col.enabled = true;
        rend.enabled = true;
    }

    public void Kill(int drop)
    {
        int r = Random.Range(0, killClips.Length);

        audioSource.PlayOneShot(killClips[r]);

        col.enabled = false;
        rend.enabled = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        if (drop > 0)
            levelMan.SpawnPickup(transform.position);
    }
}
