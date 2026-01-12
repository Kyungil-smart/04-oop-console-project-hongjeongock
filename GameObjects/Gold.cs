

public class Gold : Item, IInteractable
{

    public Gold() => Init();

    public int _gold {  get; set; }
    
    private void Init()
    {
        Symbol = 'G';
    }

    public override void Use()
    {
        Inventory.Remove(this);
        Inventory = null;

        Debug.Log("골드 사용");
    }

    public void Interact(PlayerCharacter player)
    {
        player.AddItem(this);
    }
}