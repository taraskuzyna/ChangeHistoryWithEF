using AutoMapper;
using ChangeTrackerWithEF.Models;

namespace ChangeTrackerWithEF
{
    public static class AutomapperConfiguration
    {
        private static readonly MapperConfiguration Configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Address, HistAddress>();
            cfg.CreateMap<Person, HistPerson>();
            cfg.CreateMap<Car, HistCar>();
        });

        public static IMapper CreateMapper()
        {
            return Configuration.CreateMapper();
        }
    }
}
