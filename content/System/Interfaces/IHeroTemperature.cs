


namespace MyHeroMod.content.System.Interfaces
{
    public interface IHeroTemperature : IStrainSource
    {
        int Temperature { get; set; }
        int MaxTemperature { get; }
        int MinTemperature { get; }
        int HeatPerSecond { get; set; }
        void AddHeat(int amount);
        void ReduceHeat(int amount);

    }


    public interface IStrainSource
    {
        int StrainPenaltyPerSecond { get; set; }
        void AddStrain(int amount);
    }
}