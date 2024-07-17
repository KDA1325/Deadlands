using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDamege : MonoBehaviour
{
    public void Init(float num)
    {
        Managers.UI.ChangeUIText(GetComponentInChildren<Text>(), num);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 3);
        StartCoroutine("Despawn");
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(0.4f);

        ObjectHandler.Despawn(gameObject);
    }
}
