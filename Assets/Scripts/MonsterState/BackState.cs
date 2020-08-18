using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    Vector2 targetPosition;

    [SerializeField]
    private float followDistance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition = new Vector2(enemy.home.x, enemyTransform.position.y);
        if (Vector2.Distance(targetPosition, enemyTransform.position) < 0.1f || Vector2.Distance(enemyTransform.position, enemy.player.position) <= followDistance)
        {
            animator.SetBool("isBack", false);
        }
        else
        {
            enemy.DirectionEnemy(enemy.home.x, enemyTransform.position.x);
            //enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.home, Time.deltaTime * enemy.speed);
         
            
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, targetPosition, Time.deltaTime * enemy.speed);
        }
           
    }
}
