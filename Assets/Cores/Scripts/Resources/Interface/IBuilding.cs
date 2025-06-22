public interface IBuilding
{
    public IBuildData Data { get;}

    /// <summary>
    /// Return result can build at index Of Grid
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool BuildAt(int index);
}

public interface IBuildData
{
    public string ToJson();
    public int Collect();
}