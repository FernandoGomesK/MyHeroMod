


using KhacesCore.Content.System.Interfaces;

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

    public interface IHeroBreath
    {
        int BreathChangePerSecond { get; set; }
        void AddBreath(int amount);
    }

    public interface IHeroSweat
    {
        int SweatChangePerSecond { get; set; }
        void AddSweat(int amount);
    }

    public interface IFeatherCount
    {
        int FeatherChangePerSecond { get; }
        void RemoveFeathers(int amount);
        void AddFeathers(int amount);
    }
}
