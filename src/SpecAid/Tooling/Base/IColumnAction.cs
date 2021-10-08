namespace SpecAid.Tooling.Base
{
    public interface IColumnAction
    {
        bool UseWhen();
        int ConsiderOrder { get; }
    }
}
