namespace MyHeroMod.content.System.Interfaces
{
    public interface IHeroTemperature
    {
        int Temperature { get; set; }
        int MaxTemperature { get; }
        int MinTemperature { get; }
        
        
        int HeatPerSecond { get; set; }         
        int StrainPenaltyPerSecond { get; set; }  
        
        void AddHeat(int amount);
        void ReduceHeat(int amount);
        
        
        void AddStrain(int amount); 
    }
}