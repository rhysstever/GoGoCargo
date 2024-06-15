using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    protected float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }
    [SerializeField]
    protected int damage;
    public int Damage { get { return damage; } }

    protected float currentAttackTimer;

    // Start is called before the first frame update
    protected void Start()
    {
        if(attackSpeed <= 0)
            attackSpeed = 1.0f;
        if(damage <= 0)
            damage = 1;
    }

    protected void FixedUpdate()
    {
        if(CanFire())
        {
            currentAttackTimer += Time.deltaTime;

            if(currentAttackTimer >= attackSpeed)
            {
                if(gameObject.tag == "Player")
                {
                    if(Input.GetKeyDown(KeyCode.Q))
                        Fire(false);
                    else if(Input.GetKeyDown(KeyCode.E))
                        Fire(true);
                } 
                else if(gameObject.tag == "Pirate")
                {
                    
                }
            }
        }
    }

    protected bool CanFire()
    {
        return GameManager.instance.CurrentMenuState == MenuState.Sailing
            && PlayerManager.instance.CurrentPlayerState == PlayerState.Sailing;
    }

    protected void Fire(bool isFiringRight)
    {
        Vector3 positionOffset = gameObject.transform.right / 3.0f;
        Vector3 directionOffset = gameObject.transform.up / 8.0f;

        Vector3 position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
        if(isFiringRight)
            position += positionOffset;
        else
            position -= positionOffset;

        Vector3 direction = directionOffset;
        if(isFiringRight)
            direction += gameObject.transform.right;
        else
            direction -= gameObject.transform.right;

        GameObject cannonBall = NPCManager.instance.SpawnCannonball(position, direction);
        cannonBall.GetComponent<Cannonball>().source = gameObject;
        cannonBall.GetComponent<Cannonball>().damage = damage;

        currentAttackTimer = 0.0f;
    }
}
