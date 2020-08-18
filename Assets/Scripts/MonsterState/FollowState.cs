using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    Vector2 playerPos;
    Vector2 targetPosition;

    [SerializeField]
    private float followDistance;
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

        if (Vector2.Distance(targetPosition, enemyTransform.position) > followDistance)
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFollow", false);
        }
        else if (Vector2.Distance(targetPosition, enemyTransform.position) > attackDistance)
        {
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, targetPosition, Time.deltaTime * enemy.speed);
        }

        else
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFollow", false);
        }
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
        
    }

}
