public class Island
{
    float x;
    float y;
    float z;
    int level;

    public Island(float x, float z, int level)
    {
        this.x = x;
        this.z = z;
        this.y = 1.5f;
        this.level = level;
    }

    public float getX()
    {
        return x;
    }

    public float getY()
    {
        return y;
    }

    public float getZ()
    {
        return z;
    }

    public int getLevel()
    {
        return level;
    }
}