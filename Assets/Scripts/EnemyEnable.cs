using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyEnable : MonoBehaviour {

	void Update()
	{
		IEnumerable<GameObject> enemies;
        enemies = Resources.FindObjectsOfTypeAll<GameObject>().Where(g => g.CompareTag("Enemy"));
        foreach (GameObject enemy in enemies) {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(enemy.transform.position);
            if (point.x < -1 || point.x < 2 || point.y < -1 && point.y > 2) {
                enemy.SetActive(true);
            } else {
                enemy.SetActive(true);
			}
        }
	}
}
