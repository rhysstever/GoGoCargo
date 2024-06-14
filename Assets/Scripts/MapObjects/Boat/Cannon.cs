using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private int damage;

    private float currentAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        if(attackSpeed <= 0)
            attackSpeed = 1.0f;
        if(damage <= 0)
            damage = 1;
    }

    private void FixedUpdate()
    {
        if(CanFire())
        {
            currentAttackTimer += Time.deltaTime;

            if(gameObject.tag == "Player"
                && Input.GetKeyDown(KeyCode.Space)
                && currentAttackTimer >= attackSpeed)
                Fire();
        }
    }

    private bool CanFire()
    {
        return GameManager.instance.CurrentMenuState == MenuState.Sailing
            && PlayerManager.instance.CurrentPlayerState == PlayerState.Sailing;
    }

    private void Fire()
    {
        Vector3 positionOffset = gameObject.transform.right / 3.0f;
        Vector3 directionOffset = gameObject.transform.up / 8.0f;

        GameObject cannonBallLeft = NPCManager.instance.SpawnCannonball(
            gameObject.transform.position + new Vector3(0, 0.5f, 0) + -positionOffset,
            -gameObject.transform.right + directionOffset);
        GameObject cannonBallRight = NPCManager.instance.SpawnCannonball(
            gameObject.transform.position + new Vector3(0, 0.5f, 0) + positionOffset,
            gameObject.transform.right + directionOffset);
        cannonBallLeft.GetComponent<Cannonball>().source = gameObject;
        cannonBallRight.GetComponent<Cannonball>().source = gameObject;
        cannonBallLeft.GetComponent<Cannonball>().damage = damage;
        cannonBallRight.GetComponent<Cannonball>().damage = damage;

        currentAttackTimer = 0.0f;
    }
}
