using System.Collections;
using UnityEngine;
/* Controls the Enemy AI */
public class DragonController : MonoBehaviour
{
	public float lookRad = 10f;// Detection range for player
	public float attkRad = 8f;
	public float moveSpeed = 3f;
	public float enemyLimitRange = 13.0f;
	Transform target; // Reference to the player
	Transform enemy;
	public GameObject Poison;
	public GameObject Bullet;//Drag in the Bullet Prefab from the Component Inspector.
	public GameObject Player;
	public GameObject TurnBase;
	DragonStat deadCheck;
	public float BulletSpeed = 2.0f;//Enter the Speed of the Bullet from the Component Inspector.

	void Start()
	{
		target = Player.transform;
		enemy = TurnBase.transform;
		Bullet.transform.gameObject.SetActive(false);
		Poison.transform.gameObject.SetActive(false);
		deadCheck = GetComponent<DragonStat>();
	}
	// Update is called once per frame
	void Update()
	{
		// Distance to the target
		float distance = Vector3.Distance(target.position, transform.position);
		float distance2 = Vector3.Distance(enemy.position, transform.position);
		// If inside the lookRadius
		if (distance <= lookRad && distance2 <= enemyLimitRange && !deadCheck.DestroyOnDeath)
		{
			// Move towards the target
			transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
			FaceTarget();
			Debug.Log(distance2);
			Poison.transform.gameObject.SetActive(true);
			// If within attacking distance
			if (distance <= attkRad)
			{
				//Attack();
				Bullet.transform.gameObject.SetActive(true);
			}
			else
			{
				Bullet.transform.gameObject.SetActive(false);
				Poison.transform.gameObject.SetActive(false);
			}
		}
		else if (distance > lookRad || distance2 >= enemyLimitRange)
		{
			transform.position = Vector3.MoveTowards(transform.position, enemy.position, Time.deltaTime * moveSpeed);
		}
		else if (deadCheck.DestroyOnDeath == true)
		{
			transform.position = enemy.position;
		}
	}
	public void MoveBack()
	{
		transform.position = enemy.position;
	}
	// Rotate to face the target
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
	// Show the lookRadius in editor
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRad);
		Gizmos.DrawWireSphere(transform.position, enemyLimitRange);
	}
	void Attack()
	{

	}
}