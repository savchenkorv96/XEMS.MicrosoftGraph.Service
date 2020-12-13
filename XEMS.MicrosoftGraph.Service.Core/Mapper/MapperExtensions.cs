using AutoMapper;

namespace XEMS.MicrosoftGraph.Service.Core.Mapper
{
    public class MapperExtensions
    {
        public static Output Convert<Input, Output>(Input item)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Input, Output>());
            var mapper = new AutoMapper.Mapper(config);
            return mapper.Map<Output>(item);
        }
    }
}