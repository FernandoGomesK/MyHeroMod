namespace MyHeroMod.content.System.Interfaces
{
    public interface IHeroTemperature
    {
        int CurrentHeat { get; set; }
        int MaxHeat { get; }
        int MinimumHeat { get; }
        void AddHeat(int amount);
    }
}