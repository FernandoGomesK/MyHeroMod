namespace MyHeroMod.content.System.Interfaces
{
    public interface IHeroTemperature
    {
        int Temperature { get; set; }
        int MaxTemperature { get; }
        int MinTemperature { get; }
        void AddHeat(int amount);

        void ReduceHeat(int amount);
    }
}