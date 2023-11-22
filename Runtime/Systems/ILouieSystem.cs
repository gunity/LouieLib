namespace LouieLib.Systems
{
    public interface ILouieSystem
    {
        ILouieSystem Add(MonoSystem monoSystem);
        void Build();
    }
}