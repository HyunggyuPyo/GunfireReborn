using UnityEngine;

[CreateAssetMenu(fileName ="Monster", menuName = "SO/Add Monster")]
public class MonsterSO : ScriptableObject
{
    public GameObject monsterPrefab;
    public string enemyName;
    public int maxHealth;
    public int maxShield;
    public float speed;
    public int damage;

    public float uiHeight;
    public float detection;
}