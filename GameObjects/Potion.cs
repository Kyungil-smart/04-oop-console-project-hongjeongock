

public class Potion : Item, IInteractable
{

    public Potion() => Init();
    
    private void Init()
    {
        Symbol = 'I';
    }

    public override void Use()
    {
        Inventory.Remove(this);
        Inventory = null;
        
        Debug.Log("포션 사용");
        // 효과~~~~
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddItem(this);
    }
}