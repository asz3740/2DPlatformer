using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    Vector2 playerPos;
    Vector2 targetPosition;

    [SerializeField]
    private float attackDistance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = enemy.player.position;
        targetPosition = new Vector2(playerPos.x, enemyTransform.position.y);

        if (enemy.atkDelay <= 0)
        { 
            animator.SetTrigger("Attack");
            if (enemy.isFly)
            {
                for (int index = 0; index < 4; index++)
                {
                    GameObject bullet = Instantiate(enemy.RangedAtkPrefab, enemy.transform.position, enemy.transform.rotation);
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / 4),
                        Mathf.Sin(Mathf.PI * 2 * index / 4));
                    rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);

                    Vector3 rotVec = Vector3.forward * 360 * index / 4 + Vector3.forward * 90;
                    bullet.transform.Rotate(rotVec);
                    //Destroy(bullet, 3);
                }
            }
        }

        if (Vector2.Distance(targetPosition, enemyTransform.position) > attackDistance)
            animator.SetBool("isFollow", true);
        
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }
}
