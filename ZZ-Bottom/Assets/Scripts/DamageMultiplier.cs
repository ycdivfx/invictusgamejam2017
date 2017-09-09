using System;

[Serializable]
public class DamageMultiplier
{
    public float Normal = 1f;
    public float Gold = 0f;
    public float Silver = 0f;
    public float Bronze = 0f;

    public float CalculateDamage(float amount, BulletType type)
    {
        var result = amount;
        switch (type)
        {
            case BulletType.Normal:
                result *= Normal;
                break;
            case BulletType.Gold:
                result *= Gold;
                break;
            case BulletType.Silver:
                result *= Silver;
                break;
            case BulletType.Bronze:
                result *= Bronze;
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
        return result;
    }
}