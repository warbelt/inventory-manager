using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredientCommand : Command
{
    public List<GameObject> ingredients;

    public override void execute()
    {
        var item = ingredients[Random.Range(0, ingredients.Count)];

        var rx = Random.Range(-1.0f, -0.01f);
        var ry = Random.Range(-0.66f, 0.66f);

        GameObject go = Instantiate(item, transform.position + new Vector3(rx, ry, 0), Quaternion.identity);
    }
}
