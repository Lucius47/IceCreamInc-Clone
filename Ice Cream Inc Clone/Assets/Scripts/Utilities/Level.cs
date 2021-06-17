using UnityEngine;

[System.Serializable]
public class Level
{
    public Sprite imageToMatch;
    public float blueCreamPercentage;
    public float greenCreamPercentage;

    public enum IceCreamTypes { blue, green}; // We need to know the order of ice creams in the cone.
    public IceCreamTypes firstIceCream;

    Level(Sprite _imageToMatch, float _blueCreamPercentage, float _greenCreamPercentage, IceCreamTypes _firstIceCream)
    {
        imageToMatch = _imageToMatch;
        blueCreamPercentage = _blueCreamPercentage;
        greenCreamPercentage = _greenCreamPercentage;
        firstIceCream = _firstIceCream;
    }
}
