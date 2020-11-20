//Robban
using UnityEngine;

public class BlockType : MonoBehaviour
{
    public int playerteam = 1;
    public string category = "Red";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BlockManager.updateLinks = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        setColorByCategory();
        BlockManager.AddBlockToList(gameObject);
    }

    public void setCatagoryByNumber(int n)
    {
        if(n == 1)
        {
            category = "Green";
        }else if(n == 2)
        {
            category = "Blue";
        }
        else
        {
            category = "Red";
        }

        setColorByCategory();
    }
    public void setColorByCategory()
    {
        if (category == "Green")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (category == "Blue")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (category == "Yellow")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            category = "Red";
        }
    }

    public void Killme()
    {
        BlockManager.RemoveBlockFromList(gameObject);
        Destroy(gameObject);
    }
}
