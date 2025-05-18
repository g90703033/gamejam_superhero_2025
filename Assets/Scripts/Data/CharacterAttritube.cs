[System.Serializable]
public class CharacterAttributes
{
    public int weightLifting = 0;
    public int moveSpeed = 0;
    public int strength = 0;
    public int size = 0;

    // Addition operator
    public static CharacterAttributes operator +(CharacterAttributes a, CharacterAttributes b)
    {
        return new CharacterAttributes
        {
            weightLifting = a.weightLifting + b.weightLifting,
            moveSpeed = a.moveSpeed + b.moveSpeed,
            strength = a.strength + b.strength,
            size = a.size + b.size
        };
    }

    // Subtraction operator
    public static CharacterAttributes operator -(CharacterAttributes a, CharacterAttributes b)
    {
        return new CharacterAttributes
        {
            weightLifting = a.weightLifting - b.weightLifting,
            moveSpeed = a.moveSpeed - b.moveSpeed,
            strength = a.strength - b.strength,
            size = a.size - b.size
        };
    }
}
