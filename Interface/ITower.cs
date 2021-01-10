namespace AirTowerController
{
    //Air Tower Controller interface has only method to register item which inheretes from AbstractAirCraft.
    public interface ITower
    {
        void Register(AbstractAirCraft plane);
    }
}
